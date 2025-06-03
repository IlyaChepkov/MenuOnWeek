namespace MenuOnWeek.Data;

internal sealed class FileRepository : IFileRepository
{
    public Guid Add(string path)
    {
        Guid id = Guid.NewGuid();

        File.Copy(path, $"{Directory.GetCurrentDirectory()}\\FileStore\\{id.ToString()}{path.Split('.')[^1]}");

        return id;
    }
}
