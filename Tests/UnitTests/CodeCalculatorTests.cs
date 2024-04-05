using Services;

namespace Tests.UnitTests;

public class CodeCalculatorTests
{
    [Fact]
    public void CalculatesCode()
    {
        var bytes = Convert.FromBase64String(
            "2afAxT+nH5qNYrfM+D7F6cKAaCKLLA23pj8ro3SksqwsdwmC3xTndKJotewzu7HlDy/DiqgkR+HXBiA0sW1x0Q==");
        var hash = HashCreator.CreateHash(bytes);
        var result = CodeCalculator.CalculateCode(hash.ToArray());
        Assert.Equal("3676", result);
    }
}