using InfluxDB.Client.Core;

namespace Server.Models;

[Measurement("Status")]
public class Status : Sensor
{
    [Column("Latitude")] public decimal Latitude { get; set; }
    [Column("Longitude")] public decimal Longitude { get; set; }
    [Column("Altitude")] public decimal Altitude { get; set; }
    [Column("BatteryLevel")] public decimal BatteryLevel { get; set; }
}