using System.Runtime.InteropServices.JavaScript;

namespace Causality.Mud.Common.Values;

public readonly struct Roll:IValue
{
    public Roll(IValue number, IValue sides, string? description=null)
    {
        Number = number;
        Sides = sides;
        Description = description;
    }
    
    public IValue Number { get; }
    
    public IValue Sides { get; }
    public string? Description { get; }
    public int Min => Number.Min;
    public int Max => Number.Max * Sides.Max;
    public int Avg => Number.Avg * (Sides.Avg / 2);

    public int Value
    {
        get
        {
            var result = 0;
            var n = Number.Value;
            var s = Sides.Value;
            for (var i = 0; i <n; i++)
            {
                result += Randomizer.Next(s);
            }

            return result;
        }
    }
    
    public override string ToString()
    {
        return $"{Number}d{Sides}";
    }
}