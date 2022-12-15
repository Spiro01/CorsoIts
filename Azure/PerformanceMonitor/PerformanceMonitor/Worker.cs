using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
            var memorycounter = new PerformanceCounter("Memory", "Available MBytes");

            

            while (!stoppingToken.IsCancellationRequested)
            {
                using var deviceClient = DeviceClient.CreateFromConnectionString(_configuration.GetConnectionString("Device"));

                var cpuUsage = cpuCounter.NextValue();
                var ramUsage = memorycounter.NextValue();

                var messageBody = JsonSerializer.Serialize(new PerformanceData("Spiro",DateTime.Now, cpuCounter.NextValue(), memorycounter.NextValue()));

                using var message = new Message(Encoding.ASCII.GetBytes(messageBody))
                {
                    ContentType = "application/json",
                    ContentEncoding = "utf-8",
                };


                await deviceClient.SendEventAsync(message, stoppingToken);
                _logger.LogInformation("Logged {cpu} {ram}", cpuUsage,ramUsage);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}