namespace Core.Domain.Common;

public static class ResultExtensions
{
    public static Result<T> Ensure<T>(
        this Result<T> result,
        Func<T, bool> predicate,
        Error error
    ) =>
        result.IsFailure
            ? result
            : predicate(result.Value!)
                ? result
                : Result.Failure<T>(error);

    public static Result<TOut> Map<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> mappingFunc
    ) =>
        result.IsSuccess ?
            Result.Success(mappingFunc(result.Value!)) :
            Result.Failure<TOut>(result.Error!);

    public static async Task<Result> Bind<TIn>(
        this Result<TIn> result,
        Func<TIn, Task<Result>> func
    ) =>
        result.IsFailure
            ? Result.Failure(result.Error!)
            : await func(result.Value!);

    public static async Task<Result<TOut>> Bind<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, Task<Result<TOut>>> func
    ) =>
        result.IsFailure
            ? Result.Failure<TOut>(result.Error!)
            : await func(result.Value!);
}
