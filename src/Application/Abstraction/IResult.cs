using System.Diagnostics.CodeAnalysis;
using System.IO.Pipelines;

namespace Application.Abstraction;

public interface IResult<T, TError>
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
    IResult<TOut, TError> Bind<TOut>(Func<T, IResult<TOut, TError>> op);
    IResult<TOut, TError> Map<TOut>(Func<T, TOut> op);
    IResult<T, TErrorOut> MapError<TErrorOut>(Func<TError, TErrorOut> op);
    IOption<T> Ok();
    IOption<TError> Err();
}
