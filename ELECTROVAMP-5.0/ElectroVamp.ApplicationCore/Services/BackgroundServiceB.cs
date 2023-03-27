using ElectroVamp.ApplicationCore.Config;
using ElectroVamp.ApplicationCore.Interfaces.Services;
using Microsoft.Extensions.Hosting;

namespace ElectroVamp.ApplicationCore.Services
{
    public class BackgroundServiceB : BackgroundService
    {

        private readonly IFirstGenService _firstGenService;

        public BackgroundServiceB(GlobalConfiguration globalconfiguration, IFirstGenService firstGenService)
        {
            _firstGenService = firstGenService;

        }
        protected override async Task<Task> ExecuteAsync(CancellationToken ct)
        {

            while (!ct.IsCancellationRequested)
            {
                await _firstGenService.StartFirstGen();
            }
            return Task.CompletedTask;
        }
    }
}
