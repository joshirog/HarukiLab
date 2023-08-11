using Haru.Api.Commons.Constants;
using WatchDog;
using WatchDog.src.Enums;

namespace Haru.Api.Commons.Configurations.Applications;

public static class ConfigureWatchDogApplication
{
    public static void AddWatchDogApplication(this WebApplication app)
    {
        app.UseWatchDogExceptionLogger();
        
        app.UseWatchDog(opt => 
        { 
            opt.WatchPageUsername = SettingConstant.WatchDogUserName; 
            opt.WatchPagePassword = SettingConstant.WatchDogPassword; 
            opt.Blacklist = string.Empty;
            opt.Serializer = WatchDogSerializerEnum.Newtonsoft;
            opt.CorsPolicy = string.Empty;
        });
    }
}