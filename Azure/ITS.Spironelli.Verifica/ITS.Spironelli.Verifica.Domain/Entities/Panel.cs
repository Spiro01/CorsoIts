namespace ITS.Spironelli.Verifica.Domain.Entities;

public class Panel : Entity<int>
{
    public string DeviceId { get; set; }
    public string Description { get; set; }
}