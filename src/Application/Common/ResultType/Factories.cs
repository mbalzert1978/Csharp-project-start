using Application.Abstraction;
using Application.Common.OptionType;

namespace Application.Common.ResultType;

public static class Factories
{
    public static IResult<T, E> Ok<T, E>(T content)
        where T : notnull
        where E : notnull => new Result<T, E>(content);

    public static IResult<T, E> Err<T, E>(E error)
        where T : notnull
        where E : notnull => new Result<T, E>(error);
}
