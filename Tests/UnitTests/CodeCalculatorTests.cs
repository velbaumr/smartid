using Services;

namespace Tests.UnitTests;

public class CodeCalculatorTests
{
    [Fact]
    public void CalculatesCode()
    {
        var hash = HashCreator.CreateHash("Hello World!"u8.ToArray());
        var result = CodeCalculator.CalculateCode(hash.ToArray());
        Assert.Equal("7712",  result);
    }
}