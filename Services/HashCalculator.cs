using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace Services;

public static class HashCalculator
{
    public static byte[] CalculateHash(byte[]? data = null)
    {
        var bytes = GetRandomByteArray();
        var hash = SHA256.HashData(data ?? bytes);

        return hash;
    }
    
    private static byte[] GetRandomByteArray()
    {
        var result = RandomNumberGenerator.GetBytes(64);
        
        return result;
    }
}