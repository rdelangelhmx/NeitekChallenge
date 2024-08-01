using Microsoft.EntityFrameworkCore;
using Server.Classes;
using Server.Entities;
using Server.Persistence;

namespace Server.Helpers;

public class SeedData : IHostedService 
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConfigApp appConfig;

    public SeedData(IServiceProvider serviceProvider, ConfigApp _appConfig)
    {
        _serviceProvider = serviceProvider;
        appConfig = _appConfig;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        using (var context = new NeitekContext(scope.ServiceProvider.GetRequiredService<DbContextOptions<NeitekContext>>()))
        {
            if (context.TblMetas.Any())
                return;

            for (var i = 0; i < 5; i++)
            {
                var fName = $"{(new string[] { "Fake", "Dummy", "Random" })[new Random().Next(3)]}";
                context.TblMetas.Add(
                new TblMetas
                {
                    Nombre = fName
                });
            }
            context.SaveChanges();
        }

    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
