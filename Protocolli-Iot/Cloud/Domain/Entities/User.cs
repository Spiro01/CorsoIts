namespace Server.Models;

public class User : Entity<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
}