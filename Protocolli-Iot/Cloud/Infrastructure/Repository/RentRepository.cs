using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Server.Interfaces.IRepository;
using Server.Models;

namespace Server.Repository;

public class RentRepository : IRentRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public RentRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Rent>> Get()
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        return await db.Rents.ToListAsync();
    }

    public async Task<Rent> Get(Guid id)
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        return await db.Rents.FindAsync(id);
    }

    public async Task<IEnumerable<Rent>> Search(string query)
    {
        throw new NotImplementedException();
    }

    public async Task<Rent> Insert(Rent entity)
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        var insert =  await db.Rents.AddAsync(entity);
        return insert.Entity;
    }

    public async Task<Rent> Update(Rent entity)
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        var edit = db.Rents.Update(entity);
        await db.SaveChangesAsync();
        return edit.Entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        var delete = await db.Rents.FindAsync(id);
        if (delete is null) return false;
        db.Rents.Remove(delete);
        await db.SaveChangesAsync();
        return true;
    }
}