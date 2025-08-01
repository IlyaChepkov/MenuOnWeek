namespace Data;
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
    string Add(string path);
}
