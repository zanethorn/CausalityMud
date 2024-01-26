// See https://aka.ms/new-console-template for more information

using System.Net.Sockets;
using Causality.Mud.Server;
using Causality.Mud.Server.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

Console.WriteLine("Starting Application...");
try
{
    var appBuilder = Host.CreateApplicationBuilder(args);
    appBuilder.Logging.AddConsole();
    
    // Register Configuration
    appBuilder.Configuration
        .AddJsonFile("appconfig.json", false)
        .AddJsonFile($"appconfig.{Environment.GetEnvironmentVariable("EnvironmentName")}.json", true)
        .AddEnvironmentVariables();

    appBuilder.Services.AddTransient<AppConfig>(provider =>
    {
        var configProvider = provider.GetService<IConfiguration>()
                             ?? throw new KeyNotFoundException(nameof(IConfiguration));
        var config = new AppConfig();
        configProvider.Bind(config);
        return config;
    });
    
    // Register MediatR
    appBuilder.Services
        .AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Causality.Mud.Core.AssemblyMarker).Assembly); });
    
    // Register Redis
    appBuilder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
        ConnectionMultiplexer.Connect(
            provider.GetService<IConfiguration>()?.GetConnectionString("redis") ?? "localhost"
            )
        );
    appBuilder.Services.AddTransient<IDatabase>(provider =>
        provider.GetService<IConnectionMultiplexer>()?.GetDatabase()
        ?? throw new KeyNotFoundException(nameof(IConnectionMultiplexer)));
    

    appBuilder.Services.AddHostedService<ConnectionManager>();
    using var app = appBuilder.Build();
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}