namespace Domain.Primitives;

public sealed record ErrorType(string Code, string? Description = null)
{
    public static readonly ErrorType None = new(string.Empty, string.Empty);
}
