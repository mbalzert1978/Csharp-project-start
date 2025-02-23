using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Application.Abstraction;
using Application.Guards;
using static Application.Common.OptionType.Factories;
using static Application.Common.ResultType.Factories;

namespace Application.Common.ResultType;

public readonly record struct Result<T, E> : IResult<T, E>
    where T : notnull
    where E : notnull
{
    private readonly bool _isOk;
    private readonly T _content;
    private readonly E _err;

    internal Result(T content)
    {
        _content = Ensure.NotNull(content);
        _err = default!;
        _isOk = true;
    }

    internal Result(E error)
    {
        _err = Ensure.NotNull(error);
        _content = default!;
        _isOk = false;
    }

    public T UnwrapOr([DisallowNull] T @default) =>
        this switch
        {
            { _isOk: true, _content: var content } => content,
            { _isOk: false } => @default,
        };

    public IResult<U, E> Bind<U>(Func<T, IResult<U, E>> op)
        where U : notnull =>
        this switch
        {
            { _isOk: true, _content: var content } => Ensure.NotNull(op)(content),
            { _isOk: false, _err: var err } => Err<U, E>(err),
        };

    public IResult<U, E> Map<U>(Func<T, U> op)
        where U : notnull =>
        this switch
        {
            { _isOk: true, _content: var content } => Ok<U, E>(Ensure.NotNull(op)(content)),
            { _isOk: false, _err: var err } => Err<U, E>(err),
        };

    public IResult<T, F> MapError<F>(Func<E, F> op)
        where F : notnull =>
        this switch
        {
            { _isOk: true, _content: var content } => Ok<T, F>(content),
            { _isOk: false, _err: var err } => Err<T, F>(Ensure.NotNull(op)(err)),
        };

    public IOption<T> Ok() =>
        this switch
        {
            { _isOk: true, _content: var content } => Some(content),
            { _isOk: false } => None<T>(),
        };

    public IOption<E> Err() =>
        this switch
        {
            { _isOk: false, _err: var err } => Some(Ensure.NotNull(err)),
            { _isOk: true } => None<E>(),
        };
}
