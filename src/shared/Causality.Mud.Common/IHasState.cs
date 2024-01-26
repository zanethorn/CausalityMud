namespace Causality.Mud.Common;

public interface IHasState
{
    event Action<IHasState>? Reseted;

    void Reset();

    void SetState(StateBag state);

    void GetState(StateBag state);

}