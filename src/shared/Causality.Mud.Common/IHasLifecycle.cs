namespace Causality.Mud.Common;

public interface IHasLifecycle:IHasState, IDisposable
{

    event Action<IHasLifecycle>? Initialized;
    event Action<IHasLifecycle>? Disposed;
    bool IsInitialized { get; }

    bool IsDisposed { get; }
    void Initialize();
    void Update(UpdateContext context);
}