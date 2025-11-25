using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DevOpsProject.Shared.Routing;

public sealed class RouterUpdaterBackgroundService(ILogger<RouterUpdaterBackgroundService> logger, IRouterService routerService, IOptions<RouterServiceOptions> options) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(options.Value.RouterUpdatedDelayInMilliseconds, stoppingToken);

                if (routerService.IsRecalculationNeeded())
                {
                    routerService.RecalculateHops(options.Value.CurrentConnectionNameProvider());
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while executing router updater");
            }
        }
    }
}
