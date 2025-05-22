namespace Utils;

public static class Extensions
{
    public static T Required<T>(this T? value, string? name = default)
        where T : class
    {
        return value ?? throw new ArgumentNullException(name);
    }
}
