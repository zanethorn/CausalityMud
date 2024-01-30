using System.Text;

namespace Causality.Mud.Common.Values.Parser;

public static class Tokenizer
{
    public static IEnumerable<Token> Tokenize(IEnumerable<char> input)
    {
        var number = new StringBuilder();
        using var enumerator = input.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var c = enumerator.Current;
            while (char.IsDigit(c))
            {
                number.Append(c);
                if (!enumerator.MoveNext())
                {
                    yield return new Token(int.Parse(number.ToString()));
                    yield break;
                }

                c = enumerator.Current;
            }

            if (number.Length > 0)
            {
                yield return new Token(int.Parse(number.ToString()));
                number.Clear();
            }

            switch (c)
            {
                case '+':
                    yield return new Token(TokenType.Plus);
                    break;
                case '-':
                    yield return new Token(TokenType.Minus);
                    break;
                case '*':
                    yield return new Token(TokenType.Multiply);
                    break;
                case '/':
                    yield return new Token(TokenType.Divide);
                    break;
                case 'd':
                    yield return new Token(TokenType.Roll);
                    break;
                case '(':
                    yield return new Token(TokenType.LeftParen);
                    break;
                case ')':
                    yield return new Token(TokenType.RightParen);
                    break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    var value = int.Parse(c.ToString());
                    yield return new Token(value);
                    break;
                default:
                    throw new InvalidDataException($"Token '{c}'was not recognized");
            }
        }
    }
}