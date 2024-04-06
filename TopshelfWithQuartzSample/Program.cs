using Serilog;
using Topshelf;
using TopshelfWithQuartzSample;

HostFactory.Run(hostConfigurator =>
{
    hostConfigurator.Service<MyService>(serviceConfigurator =>
    {
        serviceConfigurator.ConstructUsing(settings => new MyService());
        serviceConfigurator.WhenStarted(service => service.Start());
        serviceConfigurator.WhenStopped(service => service.Stop());
    });

    hostConfigurator.RunAsLocalSystem();

    Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

    hostConfigurator.UseSerilog(Log.Logger);

    hostConfigurator.SetServiceName("MyService");
});