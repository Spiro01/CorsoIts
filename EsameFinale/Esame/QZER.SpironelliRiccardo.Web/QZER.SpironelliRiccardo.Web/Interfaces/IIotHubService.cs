using QZER.SpironelliRiccardo.Web.Models;

namespace QZER.SpironelliRiccardo.Web.Interfaces
{
    public interface IIotHubService
    {
        Task<bool> SendIotHubMessage(string device, IotHubMessage message);
    }
}