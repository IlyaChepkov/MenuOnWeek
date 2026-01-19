namespace MenuOnWeek.Application.Files;

/// <summary>
/// Сервис для работы с файлами
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Создает файл
    /// </summary>
    public Task<Guid> Add(string name, Stream fileContent, CancellationToken token);

    /// <summary>
    /// Возвращает файл по идентификатору
    /// </summary>
    public Task<FileWithName?> GetById(Guid id, CancellationToken token);
}
