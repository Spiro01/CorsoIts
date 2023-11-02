using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using QZER.SpironelliRiccardo.Web.Models;

namespace EsameWebApp.Persistence;
public class ApplicationDbContext : DbContext
{
    private readonly string _connString;

    public DbSet<Room> Room { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
        : base(options)
    {
        _connString = config.GetConnectionString("db") ?? throw new InvalidOperationException("The db connection string in empty");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connString);
    }

}