using System.ComponentModel.DataAnnotations;

namespace QZER.SpironelliRiccardo.Data.Entities;

public class Building : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(250)]
    public string? Description { get; set; }

    public virtual List<Gateway> Gateway { get; set; } = new();
}