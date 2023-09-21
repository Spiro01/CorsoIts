using ClientApp.Data.Repository;
using ClientApp.Data.Services;
using ClientApp.Interfaces;
using Microsoft.Extensions.Logging;
using Mqtt.Extensions;
using RabbitMQ.Client;

namespace ClientApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddScoped<IDeviceService, DeviceService>();
        builder.Services.AddSingleton<GlobalStatusService>();
        builder.Services.AddScoped<IStatusRepository,StatusRepository>();
        builder.Services.AddAMQPClientServiceWithConfig(config =>
        {
            config.Endpoint = new AmqpTcpEndpoint("172.16.5.2");

        }); ;
        builder.Services.AddSingleton<MainPage>();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
