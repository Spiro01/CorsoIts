using ApplicationCore.Interfaces.Persistence;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Interfaces.IRepository;
using Server.Repository;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IStatusRepository, StatusRepository>();
        services.AddSingleton<IDroneRepository, DroneRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IRentRepository, RentRepository>();
        services.AddSingleton<IControlRepository, ControlRepository>();

        services.AddDbContextFactory<ApplicationDbContext>(options => options.UseNpgsql());
        return services;
    }
}