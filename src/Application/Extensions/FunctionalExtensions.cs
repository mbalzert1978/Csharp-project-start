namespace Application.Extensions;

public static class FunctionalExtensions
{
    public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(action);
        ArgumentNullException.ThrowIfNull(sequence);
        foreach (T? item in sequence)
        {
            action(item);
        }
    }
}
