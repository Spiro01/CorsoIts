using InfluxDB.Client.Core;

namespace Server.Models;

public abstract class Sensor
{
    [Column("DroneId", IsTag = true)] public string DroneId { get; set; }
    [Column(IsTimestamp = true)] public DateTime Time { get; set; }
}