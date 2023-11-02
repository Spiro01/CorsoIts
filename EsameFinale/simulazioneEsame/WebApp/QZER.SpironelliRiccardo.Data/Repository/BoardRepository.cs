using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QZER.SpironelliRiccardo.Data.Entities;
using QZER.SpironelliRiccardo.Data.Persistence;

namespace QZER.SpironelliRiccardo.Data.Repository;
public class BoardRepository : IBoardRepository
{
    private readonly ApplicationDbContext _db;

    public BoardRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Board>> GetBoard()
    {
        return await _db.Board
            .Include(x=>x.Gateway)
            .ToListAsync();
    }
    public async Task<Board?> GetBoard(Guid id)
    {
        return await _db.Board
            .Include(x => x.Gateway)
            .FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<Board?> GetBoardByDeviceAddress(int address)
    {
        //FIXME 
        // La query non considera l'id del gateway
        return await _db.Board
            .Include(x => x.Gateway)
            .FirstOrDefaultAsync(x => x.Address == address);
    }
}
