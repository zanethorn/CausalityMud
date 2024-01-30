using Causality.Mud.Common.Values;
using FluentAssertions;
namespace Causality.Common.Tests.Values;

[TestClass]
public class ValueMathTests
{
    [TestMethod]
    public void TestAddValuesProducesAnAddedValue()
    {
        // Arrange
        var l = new StaticValue(4);
        var r = new StaticValue(2);

        // Act
        var c = l + r;

        // Assert
        c.Should().BeOfType<AddValue>();
        c.Value.Should().Be(6);
    }
    
    [TestMethod]
    public void TestSubtractValuesProducesASubtractedValue()
    {
        // Arrange
        var l = new StaticValue(4);
        var r = new StaticValue(2);

        // Act
        var c = l - r;

        // Assert
        c.Should().BeOfType<SubtractValue>();
        c.Value.Should().Be(2);
    }
    
    [TestMethod]
    public void TestMultiplyValuesProducesMultipliedValue()
    {
        // Arrange
        var l = new StaticValue(4);
        var r = new StaticValue(2);

        // Act
        var c = l * r;

        // Assert
        c.Should().BeOfType<MultiplyValue>();
        c.Value.Should().Be(8);
    }
    
    [TestMethod]
    public void TestDivideValuesProducesDividedValue()
    {
        // Arrange
        var l = new StaticValue(4);
        var r = new StaticValue(2);

        // Act
        var c = l / r;

        // Assert
        c.Should().BeOfType<DivideValue>();
        c.Value.Should().Be(2);
    }
}