using QZER.SpironelliRiccardo.Data.Entities;

namespace QZER.SpironelliRiccardo.Data.Repository;

public interface IBoardRepository
{
    Task<IEnumerable<Board>> GetBoard();
    Task<Board> GetBoard(Guid id);
    Task<Board?> GetBoardByDeviceAddress(int address);
}