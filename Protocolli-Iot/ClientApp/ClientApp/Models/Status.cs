using System.ComponentModel.DataAnnotations.Schema;

namespace ClientApp.Models;

public class Status
{
    public required string DroneId { get; set; }
    public required DateTime Time { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Altitude { get; set; }
    public double? BatteryLevel { get; set; }
}