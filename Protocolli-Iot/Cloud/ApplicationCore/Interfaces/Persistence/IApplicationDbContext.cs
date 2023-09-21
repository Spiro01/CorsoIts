namespace ApplicationCore.Interfaces.Persistence;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync();
}