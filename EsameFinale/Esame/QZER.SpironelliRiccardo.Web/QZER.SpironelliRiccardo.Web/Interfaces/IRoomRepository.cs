using QZER.SpironelliRiccardo.Web.Models;

namespace QZER.SpironelliRiccardo.Web.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room?> GetRoomAsync(Guid id);
        Task<IEnumerable<Room>> GetRoomAsync();
    }
}