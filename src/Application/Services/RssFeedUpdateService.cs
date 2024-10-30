using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.Services;

public class RssFeedUpdateService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RssFeedUpdateService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var rssFetchService = scope.ServiceProvider.GetRequiredService<IRssFetchService>();
                
            await rssFetchService.UpdateFeedItemsAsync(stoppingToken);
                
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Adjust interval as needed
        }
    }
}