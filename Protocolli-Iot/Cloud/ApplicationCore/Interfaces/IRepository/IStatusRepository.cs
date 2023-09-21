using Server.Models;

namespace Server.Interfaces.IRepository;

public interface IStatusRepository
{
    public Task Insert(Status status);
    public Task<IEnumerable<Status>> Get(string droneId, string fromTime);

    public Task Purge();
}