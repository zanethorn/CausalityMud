namespace Causality.Mud.Common;

public class StateBag
{
    private readonly Dictionary<string, object> _state = new();

    public T Get<T>(string name)
    {
        if (_state.TryGetValue(name, out var value))
        {
            if (value is T result)
            {
                return result;
            }
        }

        return default;
    }

    public void Set(string name, object value)
    {
        _state[name] = value;
    }

    public object this[string name]
    {
        get
        {
            _state.TryGetValue(name, out var value);
            return value;
        }
        set { Set(name, value); }
    }
}