using Application.Abstraction;

namespace Application.Common.OptionType;

public static class Factories
{
    public static IOption<T> Some<T>(T content) => new Option<T>(content, true);

    public static IOption<T> None<T>() => new Option<T>(default!, false);
}
