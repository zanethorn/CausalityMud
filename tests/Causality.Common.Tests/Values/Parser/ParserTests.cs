using Causality.Mud.Common.Values;
using Causality.Mud.Common.Values.Parser;
using FluentAssertions;

namespace Causality.Common.Tests.Values.Parser;

[TestClass]
public class ParserTests
{
    [TestMethod]
    [DataRow("+", 6)]
    [DataRow("-", 2)]
    [DataRow("*", 8)]
    [DataRow("/", 2)]
    public void Parser_Should_Parse_Basic_Math_Correctly(string symbol, int result)
    {
        // Arrange
        var input = $"4{symbol}2";
        
        // Act
        var output = ValueParser.Parse(input);
        
        // Assert
        output.Value.Should().Be(result);
    }

    [TestMethod]
    [DataRow("d4", 1,4)]
    [DataRow("3d6", 3,6)]
    [DataRow("2d8", 2,8)]
    [DataRow("12d20", 12,20)]
    public void Parser_Should_Construct_Die_Roll_Correctly(string input, int number, int sides)
    {
        // Act
        var output = (IBinaryValue)ValueParser.Parse(input);
        
        // Assert
        output.Left.Value.Should().Be(number);
        output.Right.Value.Should().Be(sides);
    }
}