using System.Text;
using System.Text.Json;
using AMQP.Services;
using RabbitMQ.Client.Events;
using Server.Interfaces.IRepository;
using Server.Models;

namespace Server.RabbitMq;


public class Messages : IHostedService
{
    private readonly IAMQPClientService _clientService;
    private readonly IStatusRepository _statusRepository;


    public Messages(IAMQPClientService clientService, IStatusRepository statusRepository)
    {
        _clientService = clientService;
        _statusRepository = statusRepository;


    }

    private async Task _clientService_OnMessageReceived(object sender, EventArgs @event)
    {
        var e = @event as BasicDeliverEventArgs;
        await _statusRepository.Insert(JsonSerializer.Deserialize<Status>(Encoding.UTF8.GetString(e.Body.Span)));
    }

    public  Task StartAsync(CancellationToken cancellationToken)
    {
        _clientService.OnMessageReceived += _clientService_OnMessageReceived;
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}