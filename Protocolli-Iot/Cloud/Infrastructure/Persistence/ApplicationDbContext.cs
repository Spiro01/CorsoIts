using ApplicationCore.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.Models;
namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly string _connString;



    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
        : base(options)
    {
        _connString = config.GetConnectionString("postgres")!;
    }


    public DbSet<User> Users { get; set; }
    public DbSet<Drone> Drones { get; set; }
    public DbSet<Rent> Rents { get; set; }
    public DbSet<Control>Controls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("user");
        modelBuilder.Entity<Drone>().ToTable("drone");
        modelBuilder.Entity<Rent>().ToTable("rent");
        modelBuilder.Entity<Control>().ToTable("controls");
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connString);
    }

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }
}