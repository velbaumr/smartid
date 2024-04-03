using Microsoft.Extensions.Configuration;

namespace Services;

public class HashCalculator(IConfiguration configuration)
{
    public byte[] CalculateHash(byte[] data)
    {
        throw new NotImplementedException();
    }
}