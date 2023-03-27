using ElectroVamp.ApplicationCore.Interfaces.Repositories;
using ElectroVamp.ApplicationCore.Interfaces.Services;
using ElectroVamp.Domain.Entities;
using Serilog;

namespace ElectroVamp.ApplicationCore.Services
{
    public class MidageService : IMidageService
    {

        private readonly IMidageClient _midageClient;
        private IEnumerable<MariadbData> _previousMidage;
        private List<MariadbData> _exclude;
        private bool _firstRun;
        private readonly OpcCli _kepServer;
        private readonly IBufferService _bufferService;

        private IEnumerable<MariadbData> _dblcurrent;
        private KepwareData _midageline;

        private readonly ILogger _logger;
        public MidageService(IBufferService bufferService, IOpcClient opcClient, IMidageClient midageClient, ILogger logger)
        {

            _midageClient = midageClient;

            _previousMidage = new List<MariadbData>();
            _exclude = new List<MariadbData>();
            _firstRun = false;

            _bufferService = bufferService;

            _kepServer = opcClient.Connect(OpcEndpoint.KepServer).First();

            _logger = logger;
            _logger.Information("Midage service started successfully");
        }
        public async Task Start()
        {
            _dblcurrent = await _midageClient.GetWorkingMachines();

            //if (is setted time) _midageClient.DeleteEndedProductions();

            if (_exclude != null)
            {
                foreach (var item in _exclude)
                {
                    _dblcurrent.ToList().Remove(item);
                }
            }

            if (_firstRun)
            {
                foreach (var currmachine in _dblcurrent)
                {
                    _midageline = new KepwareData();
                    var saved = await _bufferService.Reload(currmachine.MACHINE_ID);
                    
                    MariadbData prevmachine = _previousMidage.Where(data => data.MACHINE_ID == currmachine.MACHINE_ID).LastOrDefault();
                    
                    if (prevmachine is null) break;
                    _midageline.MachineId = currmachine.MACHINE_ID;
                    
                    if (currmachine.PARTS_DONE > prevmachine.PARTS_DONE)
                    {
                        
                        saved.counter = (ushort)(saved.counter + (currmachine.PARTS_DONE - prevmachine.PARTS_DONE));
                        
                        _midageline.CycleTime = Convert.ToUInt16(currmachine.TIME);
                    }
                    
                    _midageline.Counter = saved.counter;

                    _midageline.PNC = currmachine.PROGRAM_NAME;
                   

                    await _kepServer.WriteObjAsync(_midageline);
                    saved.MachineId = _midageline.MachineId;
                    await _bufferService.SaveStatus(saved);
                }
            }
            _firstRun = true;
            _previousMidage = _dblcurrent;
        }
    }
}
