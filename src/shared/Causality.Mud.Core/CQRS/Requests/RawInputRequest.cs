using Causality.Mud.Core.CQRS.Responses;
using MediatR;

namespace Causality.Mud.Core.CQRS.Requests;

public class RawInputRequest:IStreamRequest<RawInputResponse>
{
    public RawInputRequest(string rawInput)
    {
        RawInput = rawInput;
    }
    
    public string RawInput { get; }
}