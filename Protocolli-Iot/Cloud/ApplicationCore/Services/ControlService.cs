using System.Text.Json;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using Mqtt.Services;
using Newtonsoft.Json;
using Server.Interfaces.IRepository;
using Server.Models;

namespace ApplicationCore.Services;

public class ControlService : IControlService
{

    private IMqttClientService _mqttClientService;
    public ControlService(IMqttClientService mqttClientService)
    {
        _mqttClientService = mqttClientService;
    }
    public async Task<bool> SendCommand(Control control, Drone drone)
    {
        Feedback? response = null;
        await _mqttClientService.SendMessageWithControl($"DroneRental/{drone.Id}/command", control.Id.ToString(),
            $"DroneRental/{drone.Id}/feedback", res =>
            {
                response = res is null ? null : JsonConvert.DeserializeObject<Feedback>(res);
            });

        if (response is null) return false;
        if (response.ControlId == control.Id && drone.Id == drone.Id) return true;
        return false;
    }

    public async Task<bool> BroadCastCommand(Control control)
    {
        throw new NotImplementedException();
    }
}