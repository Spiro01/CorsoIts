using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;

namespace Mqtt.BackgrounddService
{
    public class MqttListener : BackgroundService
    {
        protected override async Task<Task> ExecuteAsync(CancellationToken ct)
        {

            var factory = new MqttFactory();
            using var mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer("localhost")
                .Build();

            await mqttClient.ConnectAsync(options, ct);

            await mqttClient.SubscribeAsync("prova", cancellationToken: ct);

            mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;


            await Task.Delay(-1, ct);

            return Task.CompletedTask;
        }

        private async Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)  
        {
            Console.WriteLine(Encoding.Default.GetString(arg.ApplicationMessage.Payload));
        }
    }
}
