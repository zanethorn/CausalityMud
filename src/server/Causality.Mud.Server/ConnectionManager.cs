using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text;
using Causality.Mud.Common;
using Causality.Mud.Core;
using Causality.Mud.Server.Config;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceStack;

namespace Causality.Mud.Server;

public sealed class ConnectionManager: BackgroundService
{
    private readonly IHost _host;
    private readonly AppConfig _config;
    private readonly Socket _socket;
    private readonly List<IConnectionHandler> _handlers = new List<IConnectionHandler>();

    public ConnectionManager(IHost host)
    {
        _host = host;
        _config = host.Services.GetService<AppConfig>() ?? throw new KeyNotFoundException(nameof(AppConfig));
        _socket = new Socket(_config.Server.ServerEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _socket.Bind(_config.Server.ServerEndpoint);
        _socket.Listen(_config.Server.ServerEndpoint.Port);
        
        Task.Run(()=>CleanupConnections(stoppingToken));
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var newSocket = await _socket.AcceptAsync(stoppingToken);
            var newHandler = new ConnectionHandler(newSocket, _host);
            newHandler.RunAsync(stoppingToken);
            _handlers.Add(newHandler);
        }
        
        _socket.Close();
    }

    private void CleanupConnections(CancellationToken stoppingToken)
    {
        // cleanup dead memory
        while (!stoppingToken.IsCancellationRequested)
        {
            var stoppedHandlers = _handlers.Where(h => !h.Connected).ToArray();
            foreach (var handler in stoppedHandlers)
            {
                _handlers.Remove(handler);
                handler.Dispose();
            }

            Thread.Sleep(_config.Server.CleanupTimeoutMS);
        }
    }

    

    public override void Dispose()
    {
        _socket.Dispose();
        base.Dispose();
    }
}