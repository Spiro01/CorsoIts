using Server.Models;

namespace Server.Interfaces.IRepository;

public interface IUserRepository : IRepository<User,Guid>
{
    
}