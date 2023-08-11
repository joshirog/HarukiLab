namespace Haru.Api.Commons.Configurations.Applications;

public static class ConfigureEnvironmentApplication
{
    public static void AddEnvironmentApplication(this WebApplication app)
    {
        app.AddSwaggerApplication();
    }
}