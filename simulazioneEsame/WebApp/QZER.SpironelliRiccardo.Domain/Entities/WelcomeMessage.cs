namespace QZER.SpironelliRiccardo.Domain.Entities;

public class WelcomeMessage
{
    public int DeviceAddress { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}