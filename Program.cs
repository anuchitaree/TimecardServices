
using TimecardServices.Workers;



IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<TransferFileWorker>();
        
        services.AddHostedService<InsertDataWorker>();


        services.AddHostedService<HttpPostWorker>();


    })
   
    .Build();

//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

await host.RunAsync();