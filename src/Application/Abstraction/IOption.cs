using System.Diagnostics.CodeAnalysis;
using Application.Common.OptionType;

namespace Application.Abstraction;

public interface IOption<T>
{
    [SuppressMessage(
        "Naming",
        @"CA1716: 
         In virtual/interface member IOption<T>.UnwrapOr(T), rename parameter default
         so that it no longer conflicts with the reserved language keyword 'default'.
         Using a reserved keyword as the name of a parameter on a virtual/interface 
         member makes it harder for consumers in other languages
         to override/implement the member.CA1716",
        Justification = "default is a common name for this parameter."
    )]
    T UnwrapOr(T @default);
    T? UnwrapOrNull();
    IOption<TBound> Bind<TBound>(Func<T, IOption<TBound>> op);
    IOption<TOut> Map<TOut>(Func<T, TOut> op);
}
