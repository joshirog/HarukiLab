using WatchDog;

namespace Haru.Api.Commons.Configurations.Builders;

public static class ConfigureWatchDogBuilder
{
    public static void AddWatchDogBuilderBuilder(this WebApplicationBuilder builder)
    {
        builder.Logging.AddWatchDogLogger();
    }
}