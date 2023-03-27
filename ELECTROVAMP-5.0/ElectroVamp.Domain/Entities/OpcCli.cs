using Opc.Ua;
using Opc.Ua.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroVamp.Domain.Entities
{
    public class OpcCli
    {
        public string MachineId { get; }
        private Session session;
        private readonly List<Data> nodes;
        private Subscription m_subscription;
        public event EventHandler MonitoredValueChange;
        private readonly ILogger _logger;
        public OpcCli(Session session, List<Data> nodes, string MachineId, ILogger logger)
        {
            this.session = session;
            this.nodes = nodes;
            this.MachineId = MachineId;
            _logger = logger;
        }
        internal NodeId getNodeId(OpcData data)
        {
            try
            {
                return nodes.Where(x => data.id == x.id && x.machineid == data.machineid).Select(x => x.node).Single();
            }
            catch (Exception e)
            {
                _logger.Error("Error on node [{machineid} - {id}] from [{endp}]", data.machineid, data.id, session.ConfiguredEndpoint.EndpointUrl);
                throw new InvalidOperationException($"Bad NodeId, Check yours nodes");
            }

        }
        public async Task addNodeToMonitor(List<OpcData> node)
        {
            if (m_subscription == null)
            {
                m_subscription = new Subscription(session.DefaultSubscription);
                m_subscription.PublishingEnabled = true;
                m_subscription.PublishingInterval = 1000;
                session.AddSubscription(m_subscription);
                m_subscription.Create();
            }
            foreach (var n in node)
            {
                var itemtomonitor = new MonitoredItem(m_subscription.DefaultItem)
                {
                    StartNodeId = getNodeId(n),
                    AttributeId = Attributes.Value,
                    DisplayName = null,
                    MonitoringMode = MonitoringMode.Reporting,
                    SamplingInterval = 1000,
                    QueueSize = 0,
                    DiscardOldest = true,
                };
                itemtomonitor.Notification += new MonitoredItemNotificationEventHandler(monitoredItem_Notification);
                m_subscription.AddItem(itemtomonitor);
            }
            await m_subscription.ApplyChangesAsync();

        }
        private void monitoredItem_Notification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {
            MonitoredItemNotification notification = e.NotificationValue as MonitoredItemNotification;
            if (notification == null)
            {
                return;
            }
            var sender = nodes.Where(x => x.node == monitoredItem.StartNodeId)
                .Select(x => new OpcData() { id = x.id, machineid = MachineId, data = notification.Value })
                .Single();

            MonitoredValueChange(sender, e);
        }
        public async Task<OpcData> ReadValueAsync(OpcData node)
        {
            return await Task.Run(() =>
            {
                var value = session.ReadValue(getNodeId(node));
                return new OpcData() { id = node.id, machineid = node.id, data = value };
            });
        }
        public async Task<IEnumerable<OpcData>> ReadAllValuesAsync(OpcData opc)
        {
            var test = nodes.Where(x => x.machineid == opc.machineid || opc.machineid == null).Select(x => new OpcData() { id = x.id, machineid = x.machineid }).ToArray();

            var task = nodes.Where(x => x.machineid == opc.machineid || opc.machineid == null).Select(x => Task.Run(() =>
            {

                return new OpcData() { id = x.id, machineid = x.machineid, data = session.ReadValue(x.node) };

            }));

            return await Task.WhenAll(task);

        }
        public async Task WriteValueAsync(List<OpcData> node)
        {
            WriteValueCollection nodesToWrite = new WriteValueCollection();

            nodesToWrite.AddRange(node.Select(x => new WriteValue()
            {
                NodeId = getNodeId(x),
                AttributeId = Attributes.Value,
                Value = new DataValue()
                {
                    Value = x.data.WrappedValue
                }
            }));
            var ct = new CancellationToken();
            await session.WriteAsync(null, nodesToWrite, ct);
        }
        public async Task WriteValueAsync(OpcData node)
        {
            var write = new List<OpcData>() { node };
            await WriteValueAsync(write);
        }
        public async Task WriteObjAsync<T>(T data)
        {
            var obj = typeof(T).GetProperties()
                .Select(x => new obj() { name = x.Name, value = x.GetValue(data), type = x.PropertyType })
                .ToList();
            var machineid = obj.Where(x => x.name == "MachineId" || x.name == "Machine_Id").Select(x => x.value).First().ToString();
            obj.Remove(obj.Where(x => x.name == "MachineId").First());
            var result = obj.Where(x =>x.value != null).Where(x=>x.value.ToString() != "0").Select(x => OpcData.Parse(machineid, x.name, Convert.ChangeType(x.value, x.type))).ToList();
            
            await WriteValueAsync(result);
        }



    }

    public class Data
    {
        public Data(string id, NodeId node, DataValue value)
        {
            this.id = id;
            this.node = node;
            this.value = value;
        }
        public Data(string id, NodeId node)
        {
            this.id = id;
            this.node = node;
        }
        public Data(string machineid, string id, NodeId node)
        {
            this.machineid = machineid;
            this.id = id;
            this.node = node;
        }
        public Data(string id)
        {
            this.id = id;
        }
        public string machineid { get; set; }
        public string id { get; set; }
        public NodeId? node { get; set; }
        public DataValue? value { get; set; }

    }
    internal class obj
    {
        public string name { get; set; }
        public object value { get; set; }
        public Type type { get; set; }
       
    }

}
