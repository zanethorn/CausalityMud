namespace Causality.Mud.Common.Values;

public record DivideValue(IValue Left, IValue Right, string? Description = null) : IBinaryValue
{
    public int Min => Operate(Left.Min, Right.Min);
    public int Max => Operate(Left.Max, Right.Max);
    public int Avg => Operate(Left.Avg, Right.Avg);
    public int Value => Operate(Left.Value, Right.Value);

    public override string ToString()
    {
        return $"({Left}/{Right})";
    }
    public static int Operate(int left, int right)
    {
        return left / right;
    }
    
    object ICloneable.Clone()
    {
        return new DivideValue((IValue)Left.Clone(), (IValue)Right.Clone());
    }
    
    public static IValue operator +(DivideValue left, IValue right) => new AddValue(left, right);
    public static IValue operator +(DivideValue left, int right) => new AddValue(left,  (StaticValue)right);
    public static IValue operator -(DivideValue left, IValue right) => new SubtractValue(left, right);
    public static IValue operator -(DivideValue left, int right) => new SubtractValue(left,  (StaticValue)right);
    public static IValue operator *(DivideValue left, IValue right) => new MultiplyValue(left, right);
    public static IValue operator *(DivideValue left, int right) => new MultiplyValue(left,  (StaticValue)right);
    public static IValue operator /(DivideValue left, IValue right) => new DivideValue(left, right);
    public static IValue operator /(DivideValue left, int right) => new DivideValue(left, (StaticValue)right);
    public static IValue operator -(DivideValue term) => new NegateValue(term);
    public static explicit operator int(DivideValue term) => term.Value;
}