using System.Text.RegularExpressions;
using TimecardServices.Data;
using TimecardServices.DTO;
using TimecardServices.Models;
using TimecardServices.Modules;

namespace TimecardServices.Workers
{
    public class InsertDataWorker : BackgroundService
    {
        private readonly ILogger<InsertDataWorker> _logger;

        private readonly IConfiguration _configuration;

        public InsertDataWorker(ILogger<InsertDataWorker> logger, IConfiguration configuration)
        {
            _logger = logger;

            _configuration = configuration;
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Initialize();
            return base.StartAsync(cancellationToken);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var db = new TimeCardContext())
                    {
                        if (db.Database.CanConnect())
                        {
                            _logger.LogInformation("The database is connected.");

                            string path = Param.ProcessFolder + "\\";

                            string[] fileLists = Directory.GetFiles(path);

                            if (fileLists.Length > 0)
                            {
                                Parallel.ForEach(fileLists, filename =>
                                {
                                    Task.Run(() => InsertDateOneFile(filename));
                                });
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "This is error of InsertDataWorker");
                }


                await Task.Delay(Param.ScanLoopTime * 1000, stoppingToken);
            }
        }


        private void Initialize()
        {
            Param.DbConnnectionString = _configuration.GetValue<string>("ConnectionString");

            Param.HttpPostUrl = _configuration.GetValue<string>("Settings:HttpPostUrl");

            Param.BackupFolder = _configuration.GetValue<string>("Settings:BackupFolderName");

            Param.HistoryOnOff = _configuration.GetValue<bool>("Settings:HistoryOnOff");

            Param.ScanLoopTime = _configuration.GetValue<int>("Settings:ScanLoopTime");

            Param.BaseFolder = _configuration.GetValue<string>("Settings:BaseFolder");

            CreateFolder.IsFolder();
        }

        private async Task InsertDateOneFile(string filename)
        {
            try
            {
                string[] records = File.ReadAllText(filename).Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                List<TimecardRecord> newRecord = new();

                foreach (string record in records)
                {
                    string[] parts = record.Split(',');  // ID , Direction ,date , hour , Machine  => 6000774,O,220403,1340,0011
                    if (parts.Length == 5)
                    {
                        string date = parts[2];
                        if (date.Length != 6 || isNum(date))
                            continue;
                        int yy = 2000 + int.Parse(date.Substring(0, 2));
                        int mm = int.Parse(date.Substring(2, 2));
                        int dd = int.Parse(date.Substring(4, 2));

                        string hour = parts[3];
                        if (hour.Length != 4 || isNum(hour))
                            continue;
                        int hh = int.Parse(hour.Substring(0, 2));
                        int m = int.Parse(hour.Substring(2, 2));

                        string mc = parts[4];
                        if (mc.Length != 4)
                            continue;

                        string direction = parts[1].ToUpper();
                        if (direction.Length != 1)
                            continue;

                        string emp = parts[0];
                        if (emp.Length != 7)
                            continue;

                        var input = new TimecardRecord()
                        {
                            Id = Guid.NewGuid().ToString(),
                            EmpId = emp,
                            Date = (new DateTime(yy, mm, dd, hh, m, 0)),
                            Direction = direction,
                            MachineSn = mc,

                        };
                        newRecord.Add(input);
                    }
                }


                using (var db = new TimeCardContext())
                {
                    await db.TimecardRecords.AddRangeAsync(newRecord);
                    await db.SaveChangesAsync();
                }
                if (Param.HistoryOnOff)
                    await MoveFilesNameAsync(true, filename);
                else
                    await DeleteFilesNameAsync(filename);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "The Postgres database is connected.");
            }

            bool isNum(string input)
            {
                return Regex.IsMatch(input, @"^\d{9}$");
            }

        }

        private async Task MoveFilesNameAsync(bool result, string filename)
        {
            string file = Path.GetFileName(filename);
            string historyfile = string.Format($"{Param.HistoryFolder}\\{file}");
            string uuid = Guid.NewGuid().ToString();

            string historyRenamefile = string.Format($"{Param.HistoryFolder}\\{uuid}-{file}");

            try
            {
                if (result)
                {
                    File.Move(filename, historyfile);
                    string log = String.Format($"can move fille {filename} to \".hist\" folder");
                    await LogFileAsync("OK", log);
                }

            }
            catch
            {

                try
                {
                    if (result)  // rename first
                    {
                        File.Move(filename, historyRenamefile);
                        string log = String.Format($"cannot move fille {filename} to \".hist\",rename file instead.");
                        await LogFileAsync("WN", log);
                    }
                }
                catch (Exception ex)
                {
                    File.Delete(filename);
                    string log = String.Format($"cannot move fille {filename} to \".hist\" folder, already deleted !!!!");
                    await LogFileAsync("WN", log);

                    _logger.LogError(ex.Message, "This is error");
                }

            }
        }


        private async Task DeleteFilesNameAsync(string filename)
        {
            try
            {
                File.Delete(filename);
                string log = String.Format($"cannot move fille {filename} to \".hist\" folder, already deleted !!!!");
                await LogFileAsync("WN", log);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "This is error");
            }


        }

        public async Task LogFileAsync(string result, string processname)
        {
            await Task.Run(async () =>
            {
                try
                {
                    var date = DateTime.Now;
                    result = result.Length < 6 ? result : result.Substring(0, 5);
                    string decription = processname.Length < 100 ? processname : processname.Substring(0, 100);
                    var newlog = new LogRecord()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Registdatetime = date,
                        Result = result,
                        Decription = decription
                    };
                    using (var db = new TimeCardContext())
                    {
                        await db.LogRecords.AddAsync(newlog);
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "This is error");
                }
            });

        }




    }
}
