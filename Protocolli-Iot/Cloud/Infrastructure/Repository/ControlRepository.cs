using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Server.Interfaces.IRepository;
using Server.Models;

namespace Server.Repository;

public class ControlRepository : IControlRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
    public ControlRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _contextFactory = dbContextFactory;
    }
    public async Task<IEnumerable<Control>> Get()
    {
        using var db = await _contextFactory.CreateDbContextAsync();

        return await db.Controls.ToListAsync();
    }

    public async Task<Control> Get(Guid id)
    {
        using var db = await _contextFactory.CreateDbContextAsync();

        return await db.Controls.SingleOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<IEnumerable<Control>> Search(string query)
    {
        throw new NotImplementedException();
    }

    public async Task<Control> Insert(Control entity)
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        var newControl = await db.AddAsync(entity);
        await db.SaveChangesAsync();
        return newControl.Entity;
    }

    public async Task<Control> Update(Control entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}