using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QZER.SpironelliRiccardo.Data.Entities;

namespace QZER.SpironelliRiccardo.Data.Persistence;
public class ApplicationDbContext : DbContext
{
    private readonly string _connString;

    public DbSet<Board> Board { get; set; }
    public DbSet<Building> Building { get; set; }
    public DbSet<Gateway> Gateway { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
        : base(options)
    {
        _connString = config.GetConnectionString("db") ?? throw  new InvalidOperationException("The db connection string in empty");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        
        modelBuilder.Entity<Gateway>()
            .HasMany(e => e.Boards)
            .WithOne(e => e.Gateway)
            .HasForeignKey(e => e.GatewayId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

        modelBuilder.Entity<Building>()
            .HasMany(e => e.Gateway)
            .WithOne(e => e.Building)
            .HasForeignKey(e => e.BuildingId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connString);
    }

}