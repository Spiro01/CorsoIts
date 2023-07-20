using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QZER.SpironelliRiccardo.Data.Entities;

public class Gateway : BaseEntity
{
    [Required]
    public int FloorNumber { get; set; }

    [MaxLength(250)] public string? Description { get; set; }
    public Guid BuildingId { get; set; }

    [MaxLength(50)] public string HubDeviceId { get; set; } = string.Empty;
    public Building Building { get; set; } = null!;
    public virtual List<Board> Boards { get; } = new();
    
}