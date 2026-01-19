using System.Runtime.CompilerServices;

namespace MenuOnWeek.Utils;

public abstract class ValidationException : Exception
{
    protected ValidationException(string message) : base(message) { }
}

/// <summary>
///  Исключение, выкидываемое если сущность не валидна
/// </summary>
public sealed class ValidationException<T> : ValidationException
{
    private ValidationException(string message) : base($"Сущность {typeof(T).Name} не вадидна. {message}") { }

    /// <summary>
    /// Бросает исключение ValidationException если переданное значение null
    /// </summary>
    public static void ThrowIfNull(T? value)
    {
        if (value is null)
        {
            throw new ValidationException<T?>("Значение было null");
        }
    }

    /// <summary>
    /// Бросает исключение ValidationException если переданное значение соответствует переданному предикату
    /// </summary>
    public static void ThrowIf(Func<T, bool> predicate, T value, string message)
    {
        if (predicate(value))
        {
            throw new ValidationException<T>(message);
        }
    }
}
