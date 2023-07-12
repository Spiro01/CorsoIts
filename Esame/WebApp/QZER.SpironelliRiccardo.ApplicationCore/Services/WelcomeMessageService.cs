using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Rest;
using QZER.SpironelliRiccardo.Data.Entities;
using QZER.SpironelliRiccardo.Data.Repository;
using QZER.SpironelliRiccardo.Domain.Entities;
using QZER.SpironelliRiccardo.ApplicationCore.Interfaces.Services;

namespace QZER.SpironelliRiccardo.ApplicationCore.Services;


public class WelcomeMessageService : IWelcomeMessageService
{
    private readonly IBoardRepository _boardRepository;
    private readonly ServiceClient _serviceClient;
    public WelcomeMessageService(IBoardRepository boardRepository, ServiceClient serviceClient)
    {
        _boardRepository = boardRepository;
        _serviceClient = serviceClient;
    }
    public async Task<bool> SetWelcomeMessage(WelcomeMessage message)
    {
        var board = await _boardRepository.GetBoardByDeviceAddress(message.DeviceAddress);
        if (board is null) return false;

        var commandMessage = new
            Message(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(message)));


        await _serviceClient.SendAsync(board.Gateway.HubDeviceId, commandMessage);


        return true;
    }
}
