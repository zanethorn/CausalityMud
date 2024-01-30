using System.Runtime.InteropServices.JavaScript;

namespace Causality.Mud.Common.Values;

public record Roll(IValue Number, IValue Sides, string? Description = null) : IBinaryValue
{
    public int Min => Number.Min;
    public int Max => Number.Max * Sides.Max;
    public int Avg => Number.Avg * (Sides.Avg / 2);

    public int Value => Operate(Left.Value, Right.Value);
    
    public override string ToString()
    {
        return $"{Number}d{Sides}";
    }
    
    object ICloneable.Clone()
    {
        return new Roll((IValue)Number.Clone(), (IValue)Sides.Clone());
    }
    
    public static IValue operator +(Roll left, IValue right) => new AddValue(left, right);
    public static IValue operator +(Roll left, int right) => new AddValue(left,  (StaticValue)right);
    public static IValue operator -(Roll left, IValue right) => new SubtractValue(left, right);
    public static IValue operator -(Roll left, int right) => new SubtractValue(left,  (StaticValue)right);
    public static IValue operator *(Roll left, IValue right) => new MultiplyValue(left, right);
    public static IValue operator *(Roll left, int right) => new MultiplyValue(left,  (StaticValue)right);
    public static IValue operator /(Roll left, IValue right) => new DivideValue(left, right);
    public static IValue operator /(Roll left, int right) => new DivideValue(left, (StaticValue)right);
    public static IValue operator -(Roll term) => new NegateValue(term);
    public static explicit operator int(Roll term) => term.Value;
    public IValue Left => Number;
    public IValue Right => Sides;
    public static int Operate(int left, int right)
    {
        var result = 0;
        for (var i = 0; i <left; i++)
        {
            result += Randomizer.Next(right);
        }

        return result;
    }
}