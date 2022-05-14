using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace CS455SystemCheckService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    private const string logPath = @"C:\temp\CS455SystemCheckService.log";

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            WriteToLog("hello from service adfadfadfasdaf");
            //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }

    public void WriteToLog(string message)
    {
        string text = String.Format("{0}:\t{1}", DateTime.Now, message);
        using (StreamWriter sw = new StreamWriter(logPath, append: true))
        {
            sw.WriteLine(text);
        }
    }
}
