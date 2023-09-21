using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Server.Interfaces.IRepository;
using Server.Models;

namespace Server.Repository;

public class UserRepository : IUserRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public UserRepository(IDbContextFactory<ApplicationDbContext> contextFactory) => _contextFactory = contextFactory;

    public async Task<IEnumerable<User>> Get()
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        return await db.Users.ToListAsync();
    }

    public async Task<User> Get(Guid id)
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        return await db.Users.FindAsync(id);
    }

    public async Task<IEnumerable<User>> Search(string query)
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        return await db.Users.Where(x => String.Equals(x.Name, query, StringComparison.CurrentCultureIgnoreCase) || String.Equals(x.Surname, query, StringComparison.CurrentCultureIgnoreCase)).ToListAsync();
    }

    public async Task<User> Insert(User entity)
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        var newUser = await db.Users.AddAsync(entity);
        return newUser.Entity;
    }

    public async Task<User> Update(User entity)
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        var updateUser = db.Users.Update(entity);
        await db.SaveChangesAsync();
        return updateUser.Entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        using var db = await _contextFactory.CreateDbContextAsync();
        var userDelete = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
        if (userDelete is null) return false;
        db.Users.Remove(userDelete);
        await db.SaveChangesAsync();
        return true;
    }
}