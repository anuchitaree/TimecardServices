using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TimecardServices.Data;
using TimecardServices.DTO;
using TimecardServices.Modules;

namespace TimecardServices.Workers
{
    public class HttpPostWorker : BackgroundService
    {

        private HttpClient client;
        private readonly ILogger<HttpPostWorker> _logger;
        private readonly IConfiguration _configuration;

        public HttpPostWorker(ILogger<HttpPostWorker> logger, IConfiguration configuration)
        {
            _logger = logger;
            client = null!;
            _configuration = configuration;

        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client.Dispose();
            return base.StopAsync(cancellationToken);
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();

                bool result = false;
                int retryCount = 5;
                while (!result) // true : entry in the loop
                {
                    retryCount--;
                    try
                    {
                        using (var db = new TimeCardContext())
                        {
                            var existRecords = await db.TimecardRecords
                            .Where(s => s.Status == false).Take(2000)
                             .ToListAsync();

                            if (existRecords != null)
                            {
                                if (existRecords.Count > 0)
                                {
                                    List<TimecardReq> newRecords = new();
                                    foreach (var s in existRecords)
                                    {
                                        var record = new TimecardReq()
                                        {
                                            Id = s.Id,
                                            EmpId = s.EmpId,
                                            Date = s.Date.ToString("yyyy-MM-dd"),
                                            Direction = s.Direction,
                                            MachineSn = s.MachineSn,
                                        };
                                        newRecords.Add(record);

                                    }

                                    string json = JsonConvert.SerializeObject(newRecords, Formatting.Indented);
                                    StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                                    var response = await client.PostAsync(Param.HttpPostUrl, httpContent);
                                    if (response.IsSuccessStatusCode)
                                    {
                                        _logger.LogInformation("the website is up [ {0} ] Status code {statuscode}", Param.HttpPostUrl, response.StatusCode);
                                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                        {
                                            foreach (var update in existRecords)
                                            {
                                                update.Status = true;
                                                db.Entry(update).CurrentValues.SetValues(update);
                                            }
                                            await db.SaveChangesAsync();

                                            result = true; // exit while loop
                                        }
                                    }


                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{ex.Message} of \n { Param.HttpPostUrl} \n");
                        Task.Delay(30_000).Wait();
                        result = retryCount == 0 ? true : false;
                    }
                }

                //stopwatch.Stop();
                //TimeSpan timeTaken = stopwatch.Elapsed;


                await Task.Delay((Param.ScanLoopTime + 60) * 1000, stoppingToken);
            }
        }
    }
}
