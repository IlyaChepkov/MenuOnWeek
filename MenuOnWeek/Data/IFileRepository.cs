namespace MenuOnWeek.Data;
/// <summary>
/// Файловый репозиторий
/// </summary>
public interface IFileRepository
{
    /// <summary>
    /// Добавляет файл в хранилище
    /// Возвращает id добавленного файла
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    Guid Add(string path);
}
