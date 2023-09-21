namespace ApplicationCore.Configuration;

public class AppSettings
{
    public MqttClient MqttClient { get; set; }
    public InfluxDb InfluxDb { get; set; }

    public RabbitMq RabbitMq { get; set; }
}