using Application.Abstraction;
using Application.Common.OptionType;
using static Application.Common.OptionType.Factories;

namespace Application.Common.OptionType;

public static class OptionExtensions
{
    public static IOption<T> FirstOrNone<T>(this IEnumerable<T> items, Func<T, bool> predicate) =>
        items.Where(predicate).Select(Some).DefaultIfEmpty(None<T>()).First();

    public static IOption<TOut> Select<T, TOut>(this IOption<T> obj, Func<T, TOut> map)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentNullException.ThrowIfNull(map);
        return obj.Map(map);
    }

    public static IOption<T> Where<T>(this IOption<T> obj, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentNullException.ThrowIfNull(predicate);
        return obj.Bind(content => predicate(content) ? obj : None<T>());
    }

    public static IOption<TResult> SelectMany<T, TOut, TResult>(
        this IOption<T> obj,
        Func<T, IOption<TOut>> bind,
        Func<T, TOut, TResult> map
    )
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentNullException.ThrowIfNull(bind);
        ArgumentNullException.ThrowIfNull(map);
        return obj.Bind(original => bind(original).Map(result => map(original, result)));
    }
}
