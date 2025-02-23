using Application.Abstraction;

namespace Application.Common.OptionType;

public static class Factories
{
    public static IOption<T> Some<T>(T content)
        where T : notnull => new Option<T>(content, true);

    public static IOption<T> None<T>()
        where T : notnull => new Option<T>(default!, false);
}
