using System.Text;
using System.Text.Json;
using ITS.Spironelli.Verifica.Web.Interfaces;
using ITS.Spironelli.Verifica.Web.Models;
using Microsoft.Azure.Devices;

namespace ITS.Spironelli.Verifica.Web.Services;



public class PanelAdsService : IPanelAdsService
{
    private readonly IConfiguration _configuration;
    private readonly IPanelRepository _panelRepository;

    public PanelAdsService(IConfiguration configuration, IPanelRepository panelRepository)
    {
        _configuration = configuration;
        _panelRepository = panelRepository;
    }

    public async Task<bool> SetPanelMessage(int panelId, PanelMessage message)
    {
        var panel = await _panelRepository.GetById(panelId);
        if (panel is null) return false;

        using var serviceClient = ServiceClient.CreateFromConnectionString(_configuration.GetConnectionString("Azure"));

        var commandMessage = new
            Message(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(message)));


        await serviceClient.SendAsync(panel.DeviceId, commandMessage);

        return true;

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