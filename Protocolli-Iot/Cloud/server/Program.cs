using ApplicationCore;
using ApplicationCore.Configuration;
using Infrastructure;
using Mqtt.Extensions;
using RabbitMQ.Client;
using Server.ActionFilters;
using Server.RabbitMq;
using Server.Topics;

var builder = WebApplication.CreateBuilder(args);

var localConfiguration = builder.Configuration.Get<AppSettings>();

builder.Services.AddConfiguration(localConfiguration);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
    options.Filters.Add<OkResponseFilter>();
    options.Filters.Add<BadResponseFilter>();
});

builder.Services.AddAMQPClientServiceWithConfig(config =>
{
    config.Endpoint = new AmqpTcpEndpoint(localConfiguration.RabbitMq.HostName);

});

builder.Services.AddMqttClientServiceWithConfig(configure =>
{
    configure.WithClientId(localConfiguration.MqttClient.Id);
    configure.WithTcpServer(localConfiguration.MqttClient.Host)
        .Build();
});


builder.Services.AddLogging(options =>
{
    options.AddConsole();
    options.SetMinimumLevel(LogLevel.Debug);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationCore();
builder.Services.AddInfrastructure();
builder.Services.AddSingleton<StatusTopic>();
builder.Services.AddHostedService<Messages>();
var app = builder.Build();

app.UseCors(option =>
{
    option.AllowAnyOrigin();
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();