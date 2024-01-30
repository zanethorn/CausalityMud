namespace Causality.Mud.Common.Values.Parser;

public struct Token
{
    public Token(TokenType type)
    {
        Type = type;
        Value = -1;
    }

    public Token(int value)
    {
        Type = TokenType.Number;
        Value = value;
    }

    public TokenType Type { get; }
    public int Value { get; }
}