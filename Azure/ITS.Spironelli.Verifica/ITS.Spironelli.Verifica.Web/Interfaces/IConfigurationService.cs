using ITS.Spironelli.Verifica.Web.Models;

namespace ITS.Spironelli.Verifica.Web.Interfaces;

public interface IConfigurationService
{
    Task<bool> ChangePanelConfiguration(int panelId, PanelConfiguration configuration);
}