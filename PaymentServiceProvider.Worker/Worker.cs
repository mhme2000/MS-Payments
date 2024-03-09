namespace PaymentServiceProvider.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly HttpClient _client;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        _client = new HttpClient(){BaseAddress = new Uri("http://localhost:7000/")};
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        const int timeOf24HoursInMilliseconds = 24 * 60 * 60 * 1000;
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTime.Now);
            _logger.LogInformation($"Next run at: {DateTime.Now.AddMilliseconds(timeOf24HoursInMilliseconds)}");
            var response = await _client.PutAsync($"payables?date={DateTime.Now}", null, stoppingToken);
            response.EnsureSuccessStatusCode();
            await Task.Delay(timeOf24HoursInMilliseconds, stoppingToken);
        }
    }
}
