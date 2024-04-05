using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Services;
using Services.Interfaces;
using SmartId;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.Combine(AppContext.BaseDirectory))
    .AddJsonFile("appsettings.json", true, true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

var services = new ServiceCollection()
    .AddLogging(options =>
    {
        options.ClearProviders();
        options.AddConsole();
    })
    .AddSingleton<Application>()
    .AddSingleton<IConfiguration>(configuration)
    .AddTransient<IAuthenticator, Authenticator>()
    .AddTransient<ISmartIdClient, SmartIdClient>()
    .BuildServiceProvider();

var app = services.GetRequiredService<Application>();

await app.Run();