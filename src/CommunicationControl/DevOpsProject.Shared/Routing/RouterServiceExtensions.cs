using Microsoft.Extensions.DependencyInjection;

namespace DevOpsProject.Shared.Routing;

public static class RouterServiceExtensions
{
    public static IServiceCollection AddRouterService(this IServiceCollection services, Action<RouterServiceOptions, IServiceProvider> configure)
    {
        services.AddOptions<RouterServiceOptions>()
            .Configure(configure)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddSingleton<IRouterService, RouterService>();
        services.AddHostedService<RouterUpdaterBackgroundService>();
        services.AddHostedService<IsAliveConnectionChecker>();
        return services;
    }
}
