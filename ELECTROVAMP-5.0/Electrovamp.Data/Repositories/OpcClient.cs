using ElectroVamp.ApplicationCore.Config;
using ElectroVamp.ApplicationCore.Interfaces.Repositories;
using ElectroVamp.Domain.Entities;

using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using Serilog;
namespace ElectroVamp.Data.Repositories
{
    public class OpcClient : IOpcClient
    {
        private readonly OpcClientConfiguration _opcClientConfiguration;
        private readonly GlobalConfiguration _globalConfiguration;
        private readonly ILogger _logger;
        private Session session;
        private List<Domain.Entities.Data> nodes;
        public OpcClient(OpcClientConfiguration opcClientConfiguration, GlobalConfiguration globalConfiguration, ILogger logger) //Load the configuration from appsettings.json
        {
            _globalConfiguration = globalConfiguration;
            _opcClientConfiguration = opcClientConfiguration;
            _logger = logger;
        }

        public List<OpcCli> Connect(OpcEndpoint endpt)
        {
            List<OpcCli> cli = new List<OpcCli>();
            if (endpt == OpcEndpoint.KepServer)
            {
                var session = LoadConfig(_opcClientConfiguration.KepServerConfiguration.Url);
                var machine = _opcClientConfiguration.KepServerConfiguration.Machines.Select(x => new { x.Nodes, x.MachineId });

                nodes = machine
                    .SelectMany(x => x.Nodes.Select(y => new Domain.Entities.Data(x.MachineId, y.id, new NodeId(y.node, Convert.ToUInt16(y.name_space)))))
                    .ToList();
                cli.Add(new OpcCli(session, nodes, null, _logger));
                _logger.Information("Loaded {num} nodes for {machineid}", nodes.Count, "KepServer");
                return cli;

            }
            else if (endpt == OpcEndpoint.OpcMachines)
            {
                foreach (var opcmachine in _opcClientConfiguration.FirstGenConfiguration)
                {
                    var session = LoadConfig(opcmachine.Url);
                    var machine = new { opcmachine.MachineId, opcmachine.Nodes };
                    nodes = machine.Nodes
                        .Select(x => new Domain.Entities.Data(machine.MachineId, x.id, new NodeId(x.node, Convert.ToUInt16(x.name_space))))
                        .ToList();
                    _logger.Information("Loaded {num} nodes for {machineid}", nodes.Count, opcmachine.MachineId);
                    cli.Add(new OpcCli(session, nodes, opcmachine.MachineId, _logger));
                }
                return cli;
            }
            else
                throw new InvalidOperationException();

        }
        private Session LoadConfig(string endpt)
        {
            ApplicationInstance application = new ApplicationInstance();
            application.ApplicationType = ApplicationType.Client;

            // Load the configuration file
            application.LoadApplicationConfiguration(_globalConfiguration.ConfigPath, false).Wait();

            ApplicationConfiguration m_configuration = application.ApplicationConfiguration;

            // Connect to a server

            // Get the endpoint by connecting to server's discovery endpoint.
            // Try to find the first endopint without security.
            EndpointDescription endpointDescription = CoreClientUtils.SelectEndpoint(endpt, false);

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
            return session;
        }

    }


}
