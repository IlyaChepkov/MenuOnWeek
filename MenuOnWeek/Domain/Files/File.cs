using MenuOnWeek.Utils;

namespace MenuOnWeek.Domain.Files;

/// <summary>
/// Файл
/// </summary>
public sealed class File : IEntityWithId
{
    private string name;

    private File(Guid id, string name)
    {
        Id = id;
        this.name = name;
    }

    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя файла
    /// </summary>
    public string Name
    {
        get => name;
        set
        {
            ValidationException<string>.ThrowIf(x => String.IsNullOrWhiteSpace(x), value, "Название не может быть пустым");
            name = value;
        }
    }

    public static File Create(string name)
    {
        var id = Guid.NewGuid();
        return new File(id, $"{id}.{name.Split('.').Last()}");
    }
}
