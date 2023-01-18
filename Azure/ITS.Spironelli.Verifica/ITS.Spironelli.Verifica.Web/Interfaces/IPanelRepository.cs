using ITS.Spironelli.Verifica.Domain.Entities;


namespace ITS.Spironelli.Verifica.Web.Interfaces;

public interface IPanelRepository
{
    Task<Panel?> GetById(int id);
}