using TimecardServices.Modules;

namespace TimecardServices.Workers
{
    public class TransferFileWorker : BackgroundService
    {
        private readonly ILogger<TransferFileWorker> _logger;
        public TransferFileWorker(ILogger<TransferFileWorker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            CreateFolder.IsFolder();
            return base.StartAsync(cancellationToken);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    CreateFolder.IsFolder();

                    string destinationPath = Parameter.ProcessFolder;
                    string path = Parameter.FromFolder + "\\";
                    string[] fileLists = Directory.GetFiles(path);
                    if (fileLists.Length > 0)
                    {

                        foreach (var filename in fileLists)
                        {
                            string file = Path.GetFileName(filename);
                            string sourcefile = path + file;
                            string DestinationPath = destinationPath + "\\" + file;

                            using (StreamReader SourceReader = File.OpenText(sourcefile))
                            {
                                using (StreamWriter DestinationWriter = File.CreateText(DestinationPath))
                                {
                                    await CopyFilesAsync(SourceReader, DestinationWriter);
                                }
                            }
                            await DeleteFileAsync(sourcefile);

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


        async static Task CopyFilesAsync(StreamReader Source, StreamWriter Destination)
        {
            char[] buffer = new char[0x1000];
            int numRead;
            while ((numRead = await Source.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                await Destination.WriteAsync(buffer, 0, numRead);
            }
        }


        static Task DeleteFileAsync(string file)
        {
            return Task.Run(() =>
            {
                File.Delete(file);
            });
        }









    }
}
