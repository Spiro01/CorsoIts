using System.ComponentModel.DataAnnotations;

namespace QZER.SpironelliRiccardo.Web.Models
{
    public class Room : EntityBase
    {
        [Required]
        [MaxLength(125)]
        public string Name { get; set; }  = string.Empty;
        public string? GatewayId { get; set; }
        public int Timeout { get; set; } = 255;
    }
}
