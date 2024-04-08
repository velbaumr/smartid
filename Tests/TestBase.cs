using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public abstract class TestBase
{
    protected static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
        .SetBasePath(Path.Combine(AppContext.BaseDirectory))
        .AddJsonFile("appsettings.json", true, true)
        .Build();

    protected readonly ServiceProvider Services = new ServiceCollection()
        .AddSingleton<IConfiguration>(Configuration)
        .BuildServiceProvider();
}