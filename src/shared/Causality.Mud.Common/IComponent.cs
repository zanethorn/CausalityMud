namespace Causality.Mud.Common;

public interface IComponent : IHasLifecycle
{
    event Action<IComponent>? Attached;
    event Action<IComponent>? Detached;
    
    GameObject? Parent { get; }
    bool IsAttached { get; }
    void Attach(GameObject parent);
    void Detach();

}