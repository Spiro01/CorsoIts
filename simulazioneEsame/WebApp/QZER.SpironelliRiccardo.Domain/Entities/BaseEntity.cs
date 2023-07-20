using System.ComponentModel.DataAnnotations;

namespace QZER.SpironelliRiccardo.Data.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.Empty;
}