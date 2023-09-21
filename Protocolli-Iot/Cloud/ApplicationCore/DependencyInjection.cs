using ApplicationCore.Configuration;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ApplicationCore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        services.AddSingleton<IControlService, ControlService>();
        return services;
    }

    public static IServiceCollection AddConfiguration(this IServiceCollection services, AppSettings localConfiguration)
    {
        
        services.AddSingleton(localConfiguration);
        services.AddSingleton(localConfiguration.InfluxDb);
        services.AddSingleton(localConfiguration.MqttClient);
        
        return services;
    }
}