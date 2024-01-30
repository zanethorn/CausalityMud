using System.Reflection;

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
            Initialized?.Invoke(this);
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
            Disposed?.Invoke(this);
        }
    }

    public virtual object Clone()
    {
        var state = new StateBag();
        GetState(state);
        var clone = (IHasLifecycle)Activator.CreateInstance(GetType())!;
        clone.SetState(state);
        return clone;
    }
    
    protected virtual void OnInitialize()
    {
        // does nothing
    }
    
    protected virtual void OnUpdate(UpdateContext context)
    {
        // Does nothing
    }
    
    protected virtual void OnDispose()
    {
        // does Nothing
    }
    
    
}