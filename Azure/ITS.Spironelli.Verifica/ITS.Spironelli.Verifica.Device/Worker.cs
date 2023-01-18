using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.Json;
using ITS.Spironelli.Verifica.Domain.Entities;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace ITS.Spironelli.Verifica.Device
{
    public class Worker : BackgroundService
    {

        private readonly IConfiguration _configuration;

        public Worker(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var deviceClient = DeviceClient.CreateFromConnectionString(_configuration.GetConnectionString("Device"));

            while (!stoppingToken.IsCancellationRequested)
            {

                var data = await deviceClient.GetTwinAsync();
                int rows = data.Properties.Desired["row"];
                int columns = data.Properties.Desired["column"];
                var c = (string)data.Properties.Desired["color"];
                if (Enum.TryParse(c, out ConsoleColor color))
                {
                    Console.ForegroundColor = color;
                }
                Message receivedMessage = await deviceClient.ReceiveAsync();

                if (receivedMessage == null) continue;
                var message = JsonConvert.DeserializeObject<PanelMessage>(Encoding.ASCII.GetString(receivedMessage.GetBytes()));

                for (int i = 0; i < rows; i++)
                {
                    if (i >= message.Rows.Count()) break;
                    Console.WriteLine($"{message.Rows.ToArray()[i]}");
                }

                await deviceClient.CompleteAsync(receivedMessage);
                await Task.Delay(2000, stoppingToken);


            }
        }
    }
}