namespace Services;

public static class CodeCalculator
{
    public static string CalculateCode(byte[] hash)
    {
        var toConvert = hash[^2..];
        var intVal = toConvert[0] * 0x0100 + toConvert[1];
        var digits = intVal % 10000;
        return $"{digits:0000}";
    }
}