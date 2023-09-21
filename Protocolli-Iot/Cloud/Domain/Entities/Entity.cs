

namespace Server.Models;

public abstract class Entity<TPrimaryKey>
{
    [System.ComponentModel.DataAnnotations.Key]
    public TPrimaryKey Id { get; set; }
}