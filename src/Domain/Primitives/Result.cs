﻿namespace Domain;

public class Result
{
    private const string InvalidResult = "Result must be either success or failure, but not both.";

    private Result(bool isSuccess, Error error)
    {
        if (isSuccess
            && error != Error.None
            || !isSuccess
            && error == Error.None)
        {
            throw new ArgumentException(InvalidResult,
                                        nameof(error));
        }
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}

public sealed record Error(string Code, string? Description = null)
{
    public static readonly Error None = new(string.Empty,
                                            string.Empty);
}
