using ElectroVamp.ApplicationCore.Config;
using ElectroVamp.ApplicationCore.Interfaces.Repositories;
using ElectroVamp.ApplicationCore.Interfaces.Services;
using ElectroVamp.Domain.Entities;
using Serilog;

namespace ElectroVamp.ApplicationCore.Services
{
    public class FirstGenService : IFirstGenService
    {
        private readonly GlobalConfiguration _globalConfiguration;

        private readonly OpcCli _kepServer;
        private readonly List<OpcCli> _opcMachines;
        private readonly ILogger _logger;


        private IBufferService _bufferService;
        public FirstGenService(GlobalConfiguration globalConfiguration, IOpcClient opcClient, ILogger logger, IBufferService bufferService)
        {
            _globalConfiguration = globalConfiguration;
            _kepServer = opcClient.Connect(OpcEndpoint.KepServer).First();
            _opcMachines = opcClient.Connect(OpcEndpoint.OpcMachines);
            _bufferService = bufferService;
            _logger = logger;
            SetNodeToMonitor();
            _logger.Information("FirstGenService initialized");

        }
        private async void SetNodeToMonitor()
        {
            foreach (var conn in _opcMachines)
            {
                var data = await conn.ReadAllValuesAsync(new OpcData());
                await conn.addNodeToMonitor(data.ToList());
                conn.MonitoredValueChange += Conn_MonitoredValueChange;
            }

        }

        private async void Conn_MonitoredValueChange(object? sender, EventArgs e)
        {

            var d = sender as OpcData;
            if (d.id == "WorkDone" && !(bool)d.data.WrappedValue.Value) return;
            _logger.Verbose($"Value changed {d.machineid} -- {d.id}", d);

            var currentmachine = await _bufferService.Reload(d.machineid);
            if (currentmachine == null) currentmachine = new MachineState() { MachineId = d.machineid };

            var send = new KepwareData();

            currentmachine.MachineId = d.machineid;
            try
            {
                var conn = _opcMachines.Where(x => x.MachineId == d.machineid).Single();
                var data = await conn.ReadAllValuesAsync(d);
                var alarm = data.Where(x => x.id == "Alarm").Select(x => (bool)x.data.WrappedValue.Value).Single();
                var machineready = data.Where(x => x.id == "MachineREADY").Select(x => (bool)x.data.WrappedValue.Value).Single();
                var machineon = data.Where(x => x.id == "MachineON").Select(x => (bool)x.data.WrappedValue.Value).Single();

                send.MachineId = d.machineid;
                send.Alarm = alarm;
                send.Other = !alarm && !machineon;
                send.Working = machineready && machineon && !alarm;

                if (d.id == "MachineREADY" && send.Working && (bool)d.data.WrappedValue.Value)
                {
                   currentmachine.time_t = DateTime.Now;
                }

                if (send.Working && d.id == "WorkDone" && (bool)d.data.Value)
                {
                    await conn.WriteValueAsync(OpcData.Parse(d.machineid, "WorkDone", false));
                    var deltaT = (ushort)(DateTime.Now - currentmachine.time_t).Seconds;
                    if(deltaT > 10000)  deltaT = 0;
                    currentmachine.time_t = DateTime.Now;
                    
                    send.CycleTime = deltaT;
                    currentmachine.counter++;

                    await _bufferService.SaveStatus(currentmachine);
                }
                send.Counter = currentmachine.counter;
                await _kepServer.WriteObjAsync(send);
            }

            catch (Exception ex)
            {
                _logger.Error("Error while writing {machineid} -- {id} {ex}", d.machineid, d.id, ex);
            }
        }

        public async Task StartFirstGen()
        {
            await Task.Delay(-1);
        }
    }
}
