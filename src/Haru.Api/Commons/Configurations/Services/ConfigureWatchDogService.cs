using Haru.Api.Commons.Constants;
using WatchDog;
using WatchDog.src.Enums;

namespace Haru.Api.Commons.Configurations.Services;

public static class ConfigureWatchDogService
{
    public static void AddWatchDogService(this IServiceCollection services)
    {
        services.AddWatchDogServices(opt => 
        { 
            opt.IsAutoClear = false; 
            opt.SetExternalDbConnString = SettingConstant.WatchDogConnectionString; 
            opt.DbDriverOption = WatchDogDbDriverEnum.Mongo; 
        });
    }
}