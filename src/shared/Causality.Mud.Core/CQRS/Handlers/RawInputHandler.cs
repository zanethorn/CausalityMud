using Causality.Mud.Core.CQRS.Requests;
using Causality.Mud.Core.CQRS.Responses;
using MediatR;

namespace Causality.Mud.Core.CQRS.Handlers;

public class RawInputHandler:IStreamRequestHandler<RawInputRequest, RawInputResponse>
{
    public IAsyncEnumerable<RawInputResponse> Handle(RawInputRequest request, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            
        }
    }
}