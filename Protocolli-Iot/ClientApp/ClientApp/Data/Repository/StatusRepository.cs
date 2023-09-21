using System.Text;
using System.Text.Json;
using ClientApp.Interfaces;
using ClientApp.Models;
using RabbitMQ.Client;

namespace ClientApp.Data.Repository;

public class StatusRepository : IStatusRepository
{
    private readonly IConnection _connection;

    public StatusRepository(IConnection connection)
    {
        _connection = connection;
    }

    public async Task PublishStatus(Location position,double batteryLevel)
    {
        using var channel = _connection.CreateModel();

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Status
        {
            DroneId = "RealDrone",
            Time = DateTime.UtcNow,
            Altitude = position.Altitude,
            BatteryLevel = batteryLevel, 
            Latitude = position.Latitude,
            Longitude = position.Longitude
        }));

        channel.BasicPublish("DroneRental", "DroneStatus", false, null, body);
    }
}