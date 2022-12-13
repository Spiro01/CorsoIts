
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SendToDb;
using SendToDb.Interfaces;
using SendToDb.Services;

[assembly: FunctionsStartup(typeof(SendToDb.Startup))]

namespace SendToDb;

public class Startup : FunctionsStartup
{


    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingleton<IPerformanceDataStorage, SendToDb.Services.PerformanceSqlDataStorage>();
    }
}