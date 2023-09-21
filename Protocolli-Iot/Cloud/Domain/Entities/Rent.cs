using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class Rent : Entity<Guid>
{
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    
    [ForeignKey("Drone")]
    public string DroneId { get; set; }
    public virtual Drone Drone { get; set; }
    
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}