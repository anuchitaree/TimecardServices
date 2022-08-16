using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TimecardServices.Data;
using TimecardServices.Models;
using TimecardServices.Modules;

namespace TimecardServices.Workers
{
    public class UploadApiWorker : BackgroundService
    {

        private HttpClient client;
        private readonly ILogger<UploadApiWorker> _logger;

        public UploadApiWorker(ILogger<UploadApiWorker> logger)
        {
            _logger = logger;
            client = null!;
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
                try
                {
                    using (var db = new NpgContext())
                    {
                        var existRecords = await db.TimecardRecords
                        .Where(s => s.Status == false).Take(1000)
                         .ToListAsync();

                        if (existRecords != null)
                        {
                            if (existRecords.Count > 0)
                            {
                                List<MpCalculateTimecardRecord> newRecords = new();
                                foreach (var s in existRecords)
                                {
                                    var record = new MpCalculateTimecardRecord()
                                    {
                                        Id = s.Id,
                                        EmpId = s.EmpId,
                                        Date = s.Date,
                                        Direction = s.Direction,
                                        MachineSn = s.MachineSn,
                                    };
                                    newRecords.Add(record);

                                }

                                string json = JsonConvert.SerializeObject(newRecords);
                                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                                var response = await client.PostAsync(Parameter.UploadUrl, httpContent);
                                if (response.IsSuccessStatusCode)
                                {
                                    _logger.LogInformation("the website is up. Status code {statuscode}", response.StatusCode);
                                    foreach (var update in existRecords)
                                    {
                                        update.Status = true;
                                        db.Entry(update).CurrentValues.SetValues(update);
                                    }
                                    db.SaveChanges();

                                }

                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "This is error");
                }


                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
