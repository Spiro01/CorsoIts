namespace Server.Models;

public class Drone : Entity<string>
{
    public string Description { get; set; }
    public DateTime FirstFlight { get; set; }
}