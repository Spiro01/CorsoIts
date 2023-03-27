using ElectroVamp.ApplicationCore.Config;
using ElectroVamp.ApplicationCore.Interfaces.Services;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ElectroVamp.ApplicationCore.Services
{
    public class ResetService : BackgroundService, IResetService
    {
        private readonly GlobalConfiguration _globalConfiguration;
        private readonly ILogger _logger;
        public event EventHandler ResetTimeOccurred;
        public List<DateTime> ResetHours;

        public bool ResetNow = false;

        public ResetService(GlobalConfiguration globalconfiguration, ILogger logger)
        {
            _globalConfiguration = globalconfiguration;
            _logger = logger;

            ResetHours = new List<DateTime>();

            foreach (string s in _globalConfiguration.CounterReset)
            {
                string[] sample = s.Split(':');
                double result = double.Parse(sample[0]) + double.Parse(sample[1]) / 60;
                DateTime dt = DateTime.Today.AddHours(result);

                if (DateTime.Now >= dt)
                {
                    dt = dt.AddHours(24);
                }
                ResetHours.Add(dt);
            }
        }

        protected override async Task<Task> ExecuteAsync(CancellationToken ct)
        {
            _logger.Information("Reset Service started");
            while (!ct.IsCancellationRequested)
            {
                for (int i = 0; i < ResetHours.Count; i++)
                {
                    if (DateTime.Now >= ResetHours[i])
                    {
                        //Reset action
                        _logger.Information("Resetting all counters");
                        ResetNow = true;
                        ResetTimeOccurred(this,EventArgs.Empty);
                        ResetHours[i].AddHours(24);
                        
                    }
                }
                await Task.Delay(60000);
            }

            return Task.CompletedTask;
        }

        public async Task<bool> CheckResetNow()
        {
            if (ResetNow)
            {
                ResetNow = false;
                return true;
            }
            else
                await Task.Delay(60000);
            return false;
        }



    }
}

