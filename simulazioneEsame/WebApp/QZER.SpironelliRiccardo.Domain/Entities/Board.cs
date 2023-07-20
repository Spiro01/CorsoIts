using System.ComponentModel.DataAnnotations;

namespace QZER.SpironelliRiccardo.Data.Entities;

public class Board : BaseEntity
{
    [Required]
    public short Address { get; set; }
    [Required]
    public int RoomNumber { get; set; }
    [MaxLength(250)]
    public string? Description { get; set; }

    public Guid GatewayId { get; set; }
    public Gateway Gateway { get; set; } = null!;
}