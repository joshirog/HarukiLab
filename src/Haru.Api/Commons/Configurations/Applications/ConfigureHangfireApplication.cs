using Hangfire;
using HangfireBasicAuthenticationFilter;
using Haru.Api.Commons.Constants;

namespace Haru.Api.Commons.Configurations.Applications;

public static class ConfigureHangfireApplication
{
    public static void AddHangfireApplication(this WebApplication app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "Hangfire Dashboard",
            Authorization = new[]
            {
                new HangfireCustomBasicAuthenticationFilter
                {
                    User = SettingConstant.HangfireUserName,
                    Pass = SettingConstant.HangfirePassword,
                }
            }
        });
    }
}