using System.Diagnostics.CodeAnalysis;
using System.IO.Pipelines;

namespace Application.Abstraction;

public interface IResult<T, E>
    where T : notnull
    where E : notnull
{
    [SuppressMessage(
        "Naming",
        @"CA1716: 
         In virtual/interface member IResult<T, TError>.UnwrapOr(T), rename parameter default
         so that it no longer conflicts with the reserved language keyword 'default'.
         Using a reserved keyword as the name of a parameter on a virtual/interface 
         member makes it harder for consumers in other languages
         to override/implement the member.CA1716",
        Justification = "default is a common name for this parameter."
    )]
    T UnwrapOr(T @default);
    IResult<U, E> Bind<U>(Func<T, IResult<U, E>> op)
        where U : notnull;
    IResult<U, E> Map<U>(Func<T, U> op)
        where U : notnull;
    IResult<T, F> MapError<F>(Func<E, F> op)
        where F : notnull;
    IOption<T> Ok();
    IOption<E> Err();
}
