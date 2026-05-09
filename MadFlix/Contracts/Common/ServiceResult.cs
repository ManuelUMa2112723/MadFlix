namespace MadFlix.Contracts.Common;

public class ServiceResult
{
    protected ServiceResult(bool succeeded, string? errorMessage)
    {
        Succeeded = succeeded;
        ErrorMessage = errorMessage;
    }

    public bool Succeeded { get; }
    public string? ErrorMessage { get; }

    public static ServiceResult Success()
    {
        return new(true, null);
    }

    public static ServiceResult Failure(string errorMessage)
    {
        return new(false, errorMessage);
    }
}

public sealed class ServiceResult<T> : ServiceResult
{
    private ServiceResult(bool succeeded, T? value, string? errorMessage)
        : base(succeeded, errorMessage)
    {
        Value = value;
    }

    public T? Value { get; }

    public static ServiceResult<T> Success(T value)
    {
        return new(true, value, null);
    }

    public new static ServiceResult<T> Failure(string errorMessage)
    {
        return new(false, default, errorMessage);
    }
}
