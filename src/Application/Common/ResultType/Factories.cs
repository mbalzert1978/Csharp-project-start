using Application.Abstraction;
using Application.Common.OptionType;

namespace Application.Common.ResultType;

public static class Factories
{
    public static IResult<T, TError> Ok<T, TError>(T content) => new Result<T, TError>(content);

    public static IResult<T, TError> Err<T, TError>(TError error) => new Result<T, TError>(error);
}
