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
            
            Console.WriteLine($"Verification Code: {verificationCode}");
            Console.WriteLine("Enter Estonian Id Code or Test Document number");
            Console.WriteLine();
            var input = Console.ReadLine() ?? string.Empty;
            var result = await authenticator.Authenticate(request,
                PrepareCode(input));
            Console.WriteLine();
            Console.WriteLine(result.Value ?? $"Error: {result.ErrorMessage}");
            Console.WriteLine();
            if (result is { ErrorMessage: not null, HasErrors: true } && result.ErrorMessage.Contains("404"))
            {
                Console.WriteLine("Smart-Id Account Doesn't Exists For Entered Value.");
            }
            Console.WriteLine();
            Console.WriteLine(result.Value == "OK" ? "You Are Successfully Authenticated." : "Authentication Failed.");
            Console.WriteLine();
            Console.WriteLine("Do You Want to Continue? (y to Continue/Any Other Key to Quit)");
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