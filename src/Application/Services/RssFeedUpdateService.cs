using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.Services;

public class RssFeedUpdateService(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var rssFetchService = scope.ServiceProvider.GetRequiredService<IRssFetchService>();
                
            await rssFetchService.UpdateFeedItemsAsync(stoppingToken);
                
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Adjust interval as needed
        }
    }
}