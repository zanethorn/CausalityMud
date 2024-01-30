using System.Reflection;

namespace Causality.Mud.Common.Values;

public record NegateValue(IValue Term, string? Description = null) : IValue
{
    public int Value => -Term.Value;
    public int Min => -Term.Max;
    public int Max => -Term.Min;
    public int Avg => -Term.Avg;

    public override string ToString()
    {
        return $"-{Term}";
    }
    
    object ICloneable.Clone()
    {
        return new NegateValue((IValue)Term.Clone());
    }
    
    public static IValue operator +(NegateValue left, IValue right) => new AddValue(left, right);
    public static IValue operator +(NegateValue left, int right) => new AddValue(left,  (StaticValue)right);
    public static IValue operator -(NegateValue left, IValue right) => new SubtractValue(left, right);
    public static IValue operator -(NegateValue left, int right) => new SubtractValue(left,  (StaticValue)right);
    public static IValue operator *(NegateValue left, IValue right) => new MultiplyValue(left, right);
    public static IValue operator *(NegateValue left, int right) => new MultiplyValue(left,  (StaticValue)right);
    public static IValue operator /(NegateValue left, IValue right) => new DivideValue(left, right);
    public static IValue operator /(NegateValue left, int right) => new DivideValue(left, (StaticValue)right);
    public static IValue operator -(NegateValue term) => new NegateValue(term);
    public static explicit operator int(NegateValue term) => term.Value;
}