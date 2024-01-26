namespace Causality.Mud.Common;

public static class Randomizer
{
    public static int Next(int sides)
    {
        return Random.Shared.Next(sides);
    }
}