using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AMQP.Services;

public class AMQPClientService : IAMQPClientService
{
    private readonly IConnection _connection;
    private readonly IModel model;

    public event AsyncEventHandler<BasicDeliverEventArgs> OnMessageReceived ;


    public AMQPClientService(IConnection connection)
    {
        _connection = connection;
        

    }

    private void Configure()
    {
        var channel = _connection.CreateModel();
        
        channel.ExchangeDeclare("DroneRental",ExchangeType.Topic,true,false,null);
        channel.QueueDeclare("DroneStatusQueue", true, false, false, null);
        channel.QueueBind("DroneStatusQueue", "DroneRental","DroneStatus",null);

       var consumer = new EventingBasicConsumer(channel);
        consumer.Received += MessageReceivedAsync;
        channel.BasicConsume(queue: "DroneStatusQueue", autoAck: true, consumer: consumer);
    }

    private async void MessageReceivedAsync(object sender, BasicDeliverEventArgs @event)
    {
        if (OnMessageReceived is null) return;
        await OnMessageReceived.Invoke(this, @event);
    }

    public  Task StartAsync(CancellationToken cancellationToken)
    {
        Configure();
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    
}