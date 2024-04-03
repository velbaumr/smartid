using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public abstract class TestBase
{
    protected readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
        .SetBasePath(Path.Combine(AppContext.BaseDirectory))
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();

    protected readonly ServiceProvider _services = new ServiceCollection()
        .BuildServiceProvider();

}