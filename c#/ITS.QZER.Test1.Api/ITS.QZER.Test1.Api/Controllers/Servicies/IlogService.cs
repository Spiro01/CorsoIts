using ITS.QZER.Test1.Api.Models;

namespace ITS.QZER.Test1.Api.Controllers.Servicies
{
    public interface IlogService
    {
        IEnumerable<Log> GetAll();
        IEnumerable<Log> GetLogByCode(string code);
        IEnumerable<string> GetAllPrograms();
    }
}
