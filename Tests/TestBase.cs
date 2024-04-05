using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public abstract class TestBase
{
    protected static readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
        .SetBasePath(Path.Combine(AppContext.BaseDirectory))
        .AddJsonFile("appsettings.json", true, true)
        .Build();

    protected readonly ServiceProvider _services = new ServiceCollection()
        .AddSingleton<IConfiguration>(_configuration)
        .BuildServiceProvider();
}