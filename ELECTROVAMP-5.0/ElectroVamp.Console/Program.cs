using ElectroVamp.ApplicationCore;
using ElectroVamp.ApplicationCore.Interfaces.Repositories;
using ElectroVamp.ApplicationCore.Interfaces.Services;
using ElectroVamp.ApplicationCore.Services;
using ElectroVamp.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;



namespace ElectroVamp.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
                  .ConfigureServices((hostContext, services) =>
                  {
                      // Add Appsettings configuration
                      services.Configure<AppSettings>(hostContext.Configuration);
                      services.AddSingleton(provider => provider.GetRequiredService<IOptions<AppSettings>>().Value);

                      //Register the actions
                      services.AddSingleton(provider => provider.GetRequiredService<AppSettings>().OpcClientConfiguration);
                      services.AddSingleton(provider => provider.GetRequiredService<AppSettings>().SalvagniniConfiguration);
                      services.AddSingleton(provider => provider.GetRequiredService<AppSettings>().GlobalConfiguration);

                      services.AddSingleton(provider => provider.GetRequiredService<AppSettings>().MariaDbConfiguration);
                      services.AddSingleton(provider => provider.GetRequiredService<AppSettings>().MariaDbConfiguration);

                      services.AddSingleton<IOpcClient, OpcClient>();
                      services.AddSingleton<IFirstGenService, FirstGenService>();
                      services.AddSingleton<ISalvagniniService, SalvagniniService>();
                      services.AddSingleton<ISalvagniniLogReader, SalvagniniLogReader>();
                      services.AddSingleton<IMidageClient, MariadbClient>();
                      services.AddSingleton<IMidageService, MidageService>();
                      services.AddSingleton<IResetService, ResetService>();
                      services.AddSingleton<IBufferService, BufferService>();
                      services.AddSingleton(Log.Logger = new LoggerConfiguration()
                                            .MinimumLevel.Verbose()
                                            .WriteTo.Console()
                                            .CreateLogger());

                      services.AddHostedService<ResetService>();
                      services.AddHostedService<BackgroundServiceA>();
                      services.AddHostedService<BackgroundServiceB>();
                      services.AddHostedService<BackgroundServiceC>();
                  });

    }
}