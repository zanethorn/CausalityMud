using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Causality.Mud.Common;

public abstract class Component: LifecycleObject, IComponent
{
    public event Action<IComponent>? Attached;
    public event Action<IComponent>? Detached;
    
    public GameObject? Parent { get; private set; }

    public bool IsAttached { get; private set; }
    

    public void Attach(GameObject parent)
    {
        if (!IsInitialized)
        {
            throw new InvalidOperationException("Object is not initialized");
        }
        if (IsAttached)
        {
            throw new InvalidOperationException("Object is already ready");
        }

        Parent = parent;
        OnAttach(parent);
        IsAttached = true;
    }

    public void Detach()
    {
        if (IsAttached)
        {
            OnDetach();
            Parent = null;
            IsAttached = false;
        }
    }

    protected virtual void OnAttach(GameObject parent)
    {
        Attached?.Invoke(this);
    }


    protected virtual void OnDetach()
    {
        Detached?.Invoke(this);
    }

    
}