using File = MenuOnWeek.Domain.Files.File;

namespace MenuOnWeek.Application.Files;

/// <summary>
/// Файловый репозиторий
/// </summary>
public interface IFileRepository
{
    /// <summary>
    /// Добавляет файл в хранилище
    /// Возвращает имя добавленного файла
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Task Add(File file, Stream stream, CancellationToken token);

    /// <summary>
    /// Возвращает файл по идентификатору
    /// </summary>
    public Task<FileWithName?> GetById(Guid id, CancellationToken token);
}
