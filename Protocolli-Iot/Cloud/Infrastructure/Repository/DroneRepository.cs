using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Interfaces.IRepository;
using Server.Models;

namespace Server.Repository;


public class DroneRepository : IDroneRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public DroneRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Drone>> Get()
    { 
        using var db = await _contextFactory.CreateDbContextAsync();
        return await db.Drones.ToListAsync();
    }


    public async Task<Drone> Get(string id)
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        return await db.Drones.FindAsync(id);
    }


    public async Task<IEnumerable<Drone>> Search(string query)
    {
        throw new NotImplementedException();
    }


    public async Task<Drone> Insert(Drone entity)
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        var newDrone = await db.Drones.AddAsync(entity);
        await db.SaveChangesAsync();
        return newDrone.Entity;
    }


    public async Task<Drone> Update(Drone entity)
    {
        throw new NotImplementedException();
    }

  
    public async Task<bool> Delete(string id)
    {
        throw new NotImplementedException();
    }
}