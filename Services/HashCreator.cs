using System.Security.Cryptography;

namespace Services;

public static class HashCreator
{
    public static IEnumerable<byte> CreateHash(byte[]? data = null)
    {
        var hash = SHA256.HashData(data ?? GetRandomByteArray());

        return hash;
    }

    private static byte[] GetRandomByteArray()
    {
        var result = RandomNumberGenerator.GetBytes(64);

        return result;
    }
}