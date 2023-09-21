namespace Server.Models;

public class Control : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
}