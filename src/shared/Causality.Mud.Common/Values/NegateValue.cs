using System.Reflection;

namespace Causality.Mud.Common.Values;

public readonly struct NegateValue:IValue
{
    public NegateValue(IValue term, string? description=null)
    {
        Term = term;
        Description = description;
    }
    
    public IValue Term { get; }

    public int Value => -Term.Value;
    public string? Description { get; }
    public int Min => -Term.Max;
    public int Max => -Term.Min;
    public int Avg => -Term.Avg;

    public override string ToString()
    {
        return $"-{Term}";
    }
}