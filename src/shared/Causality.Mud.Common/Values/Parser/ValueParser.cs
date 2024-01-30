using System.Reflection;

namespace Causality.Mud.Common.Values.Parser;

public static class ValueParser
{
    public static IValue Parse(string input)
    {
        return ParseTokens(Tokenizer.Tokenize(input));
    }
    
    public static IValue ParseTokens(IEnumerable<Token> tokens)
    {
        var rawValue= ProcessEnumerator( tokens.GetEnumerator());
        if (rawValue == null)
        {
            throw new Exception("Nothing could be parsed");
        }

        return rawValue;
    }

    private static IValue? ProcessEnumerator(IEnumerator<Token> enumerator)
    {
        var op = TokenType.Unknown;
        IValue? currentValue = null;
        while (enumerator.MoveNext())
        {
            var currentToken = enumerator.Current;
            switch (currentToken.Type)
            {
                case TokenType.Number:
                    var value = new StaticValue(currentToken.Value);
                    if (op == TokenType.Unknown)
                    {
                        currentValue = value;
                    }
                    else
                    {
                        if (currentValue == null)
                        {
                            throw new ParserException("Operator with no left clause");
                        }
                        currentValue = MakeValue(currentValue, value, op);
                        op = TokenType.Unknown;
                    }
                    break;
                case TokenType.Roll:
                case TokenType.Minus:
                case TokenType.Plus:
                case TokenType.Multiply:
                case TokenType.Divide:
                    if (op != TokenType.Unknown)
                    {
                        throw new ParserException("Syntax makes no sense");
                    }
                    if (currentValue == null)
                    {
                        if (currentToken.Type == TokenType.Roll)
                        {
                            currentValue = new StaticValue(1);
                        }
                        else
                        {
                            throw new ParserException("Operator with no left clause");
                        }
                    }
                    op = currentToken.Type;
                    break;
                case TokenType.LeftParen:
                    currentValue = ProcessEnumerator(enumerator);
                    break;
                case TokenType.RightParen:
                    return currentValue;
                default:
                    throw new ParserException("Unknown token Type");
            }
        }

        return currentValue;
    }

    public static IValue MakeValue(IValue left, IValue right, TokenType op)
    {
        switch (op)
        {
            case TokenType.Plus:
                return new AddValue(left, right);
            case TokenType.Minus:
                return new SubtractValue(left, right);
            case TokenType.Multiply:
                return new MultiplyValue(left, right);
            case TokenType.Divide:
                return new DivideValue(left, right);
            case TokenType.Roll:
                return new Roll(left, right);
            default:
                throw new ParserException("Token Type is not binary");
        }
    }

}