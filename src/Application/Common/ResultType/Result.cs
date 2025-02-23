using Application.Abstraction;
using static Application.Common.OptionType.Factories;
using static Application.Common.ResultType.Factories;

namespace Application.Common.ResultType;

public readonly record struct Result<T, TError> : IResult<T, TError>
{
    private readonly bool _isOk;
    private readonly T _value;
    private readonly TError _err;

    public Result(T value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _value = value;
        _err = default!;
        _isOk = true;
    }

    public Result(TError error)
    {
        ArgumentNullException.ThrowIfNull(error);
        _err = error;
        _value = default!;
        _isOk = false;
    }

    public IResult<TOut, TError> Bind<TOut>(Func<T, IResult<TOut, TError>> op)
    {
        ArgumentNullException.ThrowIfNull(op);
        return _isOk ? op(_value) : Err<TOut, TError>(_err);
    }

    public IOption<TError> Err() => _isOk ? None<TError>() : Some(_err);

    public IResult<TOut, TError> Map<TOut>(Func<T, TOut> op)
    {
        ArgumentNullException.ThrowIfNull(op);
        return _isOk ? Ok<TOut, TError>(op(_value)) : Err<TOut, TError>(_err);
    }

    public IResult<T, TErrorOut> MapError<TErrorOut>(Func<TError, TErrorOut> op)
    {
        ArgumentNullException.ThrowIfNull(op);
        return _isOk ? Ok<T, TErrorOut>(_value) : Err<T, TErrorOut>(op(_err));
    }

    public IOption<T> Ok() => _isOk ? Some(_value) : None<T>();

    public T UnwrapOr(T @default) => _isOk ? _value : @default;
}
