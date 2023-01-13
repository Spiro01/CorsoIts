using ITS.Spironelli.Verifica.Web.Models;

namespace ITS.Spironelli.Verifica.Web.Interfaces;

public interface IPanelRepository
{
    Task<Panel?> GetById(int id);
}