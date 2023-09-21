namespace Mqtt.Attributes;

public class MqttRouteAttribute : Attribute
{
    public string Topic { get; set; }

    public MqttRouteAttribute(string route)
    {
        Topic = route;
    }
}