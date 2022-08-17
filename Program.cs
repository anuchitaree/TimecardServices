using Serilog;
using TimecardServices.Workers;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
       
        services.AddHostedService<UploadDatabaseWorker>();
        services.AddHostedService<TransferFileWorker>();
        services.AddHostedService<InsertDataWorker>(); 
    })
    //.UseSerilog() 
    .Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
await host.RunAsync();