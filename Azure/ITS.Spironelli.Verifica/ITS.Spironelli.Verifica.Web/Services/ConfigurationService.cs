using ITS.Spironelli.Verifica.Web.Interfaces;
using ITS.Spironelli.Verifica.Web.Models;
using Microsoft.Azure.Devices;

namespace ITS.Spironelli.Verifica.Web.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly IPanelRepository _panelRepository;
    private readonly IConfiguration _configuration;

    public ConfigurationService(IPanelRepository panelRepository, IConfiguration configuration)
    {
        _panelRepository = panelRepository;
        _configuration = configuration;
    }

    public async Task<bool> ChangePanelConfiguration(int panelId, PanelConfiguration configuration)
    {
        var panel = await _panelRepository.GetById(panelId);
        if (panel is null) return false;



        using var manager = RegistryManager.CreateFromConnectionString(_configuration.GetConnectionString("Azure"));

        var twin = await manager.GetTwinAsync(panel.DeviceId);

        twin.Properties.Desired["row"] = configuration.Row;
        twin.Properties.Desired["column"] = configuration.Column;
        twin.Properties.Desired["color"] = configuration.Color;

        await manager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);


        return true;
    }
}