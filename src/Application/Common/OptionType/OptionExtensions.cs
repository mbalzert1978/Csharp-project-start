using Application.Abstraction;
using Application.Common.OptionType;
using Application.Guards;
using static Application.Common.OptionType.Factories;

namespace Application.Common.OptionType;

public static class OptionExtensions
{
    public static IOption<T> FirstOrNone<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        where T : notnull => items.Where(predicate).Select(Some).DefaultIfEmpty(None<T>()).First();

    public static IOption<U> Select<T, U>(this IOption<T> obj, Func<T, U> map)
        where T : notnull
        where U : notnull => Ensure.NotNull(obj).Map(Ensure.NotNull(map));

    public static IOption<T> Where<T>(this IOption<T> obj, Func<T, bool> predicate)
        where T : notnull =>
        Ensure.NotNull(obj).Bind(content => predicate(content) ? obj : None<T>());

    public static IOption<R> SelectMany<T, U, R>(
        this IOption<T> obj,
        Func<T, IOption<U>> bind,
        Func<T, U, R> map
    )
        where T : notnull
        where U : notnull
        where R : notnull =>
        Ensure.NotNull(obj).Bind(original => bind(original).Map(result => map(original, result)));
}
