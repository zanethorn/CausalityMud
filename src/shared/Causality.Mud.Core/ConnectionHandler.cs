using System.Net.Sockets;
using System.Text;
using Causality.Mud.Common;
using Causality.Mud.Core.CQRS.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Causality.Mud.Core;

public class ConnectionHandler:IConnectionHandler
{
    private const string ACK = "<|ACK|>";
    private const string EOM = "<|EOM|>";
    private static readonly byte[] ACK_BYTES = Encoding.UTF8.GetBytes(ACK);
    
    private readonly IMediator _mediator;
    private readonly Socket _socket;
    private readonly byte[] _buffer = new byte[1024];
    
    public ConnectionHandler(Socket socket, IHost host)
    {
        _socket = socket;
        _mediator = host.Services.GetService<IMediator>() 
                    ?? throw new KeyNotFoundException(nameof(IMediator));
    }

    public async Task RunAsync(CancellationToken systemStopToken)
    {
        var cancelToken = new CancellationToken();
        
        
        while (!systemStopToken.IsCancellationRequested && !cancelToken.IsCancellationRequested && _socket.Connected)
        {
            var bytesReceived = await _socket.ReceiveAsync(_buffer, SocketFlags.None, systemStopToken);
            var stringReceived = Encoding.UTF8.GetString(_buffer, 0, bytesReceived);
            
            var endOfMessage = stringReceived.IndexOf(EOM, StringComparison.InvariantCulture);
            if (endOfMessage > -1)
            {
                var request = new RawInputRequest(stringReceived.Substring(0, endOfMessage));
                await _socket.SendAsync(ACK_BYTES, cancelToken);
                
                await foreach (var response in _mediator.CreateStream(request, cancelToken))
                {
                    await _socket.SendAsync(response.ResponseBytes,0);
                }
            }

        }
        
    }

    public bool Connected => _socket.Connected;

    public void Dispose()
    {
        _socket.Dispose();
    }
}