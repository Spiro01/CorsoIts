using EsameWebApp.Persistence;
using Microsoft.EntityFrameworkCore;
using QZER.SpironelliRiccardo.Web.Interfaces;
using QZER.SpironelliRiccardo.Web.Models;

namespace QZER.SpironelliRiccardo.Web.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _db;

        public RoomRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Room>> GetRoomAsync()
        {
            return await _db.Room.ToListAsync();
        }
        public async Task<Room?> GetRoomAsync(Guid id)
        {
            return await _db.Room.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
