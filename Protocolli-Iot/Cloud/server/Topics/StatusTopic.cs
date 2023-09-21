using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Mqtt.Attributes;
using Mqtt.Services;
using MQTTnet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Interfaces.IRepository;
using Server.Models;

namespace Server.Topics;

public class StatusTopic
{
    private readonly IStatusRepository _statusRepository;
    private readonly IMqttClientService _mqttClient;
    private readonly ILogger<StatusTopic> _logger;

    public StatusTopic(IStatusRepository statusRepository, IMqttClientService mqttClient, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<StatusTopic>();
        _statusRepository = statusRepository;
        _mqttClient = mqttClient;
    }

    [MqttRoute("DroneRental/+/status")]
    public async Task HandleMqttStatus(MqttApplicationMessage message)
    {
        
        try
        {
            var status = JsonConvert.DeserializeObject<Status>(Encoding.UTF8.GetString(message.Payload));
            await _mqttClient.SendMessage("response", JsonConvert.SerializeObject(status));
            await _statusRepository.Insert(status);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        
    }
}