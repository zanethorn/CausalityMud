namespace Causality.Mud.Common;

public abstract class StatefulObject:IHasState
{
    public event Action<IHasState>? Reseted;

    public void Reset()
    {
        SetState(new StateBag());
        OnReset();
    }

    public void SetState(StateBag state)
    {
        OnSetState(state);
    }

    public void GetState(StateBag state)
    {
        OnGetState(state);
    }
    
    protected virtual void OnReset()
    {
        Reseted?.Invoke(this);
    }
    
    protected virtual void OnSetState(StateBag state)
    {
        
    }

    protected virtual void OnGetState(StateBag state)
    {
        
    }
}