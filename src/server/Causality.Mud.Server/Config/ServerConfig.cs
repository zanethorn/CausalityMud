using System.Net;

namespace Causality.Mud.Server.Config;

public class ServerConfig
{
    public IPEndPoint ServerEndpoint { get; set; } = new (16256,1001);

    public int CleanupTimeoutMS { get; set; } = 1000;
}