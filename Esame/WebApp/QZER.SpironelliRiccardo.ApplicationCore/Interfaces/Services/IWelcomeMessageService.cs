using QZER.SpironelliRiccardo.Domain.Entities;

namespace QZER.SpironelliRiccardo.ApplicationCore.Interfaces.Services;

public interface IWelcomeMessageService
{
    Task<bool> SetWelcomeMessage(WelcomeMessage message);
}