using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AMQP.Services;

public class AMQPClientService : IAMQPClientService
{
    private readonly IConnection _connection;
    private readonly IModel model;

    public AMQPClientService(IConnection connection)
    {
        _connection = connection;


    }

    private void Configure()
    {
        var channel = _connection.CreateModel();
        channel.ExchangeDeclare("test", "topic", true, false, null);
        channel.QueueDeclare("myqueue", true, false, false, null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += MessageReceivedAsync;
        
    }

    private async Task MessageReceivedAsync(object sender, BasicDeliverEventArgs @event)
    {
        var message = Encoding.UTF8.GetString(@event.Body.Span);
        Console.WriteLine(message);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Configure();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}