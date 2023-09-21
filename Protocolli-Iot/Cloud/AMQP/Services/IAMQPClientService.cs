using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;

namespace AMQP.Services;

public interface IAMQPClientService :IHostedService
{
    public event AsyncEventHandler<BasicDeliverEventArgs> OnMessageReceived;
}