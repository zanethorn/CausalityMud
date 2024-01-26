namespace Causality.Mud.Common.Values;

public readonly struct StaticValue:IValue
{
    public StaticValue(int value, string? description=null)
    {
        Value = value;
        Description = description;
    }
    
    public int Value { get; }
    public string? Description { get; }
    public int Min => Value;
    public int Max => Value;
    public int Avg => Value;

    public override string ToString()
    {
        return Value.ToString();
    }

}