using Microsoft.EntityFrameworkCore;
using TimecardServices.Data;
using TimecardServices.Models;
using TimecardServices.Modules;

namespace TimecardServices.Workers
{
    public class UploadDatabaseWorker : BackgroundService
    {
        private readonly ILogger<UploadDatabaseWorker> _logger;

        public UploadDatabaseWorker(ILogger<UploadDatabaseWorker> logger)
        {
            _logger = logger;

        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
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
                                List<MpTimecardRecord> newRecords = new();
                                foreach (var s in existRecords)
                                {
                                    var record = new MpTimecardRecord()
                                    {
                                        Id = s.Id,
                                        EmpId = s.EmpId,
                                        Date = s.Date,
                                        Direction = s.Direction,
                                        MachineSn = s.MachineSn,
                                    };
                                    newRecords.Add(record);

                                }
                                using (var dbmp = new MsContext())
                                {
                                    await dbmp.MpTimecardRecords.AddRangeAsync(newRecords);
                                    await dbmp.SaveChangesAsync();
                                }
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
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "This is error");
                }


                await Task.Delay(Parameter.Scantime *1000, stoppingToken);
            }
        }
    }
}
