namespace Causality.Mud.Common;

public sealed class GameObject:Component
{
    private readonly ICollection<IComponent> _components = new List<IComponent>();

    public event Action<GameObject, IComponent>? AttachedComponent;
    public event Action<GameObject, IComponent>? DetachedComponent;
    
    public string Id { get; init; }
    
    public IEnumerable<IComponent> Components => _components;
    
    public void AttachComponent<T>()
        where T : IComponent, new()
    {
        var component = new T();
        component.Initialize();
        _components.Add(component);
        component.Attach(this);
        AttachedComponent?.Invoke(this,component);
    }
    

    public void DetachComponent<T>()
        where T : IComponent, new()
    {
        var component = GetComponent<T>();
        if (component == null)
        {
            throw new InvalidOperationException("Component has not been registered");
        }

        component.Detach();
        _components.Remove(component);
        DetachedComponent?.Invoke(this, component);
    }

    public bool HasComponent<T>()
        where T : IComponent
    {
        return _components.OfType<T>().Any();
    }

    public T? GetComponent<T>()
        where T : IComponent, new()
    {
        return _components.OfType<T>().FirstOrDefault();
    }

    protected override void OnUpdate(UpdateContext context)
    {
        base.OnUpdate(context);
        foreach (var c in Components)
        {
            c.Update(context);
        }
        
    }

}