namespace Domain.Primitives;

public readonly record struct Result<T, E>
    where T : notnull {
    private readonly bool _isOk;
    private readonly T _value;

    public T? OptionalOk => _value;
    public E? OptionalErr { get; }

    public Result(T value) {
        ArgumentNullException.ThrowIfNull(value);
        _value = value;
        OptionalErr = default;
        _isOk = true;
    }

    public Result(E error) {
        ArgumentNullException.ThrowIfNull(error);
        _value = default!;
        OptionalErr = error;
        _isOk = false;
    }

    private void Deconstruct(out bool isSuccess, out T value, out E error) {
        isSuccess = _isOk;
        value = _isOk ? _value : default!;
        error = _isOk ? default! : OptionalErr!;
    }

    public bool IsOk() => this switch {
        (true, _, _) => true,
        _ => false,
    };

    public bool IsOk(out T value) {
        (var isOk, value, _) = this;
        return isOk;
    }

    public bool IsOk(Func<T, bool> predicate) =>
        this switch {
            (true, var value, _) => predicate(value),
            _ => false,
        };

    public bool IsErr() => !_isOk;

    public bool IsErr(out E error) {
        (var isOk, _, error) = this;
        return !isOk;
    }

    public bool IsErr(Func<E, bool> predicate) => this switch {
        (false, _, var error) => predicate(error),
        _ => false,
    };

    public T Expect(string message) => this switch {
        (true, var value, _) => value,
        _ => throw new UnwrapFailedException(message),
    };

    public E ExpectErr(string message) =>
        this switch {
            (false, _, var error) => error,
            _ => throw new UnwrapFailedException(message),
        };

    public Result<U, E> Map<U>(Func<T, U> mapFunc)
        where U : notnull =>
        this switch {
            (true, var value, _) => Factories.Ok<U, E>(mapFunc(value)),
            _ => Factories.Err<U, E>(OptionalErr!),
        };

    public Result<T, F> MapErr<F>(Func<E, F> op)
        where F : notnull =>
        this switch {
            (false, _, var error) => Factories.Err<T, F>(op(error)),
            _ => Factories.Ok<T, F>(_value),
        };

    public Result<U, E> AndThen<U>(Func<T, Result<U, E>> op)
        where U : notnull =>
        this switch {
            (true, var value, _) => op(value),
            _ => Factories.Err<U, E>(OptionalErr!),
        };

    public U MapOr<U>(U @default, Func<T, U> op) =>
        this switch {
            (true, var value, _) => op(value),
            _ => @default,
        };

    public U MapOrElse<U>(Func<E, U> @default, Func<T, U> op) =>
        this switch {
            (true, var value, _) => op(value),
            (false, _, var error) => @default(error),
        };

    public U Or<U>(U @default) => _isOk ? (U)(object)_value : @default;

    public Result<T, F> OrElse<F>(Func<E, Result<T, F>> op) =>
        this switch {
            (true, var value, _) => Factories.Ok<T, F>(value),
            (false, _, var error) => op(error),
        };
};
