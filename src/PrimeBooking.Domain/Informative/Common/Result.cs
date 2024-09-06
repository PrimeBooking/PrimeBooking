namespace PrimeBooking.Domain.Informative.Common;

public class Result
{
    public Error? Error { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Success<TValue>(TValue value) => new (value, true, null);
    public static Result<TValue> Failure<TValue>(Error error) => new (default, false, error);
    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(
            new Error(
                ErrorCode.UnhandledRequest, 
                ErrorType.InvalidFormat, 
                "Unable to create a Result object"));
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error? error) : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue Value => IsSuccess 
        ? _value! 
        : throw new InvalidOperationException("Failed Result can't be accessed");

    public static implicit operator Result<TValue>(TValue value) => Create(value);
}