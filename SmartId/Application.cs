using Services.Interfaces;

namespace SmartId;

public class Application(
    IAuthenticator authenticator,
    IRequestBuilder requestBuilder)
{
    public async Task Run()
    {
        var finished = false;
        do
        {
            var request = requestBuilder.Build();
            var verificationCode = requestBuilder.VerificationCode;
            Console.WriteLine($"Verification code: {verificationCode}");
            Console.WriteLine("Enter Estonian Id Code or Test Document number");
            var input = Console.ReadLine() ?? string.Empty;
            var result = await authenticator.Authenticate(request,
                input);

            Console.WriteLine(result.Value ?? $"Error: {result.ErrorMessage}");
            if (result.Value == "OK")
            {
                Console.WriteLine("You are successfully authenticated.");
            }
            Console.WriteLine("Do you want to continue (y to continue/any other key to quit)?");
            var condition = Console.ReadKey()
                .KeyChar;

            if (char.ToUpper(condition) == 'Y')
            {
                finished = true;
            }
        } while (finished);

        Environment.Exit(0);
    }
}