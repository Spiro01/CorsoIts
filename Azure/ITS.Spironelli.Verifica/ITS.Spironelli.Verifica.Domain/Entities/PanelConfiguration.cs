namespace ITS.Spironelli.Verifica.Domain.Entities;

public class PanelConfiguration
{
    public int Row { get; set; }
    public int Column { get; set; }
    public required string Color { get; set; }
}