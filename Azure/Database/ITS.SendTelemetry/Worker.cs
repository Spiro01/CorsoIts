using Microsoft.Azure.Devices.Client;
using System.Text;
using Newtonsoft.Json;

namespace ITS.SendTelemetry
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

            using var deviceClient = DeviceClient.CreateFromConnectionString(_configuration.GetConnectionString("Device"));
            while (!stoppingToken.IsCancellationRequested)
            {
                decimal temperatura;
                Console.WriteLine("Inserisci la temperatura");
                while (!decimal.TryParse(Console.ReadLine(), out temperatura) && !stoppingToken.IsCancellationRequested)
                {

                }

                var messageBody = JsonConvert.SerializeObject(new { temperatura });
                using var message = new Message(Encoding.ASCII.GetBytes(messageBody))
                {
                    ContentType = "application/json",
                    ContentEncoding = "utf-8",
                };

                
                await deviceClient.OpenAsync(stoppingToken);

                await deviceClient.SendEventAsync(message, stoppingToken);


            }
        }
    }
}