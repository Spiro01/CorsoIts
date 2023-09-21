using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mqtt.Services;
using MQTTnet.Client;

namespace Mqtt.Extensions;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddMqttClientServiceWithConfig(this IServiceCollection services, Action<MqttClientOptionsBuilder> configure)
    {
        services.AddSingleton<MqttClientOptions>(serviceProvider =>
        {
            var optionBuilder = new MqttClientOptionsBuilder();
            configure(optionBuilder);
            return optionBuilder.Build();
        });
        services.AddSingleton<IMqttClientService, MqttClientService>();
        services.AddSingleton<IHostedService>(serviceProvider =>
        {
            return serviceProvider.GetService<IMqttClientService>();
        });
        return services;
    }
}