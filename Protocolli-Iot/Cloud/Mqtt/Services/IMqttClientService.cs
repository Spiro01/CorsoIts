using Microsoft.Extensions.Hosting;


namespace Mqtt.Services;

public interface IMqttClientService : IHostedService
{
    public Task SendMessage(string topic, string message);
    public Task SendMessageWithControl(string topic, string message, string responseTopic, Action<string?> callback);
}