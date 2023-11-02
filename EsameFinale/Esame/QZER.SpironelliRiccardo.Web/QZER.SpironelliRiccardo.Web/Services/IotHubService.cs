using Microsoft.Azure.Devices;
using QZER.SpironelliRiccardo.Web.Interfaces;
using QZER.SpironelliRiccardo.Web.Models;
using System.Text;
using System.Text.Json;

namespace QZER.SpironelliRiccardo.Web.Services
{
    public class IotHubService : IIotHubService
    {
        private readonly ServiceClient _serviceClient;

        public IotHubService(ServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<bool> SendIotHubMessage(string device, IotHubMessage message)
        {
            var commandMessage = new
                Message(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(message)));


            await _serviceClient.SendAsync(device, commandMessage);


            return true;
        }
    }
}
