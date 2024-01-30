using Causality.Mud.Common.Values.Parser;
using FluentAssertions;

namespace Causality.Common.Tests.Values.Parser;

[TestClass]
public class TokenizerTests
{
    [TestMethod]
    [DataRow("+", TokenType.Plus)]
    [DataRow("-", TokenType.Minus)]
    [DataRow("*", TokenType.Multiply)]
    [DataRow("/", TokenType.Divide)]
    [DataRow("d", TokenType.Roll)]
    public void Tokenizer_Should_Return_Proper_Token_Type(string input, TokenType type)
    {
        // Arrange
        
        
        // Act
        var tokenString = Tokenizer.Tokenize(input).ToArray();

        // Assert
        tokenString.Length.Should().Be(1);
        var token = tokenString[0];
        token.Type.Should().Be(type);
        token.Value.Should().Be(-1);
    }
    
    [TestMethod]
    [DataRow(42)]
    [DataRow(8)]
    [DataRow(123456)]
    public void Tokenizer_Should_Parse_Numbers_Properly(int number)
    {
        // Arrange
        
        
        // Act
        var tokenString = Tokenizer.Tokenize(number.ToString()).ToArray();

        // Assert
        tokenString.Length.Should().Be(1);
        var token = tokenString[0];
        token.Type.Should().Be(TokenType.Number);
        token.Value.Should().Be(number);
    }

    [TestMethod]
    [DataRow("+", TokenType.Plus)]
    [DataRow("-", TokenType.Minus)]
    [DataRow("*", TokenType.Multiply)]
    [DataRow("/", TokenType.Divide)]
    [DataRow("d", TokenType.Roll)]
    public void Tokenizer_Should_Chain_Operators_Correctly(string op, TokenType type)
    {
        // Arrange
        var input = $"4{op}2";
        
        // Act
        var tokenString = Tokenizer.Tokenize(input).ToArray();
        // Assert
        tokenString.Length.Should().Be(3);
        tokenString[0].Type.Should().Be(TokenType.Number);
        tokenString[0].Value.Should().Be(4);
        tokenString[1].Type.Should().Be(type);
        tokenString[2].Type.Should().Be(TokenType.Number);
        tokenString[2].Value.Should().Be(2);
    }
}