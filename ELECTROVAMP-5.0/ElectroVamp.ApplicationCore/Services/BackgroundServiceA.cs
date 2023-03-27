using ElectroVamp.ApplicationCore.Config;
using ElectroVamp.ApplicationCore.Interfaces.Repositories;
using ElectroVamp.ApplicationCore.Interfaces.Services;
using Microsoft.Extensions.Hosting;

namespace ElectroVamp.ApplicationCore.Services
{
    public class BackgroundServiceA : BackgroundService
    {
        private readonly GlobalConfiguration _globalConfiguration;
        private readonly ISalvagniniService _salvagniniService;
        private readonly IResetService _resetservice;
        private readonly IBufferService _bufferservice;
        public BackgroundServiceA(GlobalConfiguration globalconfiguration, ISalvagniniService salvagniniService, IResetService resetservice, IBufferService buffer)
        {
            _globalConfiguration = globalconfiguration;
            _salvagniniService = salvagniniService;
            _resetservice = resetservice;
            _resetservice.ResetTimeOccurred += _resetservice_ResetTimeOccurred;
            _bufferservice = buffer;
        }

        private async void _resetservice_ResetTimeOccurred(object? sender, EventArgs e)
        {
         
            await _bufferservice.Reset();
        }

        protected override async Task<Task> ExecuteAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                await Task.Delay(-1);
            }

            return Task.CompletedTask;
        }


    }
}
