using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QZER.SpironelliRiccardo.ApplicationCore.Interfaces.Services;
using QZER.SpironelliRiccardo.ApplicationCore.Services;

namespace QZER.SpironelliRiccardo.ApplicationCore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IWelcomeMessageService, WelcomeMessageService>();
        services.AddScoped<ServiceClient>(sp => ServiceClient.CreateFromConnectionString(configuration.GetConnectionString("IotHub")));
        return services;
    }
}