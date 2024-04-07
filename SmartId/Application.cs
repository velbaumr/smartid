using Services.Interfaces;

namespace SmartId;

public class Application(
    IAuthenticator authenticator,
    IRequestBuilder requestBuilder)
{
    public async Task Run()
    {
        var finished = false;
        
        while (!finished)
        {
            var request = requestBuilder.Build();
            var verificationCode = requestBuilder.VerificationCode;
            
            Console.WriteLine($"Verification code: {verificationCode}");
            Console.WriteLine("Enter Estonian ID code or test document number");
            Console.WriteLine();
            var input = Console.ReadLine() ?? string.Empty;
            var result = await authenticator.Authenticate(request,
                PrepareCode(input));
            Console.WriteLine();
            Console.WriteLine(result.Value ?? $"Error: {result.ErrorMessage}");
            Console.WriteLine();
            if (result is { ErrorMessage: not null, HasErrors: true } && result.ErrorMessage.Contains("404"))
            {
                Console.WriteLine("Smart-Id account doesn't exists for entered value.");
            }
            Console.WriteLine();
            Console.WriteLine(result.Value == "OK" ? "You are successfully authenticated." : "Authentication failed.");
            Console.WriteLine();
            Console.WriteLine("Do you want to continue? (y to continue/any other key to quit)");
            var condition = Console.ReadKey()
                .KeyChar;

            if (char.ToUpper(condition) != 'Y')
            {
                finished = true;
            }
            
            Console.WriteLine();
        }

        Environment.Exit(0);
    }

    private static string PrepareCode(string code)
    {
        return code.ToUpper().EndsWith("-MOCK-Q") ? code : $"PNOEE-{code}";
    }
}