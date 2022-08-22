
using TimecardServices.Workers;


IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<TransferFileWorker>();
        
        services.AddHostedService<InsertDataWorker>(); 

        //services.AddHostedService<UploadDatabaseWorker>();

        services.AddHostedService<UploadApiWorker>();

    })
   
    .Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

await host.RunAsync();