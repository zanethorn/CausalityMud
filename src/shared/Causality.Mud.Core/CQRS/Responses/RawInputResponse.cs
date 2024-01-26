using System.Text;
using Causality.Mud.Core.CQRS.Requests;

namespace Causality.Mud.Core.CQRS.Responses;

public class RawInputResponse
{
    public RawInputResponse(RawInputRequest request, string response)
    {
        Request = request;
        Response = response;
    }

    public RawInputRequest Request { get; }
    public string Response { get; }
    public byte[] ResponseBytes => Encoding.UTF8.GetBytes(Response);
}