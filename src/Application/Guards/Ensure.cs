using System.Diagnostics.CodeAnalysis;

namespace Application.Guards;

public static class Ensure
{
    private const string STRING_EMPTY_WHITESPACE = "Value cannot be empty or whitespace.";

    public static T NotNull<T>([NotNull] T? value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value;
    }

    public static string NotEmptyOrWhiteSpace([NotNull] string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException(STRING_EMPTY_WHITESPACE);
    }
}
