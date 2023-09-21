using AMQP.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;


namespace Mqtt.Extensions;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddAMQPClientServiceWithConfig(this IServiceCollection services, Action<ConnectionFactory> configure)
    {
        services.AddSingleton<IConnection>(serviceProvider =>
        {
            var optionBuilder = new ConnectionFactory();
            configure(optionBuilder);
           return optionBuilder.CreateConnection();
        });
        services.AddSingleton<IAMQPClientService,AMQPClientService>();
        services.AddSingleton<IHostedService>(serviceProvider =>
        {
            return serviceProvider.GetService<IAMQPClientService>()!;
        });
        return services;
    }
}