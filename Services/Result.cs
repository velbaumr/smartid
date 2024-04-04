namespace Services;

public class Result<T>
{
    public T? Value { get; init; }
    public string? ErrorMessage { get; init; }

    public bool HasErrors => !string.IsNullOrEmpty(ErrorMessage);
}