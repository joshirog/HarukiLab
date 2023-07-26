using Haru.Api.Commons.Constants;
using Haru.Api.Persistences.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Haru.Api.Commons.Configurations.Services;

public static class ConfigureContextService
{
    public static void AddContextService(this IServiceCollection services)
    {
        services.AddDbContext<DefaultContext>(options =>
            options.UseNpgsql(SettingConstant.DefaultConnectionString, b => b.MigrationsAssembly(typeof(DefaultContext).Assembly.FullName)));
    }
}