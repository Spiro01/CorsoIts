using ElectroVamp.ApplicationCore.Config;
using ElectroVamp.ApplicationCore.Interfaces.Repositories;
using ElectroVamp.ApplicationCore.Interfaces.Services;
using Microsoft.Extensions.Hosting;

namespace ElectroVamp.ApplicationCore.Services
{
    public class BackgroundServiceC : BackgroundService
    {
        private readonly GlobalConfiguration _globalConfiguration;
        private readonly MidageConfiguration _midageConfiguration;
        private readonly IMidageService _midageService;
        private readonly IMidageClient _midageClient;
        public BackgroundServiceC(GlobalConfiguration globalconfiguration,IResetService reset, MidageConfiguration midageConfiguration, IMidageService midageService, IMidageClient midageClient)
        {
            _midageService = midageService;
            _midageClient = midageClient;
            _midageConfiguration = midageConfiguration;
            _globalConfiguration = globalconfiguration;
            reset.ResetTimeOccurred += Reset_ResetTimeOccurred;
        }

        private void Reset_ResetTimeOccurred(object? sender, EventArgs e)
        {
            _midageClient.DeleteEndedProductions();
        }

        protected override async Task<Task> ExecuteAsync(CancellationToken ct)
        {
            _midageClient.DeleteEndedProductions();
            while (!ct.IsCancellationRequested)
            {
                await _midageService.Start();
                await Task.Delay(_midageConfiguration.RefreshTime * 1000);
            }
            return Task.CompletedTask;
        }
    }
}
