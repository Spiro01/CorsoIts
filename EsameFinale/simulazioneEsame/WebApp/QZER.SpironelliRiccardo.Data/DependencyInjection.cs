using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QZER.SpironelliRiccardo.Data.Persistence;
using QZER.SpironelliRiccardo.Data.Repository;

namespace QZER.SpironelliRiccardo.Data;
public static class DependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddScoped<IBoardRepository, BoardRepository>();

        services.AddDbContext<ApplicationDbContext>();
        return services;
    }
}
