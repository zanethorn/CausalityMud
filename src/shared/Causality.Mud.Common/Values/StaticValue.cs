namespace Causality.Mud.Common.Values;

public record StaticValue(int Value, string? Description = null) : IValue
{
    public int Min => Value;
    public int Max => Value;
    public int Avg => Value;

    public override string ToString()
    {
        return Value.ToString();
    }
    
    object ICloneable.Clone()
    {
        return new StaticValue(Value);
    }

    public static IValue operator +(StaticValue left, IValue right) => new AddValue(left, right);
    public static IValue operator +(StaticValue left, int right) => new AddValue(left,  (StaticValue)right);
    public static IValue operator -(StaticValue left, IValue right) => new SubtractValue(left, right);
    public static IValue operator -(StaticValue left, int right) => new SubtractValue(left,  (StaticValue)right);
    public static IValue operator *(StaticValue left, IValue right) => new MultiplyValue(left, right);
    public static IValue operator *(StaticValue left, int right) => new MultiplyValue(left,  (StaticValue)right);
    public static IValue operator /(StaticValue left, IValue right) => new DivideValue(left, right);
    public static IValue operator /(StaticValue left, int right) => new DivideValue(left, (StaticValue)right);
    public static IValue operator -(StaticValue term) => new NegateValue(term);
    public static explicit operator int(StaticValue term) => term.Value;
    public static explicit operator StaticValue(int value) => new (value);


}