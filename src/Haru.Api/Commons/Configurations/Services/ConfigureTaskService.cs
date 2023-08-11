using Haru.Api.Tasks;

namespace Haru.Api.Commons.Configurations.Services;

public static class ConfigureBackgroundTaskService
{
    public static void AddBackgroundTaskService(this IServiceCollection services)
    {
        services.AddHostedService<SeederBackgroundTask>();
    }
}