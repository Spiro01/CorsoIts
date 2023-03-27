using Microsoft.Extensions.Configuration;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleOPC
{
    public class SimpleOPC_Client
    {
        public event EventHandler MonitoredValueChange;
        private Session session;
        private List<NodeId> Nodes;
        private IEnumerable<Node> NodeConf;
        private Config configs;
        private Subscription m_subscription;
        private MonitoredItem itemtomonitor;
        public SimpleOPC_Client(string confpath, Config conf)
        {
            if (conf == null) throw new NullReferenceException("No config provided");
            configs = conf;
            // Generate a client application
            ApplicationInstance application = new ApplicationInstance();
            application.ApplicationType = ApplicationType.Client;

            // Load the configuration file
            application.LoadApplicationConfiguration(confpath, false).Wait();

            ApplicationConfiguration m_configuration = application.ApplicationConfiguration;

            // Connect to a server

            // Get the endpoint by connecting to server's discovery endpoint.
            // Try to find the first endopint without security.
            EndpointDescription endpointDescription = CoreClientUtils.SelectEndpoint(conf.general.endpoint, false);

            EndpointConfiguration endpointConfiguration = EndpointConfiguration.Create(m_configuration);
            ConfiguredEndpoint endpoint = new ConfiguredEndpoint(null, endpointDescription, endpointConfiguration);

            // Create the session
            session = Session.Create(
            m_configuration,
            endpoint,
            false,
            false,
            m_configuration.ApplicationName,
            (uint)m_configuration.ClientConfiguration.DefaultSessionTimeout,
            new UserIdentity(),
            null).Result;

            LoadConfig();

        }
        public void LoadConfig() //Load configs from json file
        {
            NodeConf = configs.salvagnini.nodes.Union(configs.midage.nodes).Union(configs.first_gen.nodes);
            Nodes = NodeConf.Select(x => new NodeId(x.node, x.name_space)).ToList();
        }
        public NodeId getNodeId(string nodeid)
        {
            try
            {
                var query = NodeConf.Where(x => x.id == nodeid).Single();
                return Nodes.Where(x => (string)x.Identifier == query.node).First();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Invalid node, check the nodes ID");
                return null;
            }
        }
        public Node getNode(string id)
        {
            return NodeConf.Where(x => x.id == id).Single();
        }
        public void addNode(string address, ushort name_space)
        {
            Nodes.Add(new NodeId(address, name_space));
        }
        public async Task<IEnumerable<Data>> ReadAllValuesAsync()
        {
            var tasks = NodeConf.Select(x => Task.Run(() => { return Data.Parse(getNode(x.id), session.ReadValue(getNodeId(x.id))); }));
            var result = await Task.WhenAll(tasks);
            return result;
           
        }
        public async Task<Data> ReadValueAsync(string nodeid)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var value = Nodes.Where(x => x == getNodeId(nodeid)).Select(x => new Data(NodeConf.Where(y => x.Identifier == y.node).Single(), session.ReadValue(x))).Single();

                    return value;
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Error while reading data, check yours nodes id");
                    return new Data(null, null);
                }
            }
            );
        }
        public async Task WriteAsync(List<Data> data)
        {
            WriteValueCollection nodesToWrite = new WriteValueCollection();
           
            nodesToWrite.AddRange(data.Select(x => new WriteValue()
            {
                NodeId = getNodeId(x.node.id),
                AttributeId = Attributes.Value,
                Value = new DataValue()
                {
                    Value = x.data.WrappedValue
                }
            }));

            CancellationToken ct = new CancellationToken();
            // Call Write Service
            await session.WriteAsync(null, nodesToWrite, ct);
        }
        public async Task WriteAsync(Data data)
        {
            var datal = new List<Data>();
            datal.Add(data);
            await WriteAsync(datal);
        }
        public async Task DisposeAsync()
        {
            await Task.Run(() =>
            {
                session.Close();
                session.Dispose();
            });
        }
        public async Task addNodetoMonitorAsync(string nodeid)
        {
            if (m_subscription == null)
            {
                m_subscription = new Subscription(session.DefaultSubscription);
                m_subscription.PublishingEnabled = true;
                m_subscription.PublishingInterval = 1000;
                session.AddSubscription(m_subscription);
                m_subscription.Create();
            }

            var node = getNodeId(nodeid);
            itemtomonitor = new MonitoredItem(m_subscription.DefaultItem)
            {
                StartNodeId = node,
                AttributeId = Attributes.Value,
                DisplayName = null,
                MonitoringMode = MonitoringMode.Reporting,
                SamplingInterval = 1000,
                QueueSize = 0,
                DiscardOldest = true,
            };
            itemtomonitor.Notification += new MonitoredItemNotificationEventHandler(monitoredItem_Notification);
            m_subscription.AddItem(itemtomonitor);
            await m_subscription.ApplyChangesAsync();
        }
        public Data ParseData<T>(string nodeid, T data)
        {
            return Data.Parse(getNode(nodeid), data);
        }
        private void monitoredItem_Notification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {
            MonitoredItemNotification notification = e.NotificationValue as MonitoredItemNotification;
            if (notification == null)
            {
                return;
            }


            var sender = NodeConf.Where(x => x.node == monitoredItem.ResolvedNodeId.Identifier)
                .Select(x => new Data(x, notification.Value))
                .Single();


            var handler = MonitoredValueChange;

            handler.Invoke(sender, e);
        }
        public static Config LoadJson(string path)
        {
            throw new NotImplementedException("The LoadJson function is now obsolete");
        }
    }

    #region Configs
    public class Data
    {
        public Data(Node node, DataValue data)
        {
            this.node = node;
            this.data = data;
        }
        public Node node { get; set; }
        public DataValue data { get; set; }
        public static Data Parse<T>(Node node, T data)
        {
           
            var value = new DataValue();
            value.Value = (T)Convert.ChangeType(data, typeof(T));
            return new Data(node, value);
        }

    }
    public class Settings
    {
        public Config? config { get; set; }
    }
    public class Config
    {
        public Salvagnini? salvagnini { get; set; }
        public Midage? midage { get; set; }
        public first_gen? first_gen { get; set; }
        public General? general { get; set; }
    }
    public class General
    {
        public string? endpoint { get; set; }
    }
    public class Salvagnini
    {
        public Node[]? nodes { get; set; }
    }
    public class first_gen
    {
        public Node[]? nodes { get; set; }
    }
    public class Midage
    {
        public Node[]? nodes { get; set; }
        public string conn_string { get; set; }
        public int delay_time { get; set; }
    }
    public class Node
    {
        public string? id { get; set; }
        public string? node { get; set; }
        public ushort name_space { get; set; }
    }
    #endregion
}
