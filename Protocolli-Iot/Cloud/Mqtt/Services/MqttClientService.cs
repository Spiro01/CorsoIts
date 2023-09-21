using System.Data;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mqtt.Attributes;
using Mqtt.Extensions;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace Mqtt.Services
{
    public class MqttClientService : IMqttClientService
    {
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions options;
        private readonly ILogger<MqttClientService> _logger;
        private readonly IServiceProvider serviceProvider;
        private IEnumerable<MethodInfo> _mqttAttributeMethods;

        public MqttClientService(MqttClientOptions options, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            this.options = options;
            this.serviceProvider = serviceProvider;
            _mqttClient = new MqttFactory().CreateMqttClient();
            _logger = loggerFactory.CreateLogger<MqttClientService>();
            ConfigureMqttClient();
        }

        private void ConfigureMqttClient()
        {
            _mqttClient.ConnectedAsync += HandleConnectedAsync;
            _mqttClient.DisconnectedAsync += HandleDisconnectedAsync;
            _mqttClient.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;
        }

        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {

            var methods = _mqttAttributeMethods
                .Where(p => ((MqttRouteAttribute)(p.GetCustomAttributes(typeof(MqttRouteAttribute), false)).First())
                    .Topic.IsSameTopic(eventArgs.ApplicationMessage.Topic));


            foreach (var method in methods)
            {
                var obj = serviceProvider.GetService(method.DeclaringType);
                method.Invoke(obj, new object?[] { eventArgs.ApplicationMessage }); // invoke the method
            }
        }

        public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            _logger.LogInformation("connected");
            var subscriptionTask = new List<Task>();

            _mqttAttributeMethods = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(x => x.GetTypes())
               .Where(x => x.IsClass)
               .SelectMany(x => x.GetMethods())
               .Where(p => p.GetCustomAttributes(typeof(MqttRouteAttribute), false).FirstOrDefault() != null);

            foreach (var method in _mqttAttributeMethods)
            {
                var attribute = (MqttRouteAttribute)(method.GetCustomAttributes(typeof(MqttRouteAttribute), false)).First();
                subscriptionTask.Add(_mqttClient.SubscribeAsync(attribute.Topic));
            }

            await Task.WhenAll(subscriptionTask);

        }

        public async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
        {

            _logger.LogInformation("HandleDisconnected");

            await Task.CompletedTask;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _mqttClient.ConnectAsync(options, cancellationToken);

            _ = Task.Run(
           async () =>
           {
               while (!cancellationToken.IsCancellationRequested)
               {
                   try
                   {
                       if (!await _mqttClient.TryPingAsync(cancellationToken))
                       {
                           await _mqttClient.ConnectAsync(_mqttClient.Options, cancellationToken);

                           // Subscribe to topics when session is clean etc.
                           _logger.LogInformation("The MQTT client is connected.");
                       }
                   }
                   catch (Exception ex)
                   {
                       // Handle the exception properly (logging etc.).
                       _logger.LogError(ex, "The MQTT client  connection failed");
                   }
                   finally
                   {
                       // Check the connection state every 5 seconds and perform a reconnect if required.
                       await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                   }
               }
           }, cancellationToken);

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                var disconnectOption = new MqttClientDisconnectOptions
                {
                    Reason = MqttClientDisconnectReason.NormalDisconnection,
                    ReasonString = "NormalDisconnection"
                };
                await _mqttClient.DisconnectAsync(disconnectOption, cancellationToken);
            }
            await _mqttClient.DisconnectAsync();
        }

        public async Task SendMessage(string topic, string message)
        {
            var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(message)
                    .Build();
            await _mqttClient.PublishAsync(applicationMessage);
        }

        public async Task SendMessageWithControl(string topic, string message, string responseTopic, Action<string?> callback)
        {
            await _mqttClient.SubscribeAsync(responseTopic);
            CancellationTokenSource source = new CancellationTokenSource();
            var ct = source.Token;


            _mqttClient.ApplicationMessageReceivedAsync += async eventArgs =>
            {
                if (eventArgs.ApplicationMessage.Topic == responseTopic)
                {
                    callback(eventArgs.ApplicationMessage.ConvertPayloadToString());
                    source.Cancel();
                    await _mqttClient.UnsubscribeAsync(responseTopic);

                }
            };

            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.ExactlyOnce)
                .WithRetainFlag(true)
                .Build();


            await _mqttClient.PublishAsync(applicationMessage);
            try
            {
                await Task.Delay(2000, ct);
            }
            catch (Exception ex)
            {

            }

            await _mqttClient.UnsubscribeAsync(responseTopic);
            if (!ct.IsCancellationRequested)
            {
                callback(null);
                _logger.LogError("response timeout reached in topic {topic}", topic);
            }

        }
    }
}