using Services;

namespace Tests.UnitTests;

public class HashCreatorTests : TestBase
{
    [Fact]
    public void CalculatesHash()
    {
        var result = HashCreator.CreateHash("hello world!"u8.ToArray());

        Assert.Equal("7509e5bda0c762d2bac7f90d758b5b2263fa01ccbc542ab5e3df163be08e6ca9",
            string.Concat(result.Select(b => b.ToString("x2"))));
    }
}