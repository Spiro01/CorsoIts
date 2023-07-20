using System.IO.Ports;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using Raspberry.Worker.Models;

namespace WorkerService1;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly SerialPort _serial;
    public Worker(ILogger<Worker> logger, IConfiguration configuration, SerialPort serial)
    {
        _logger = logger;
        _configuration = configuration;
        _serial = serial;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connString = _configuration["ConnectionStrings:IotHub"];
        var deviceClient = DeviceClient.CreateFromConnectionString(connString);
        _serial.Open();

        while (!stoppingToken.IsCancellationRequested)
        {
            Message receivedMessage = await deviceClient.ReceiveAsync(stoppingToken);

            if (receivedMessage == null) continue;
            var message = JsonConvert.DeserializeObject<WelcomeMessage>(Encoding.ASCII.GetString(receivedMessage.GetBytes()));
            if(message is null) continue;
            //TODO data serialization
            var bufferToSend = new List<byte>
            {
                (byte)message.DeviceAddress
            };

            while (message.Message.Length < 16)
            {
                message.Message =  message.Message.Insert(message.Message.Length, Encoding.ASCII.GetString(new byte[]{0x00}));
            }

            bufferToSend.AddRange(Encoding.ASCII.GetBytes(message.Message));
           

            bufferToSend.AddRange(new []{(byte)message.Expiration.Hour, (byte)message.Expiration.Minute });

            _logger.LogInformation("Sending message to board");
            _serial.Write(bufferToSend.ToArray(),0,bufferToSend.Count);

            await deviceClient.CompleteAsync(receivedMessage, stoppingToken);
            await Task.Delay(2000, stoppingToken);
        }
    }
}
