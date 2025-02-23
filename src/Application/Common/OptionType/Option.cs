using System.Diagnostics.CodeAnalysis;
using Application.Abstraction;
using Application.Guards;
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
    where T : notnull
{
    private readonly T _content;
    private readonly bool _hasValue;

    internal Option(T content, bool hasValue)
    {
        (_content, _hasValue) = (content, hasValue);
    }

    public IOption<U> Bind<U>(Func<T, IOption<U>> op)
        where U : notnull =>
        this switch
        {
            { _hasValue: true, _content: var content } => Ensure.NotNull(op)(content),
            { _hasValue: false } => None<U>(),
        };

    public IOption<U> Map<U>(Func<T, U> op)
        where U : notnull =>
        this switch
        {
            { _hasValue: true, _content: var content } => Some(Ensure.NotNull(op)(content)),
            { _hasValue: false } => None<U>(),
        };

    public T UnwrapOr(T @default) =>
        this switch
        {
            { _hasValue: true, _content: var content } => content,
            { _hasValue: false } => @default,
        };

    public T? UnwrapOrNull() => _content;
}
