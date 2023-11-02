using System.IO.Ports;
using WorkerService1;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton(sp =>
            new SerialPort
            {
                BaudRate = 115200,
                Parity = Parity.None,
                PortName = "COM8",
                StopBits = StopBits.One,
                WriteTimeout = 200,
                ReadTimeout = 200,

            }
        );
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
