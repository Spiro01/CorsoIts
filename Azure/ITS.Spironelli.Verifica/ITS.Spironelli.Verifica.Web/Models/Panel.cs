namespace ITS.Spironelli.Verifica.Web.Models;

public class Panel : Entity<int>
{
    public string DeviceId { get; set; }
    public string Description { get; set; }
}