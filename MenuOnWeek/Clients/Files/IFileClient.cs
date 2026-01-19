using MenuOnWeek.Contracts;
using MenuOnWeek.Utils;
using Utils;

namespace MenuOnWeek.Clients.Files;

public interface IFileClient
{
    /// <summary>
    /// Добавляет файл
    /// </summary>
    public Task<Guid> Add(string fileName, byte[] fileContent, CancellationToken token);

    /// <summary>
    /// Возвращает файл по id
    /// </summary>
    public Task<byte[]> Get(Guid? id, CancellationToken token);
}
