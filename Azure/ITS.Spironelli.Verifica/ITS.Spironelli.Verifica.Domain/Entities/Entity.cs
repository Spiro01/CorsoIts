
namespace ITS.Spironelli.Verifica.Domain.Entities;

public abstract class Entity <TKey>
{
    public TKey Id { get; set; }
}