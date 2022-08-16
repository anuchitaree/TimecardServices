using System.Net.Http.Headers;

namespace TimecardServices.Workers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient client;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            client = null!;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7028/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {
                    var result = await client.GetAsync("RatioBoard/ratio?section=0000");
                    if (result.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("the website is up. Status code {statuscode}", result.StatusCode);
                        var tempJson = await result.Content.ReadAsStringAsync();
                        //var  tempList = JsonConvert.DeserializeObject<List<Temperature>>(tempJson);
                    }
                    else
                    {
                        _logger.LogError("The Website is down.Status code {statusCode}", result.StatusCode);
                    }

                }
                catch
                {

                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}