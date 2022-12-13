using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace PerformanceMonitor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            var memorycounter = new PerformanceCounter("Memory", "Available MBytes", null);

            using var deviceClient = DeviceClient.CreateFromConnectionString("HostName=qzer3.azure-devices.net;DeviceId=spironelli;SharedAccessKey=slXAiPefx2BGGGeqeb71hEBBBVtzXiYhin00ReWBEWw=");

            while (!stoppingToken.IsCancellationRequested)
            {
                var cpuUsage = cpuCounter.NextValue();
                var ramUsage = memorycounter.NextValue();

                var messageBody =   JsonConvert.SerializeObject(new { Cpu = cpuUsage, Ram = ramUsage });

                using var message = new Message(Encoding.ASCII.GetBytes(messageBody))
                {
                    ContentType = "application/json",
                    ContentEncoding = "utf-8",
                };


                await deviceClient.SendEventAsync(message, stoppingToken);
                _logger.LogInformation("Logged {ram} {cpu}", cpuUsage,ramUsage);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}