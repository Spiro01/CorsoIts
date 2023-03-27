using ElectroVamp.ApplicationCore.Config;
using ElectroVamp.ApplicationCore.Interfaces.Repositories;
using ElectroVamp.ApplicationCore.Interfaces.Services;
using ElectroVamp.Domain.Entities;
using Serilog;

namespace ElectroVamp.ApplicationCore.Services
{
    public class SalvagniniService : ISalvagniniService
    {
        private readonly GlobalConfiguration _globalConfiguration;
        private readonly ISalvagniniLogReader _logReader;
        private readonly ILogger _logger;
        private readonly IBufferService _bufferService;
        private readonly OpcCli kepware;
     


        public SalvagniniService(IOpcClient opcClient, GlobalConfiguration globalConfiguration, ISalvagniniLogReader salvagniniLogReader, ILogger logger, IBufferService bufferService)
        {
            _globalConfiguration = globalConfiguration;
            _logReader = salvagniniLogReader;
            _logger = logger;
            kepware = opcClient.Connect(OpcEndpoint.KepServer).First();
            _logReader.onLogCreated += _salvagniniLogReader_onLogCreated;
            _logReader.Start();
            _bufferService = bufferService;

            _logger.Information("SalvagniniService loaded successfully");
        }

        private async void _salvagniniLogReader_onLogCreated(object? sender, EventArgs e)
        {

            LogLine ll = sender as LogLine;
            var ev = e as FileSystemEventArgs;
            string filepath = Directory.GetParent(ev.FullPath).Name;

            KepwareData sk = new(filepath, Convert.ToInt32(ll.Code), ll.Program);

            var currentmachine = await _bufferService.Reload(filepath);
            currentmachine.MachineId = filepath;
            if (ll.Quantity_Produced != 0)
            {

                if (ll.Quantity_Produced > currentmachine.buffer || ll.Quantity_Produced == 0)
                {
                    currentmachine.counter++;
                    sk.Counter = currentmachine.counter;
                    sk.CycleTime = (ushort)(DateTime.Parse(ll.Date_Time) - currentmachine.time_t).TotalSeconds;
                    currentmachine.time_t = DateTime.Parse(ll.Date_Time);
                    //sk.CycleTime = (ushort)Convert.ToInt16((Int64.Parse(ll.Time_T)) - (Int64.Parse(llsecondlast.Time_T)));
                }
                else
                {
                    sk.Counter = currentmachine.counter;
                }
            }
            else
            {
                sk.Counter = currentmachine.counter;
            }

            currentmachine.buffer = (int)ll.Quantity_Produced;


            _logger.Verbose("Sending data to kepware {counter}", sk.MachineId);

            try
            {
                await kepware.WriteObjAsync(sk);
            }
            catch (Exception ex)
            {
                _logger.Error("Error while sending data to kepware {ex}", ex);
            }
            finally
            {
                await _bufferService.SaveStatus(currentmachine);
            }
        }

      
    }
}
