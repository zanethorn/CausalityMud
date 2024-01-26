namespace Causality.Mud.Common.Values;

public interface IBinaryValue:IValue
{
    IValue Left { get; }
    IValue Right { get; }
    
    static abstract int Operate(int left, int right);
    
}