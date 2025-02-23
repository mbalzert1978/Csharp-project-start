using System.Diagnostics.CodeAnalysis;
using Application.Abstraction;
using static Application.Common.OptionType.Factories;

namespace Application.Common.OptionType;

[SuppressMessage(
    "Naming",
    @"CA1716:
     Rename type Option<T> so that it no longer conflicts with the reserved
     language keyword 'Option'. Using a reserved keyword as the name of a type makes
     it harder for consumers in other languages to use the type.",
    Justification = "Option is a common name for this type of monad."
)]
public readonly record struct Option<T> : IOption<T>
{
    private readonly T _content;
    private readonly bool _hasValue;

    internal Option(T content, bool hasValue)
    {
        (_content, _hasValue) = (content, hasValue);
    }

    public IOption<TBound> Bind<TBound>(Func<T, IOption<TBound>> op)
    {
        ArgumentNullException.ThrowIfNull(op);
        return _hasValue ? op(_content) : None<TBound>();
    }

    public IOption<TOut> Map<TOut>(Func<T, TOut> op)
    {
        ArgumentNullException.ThrowIfNull(op);
        return _hasValue ? Some(op(_content)) : None<TOut>();
    }

    public T UnwrapOr(T @default) => _hasValue ? _content : @default;

    public T? UnwrapOrNull() => _content;
}
