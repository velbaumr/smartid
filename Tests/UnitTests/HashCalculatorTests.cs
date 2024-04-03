using System.Text;
using Services;

namespace Tests.UnitTests;

public class HashCalculatorTests : TestBase
{
    [Fact]
    public void CalculatesHash()
    {
        var calculator = new HashCalculator(_configuration);
        var result = calculator.CalculateHash(Encoding.UTF8.GetBytes("hello world!"));

        Assert.Equal("7f83b1657ff1fc53b92dc18148a1d65dfc2d4b1fa3d677284addd200126d9069",
            Encoding.UTF8.GetString(result));
    }
}