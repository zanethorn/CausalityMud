namespace Causality.Mud.Common.Values;

public readonly struct MultiplyValue:IBinaryValue
{
    public MultiplyValue(IValue left, IValue right, string? description=null)
    {
        Left = left;
        Right = right;
        Description = description;
    }
    
    public string? Description { get; }
    public int Min => Operate(Left.Min, Right.Min);
    public int Max => Operate(Left.Max, Right.Max);
    public int Avg => Operate(Left.Avg, Right.Avg);
    public int Value => Operate(Left.Value, Right.Value);
    public IValue Left { get; }
    public IValue Right { get; }
    
    public override string ToString()
    {
        return $"({Left}*{Right})";
    }
    
    public static int Operate(int left, int right)
    {
        return left * right;
    }
}