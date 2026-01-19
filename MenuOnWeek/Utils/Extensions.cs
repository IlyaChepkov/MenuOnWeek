namespace Utils;

/// <summary>
/// Общее расширение
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Извлекает значение из нулябельного типа
    /// </summary>
    public static T Required<T>(this T? value, string? name = default)
        where T : class
    {
        return value ?? throw new ArgumentNullException(name);
    }

    /// <summary>
    /// Извлекает значение из нулябельного типа
    /// </summary>
    public static T Required<T>(this T? value, string? name = default)
        where T : struct
    {
        return value ?? throw new ArgumentNullException(name);
    }
}
