using Server.Models;

namespace Server.Interfaces.IRepository;

public interface IRepository<TEntity, TPrimaryKey>
    where TEntity : Entity<TPrimaryKey>
{
    Task<IEnumerable<TEntity>> Get();
    Task<TEntity> Get(TPrimaryKey id);
    Task<IEnumerable<TEntity>> Search(string query);
    Task<TEntity> Insert(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task<bool> Delete(TPrimaryKey id);
}