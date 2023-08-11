using Haru.Api.Persistences.Seeders;

namespace Haru.Api.Tasks;

public class SeederBackgroundTask : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public SeederBackgroundTask(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        IdentitySeeder.Seed(_serviceProvider);
        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}