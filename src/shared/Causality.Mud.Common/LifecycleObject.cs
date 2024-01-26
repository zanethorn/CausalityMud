namespace Causality.Mud.Common;

public abstract class LifecycleObject:StatefulObject, IHasLifecycle
{
    public event Action<IHasLifecycle>? Initialized;
    public event Action<IHasLifecycle>? Disposed;
    
    public bool IsInitialized { get; private set; }
    public bool IsDisposed { get; private set; }
    
    public void Initialize()
    {
        if (!IsInitialized)
        {
            OnInitialize();
            IsInitialized = true;
        }
    }
    
    public void Update(UpdateContext context)
    {
        OnUpdate(context);
    }
    
    public void Dispose()
    {
        if (!IsDisposed)
        {
            IsDisposed = true;
            OnDispose();
            IsInitialized = false;
        }
    }
    
    protected virtual void OnInitialize()
    {
        Initialized?.Invoke(this);
    }
    
    protected virtual void OnUpdate(UpdateContext context)
    {
        // Does nothing
    }
    
    protected virtual void OnDispose()
    {
        Disposed?.Invoke(this);
    }
}