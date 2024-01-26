namespace Causality.Mud.Common;

public interface IConnectionHandler:IDisposable
{
    Task RunAsync(CancellationToken systemStopToken);
    bool Connected { get; }
}