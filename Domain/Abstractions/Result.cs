using System.Runtime.InteropServices.JavaScript;

namespace Domain.Abstractions;

public class Result<T>
{
    private Result(T value)
    {
        Value = value;
        Error = null;
    }
    private Result(Error error)
    {
        Error = error;
        Value = default;
    }

    private Result()
    {
        Error = null!;
        Value = default;
    }

    public T? Value { get; }
    public Error Error { get; }
    public bool IsSuccess => Error == null;
    
    public static Result<T> Success(T value) => new Result<T>(value);
    public static Result<T> Success() => new Result<T>();
    public static Result<T> Failure(Error error) => new Result<T>(error);
    
    public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value) : onFailure(Error);
    }
}