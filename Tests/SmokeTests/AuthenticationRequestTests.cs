using Services;
using Services.Dtos;
using static RestAssured.Dsl;

namespace Tests.SmokeTests;

public class AuthenticationRequestTests: TestBase
{
    private readonly string _baseUrl;
    private readonly AuthenticationRequest _request;
    
    public AuthenticationRequestTests()
    {
        var settings = _configuration.GetSection("smartId");

        _baseUrl = settings["baseUrl"] ?? throw new InvalidOperationException("Can't read url value from settings");
        var builder = new RequestBuilder(_configuration);
        _request = builder.Build();
    }

    [Fact]
    public void ConnectsToSmartId()
    {
        var authUrl = $"{_baseUrl}authentication/document/PNOEE-30303039914-MOCK-Q";
        
        var  responseBodyAsString = Given()
            .Body(_request)
            .Header("Accept", "*/*")
            .ContentType("application/json")
            .When()
            .Post(authUrl)
            .Then()
            .Extract().Body();
        
        Assert.NotEmpty(responseBodyAsString);
    }
 }