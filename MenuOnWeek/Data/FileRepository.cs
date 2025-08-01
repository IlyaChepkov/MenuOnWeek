

namespace Data;

internal sealed class FileRepository : IFileRepository
{
    public string Add(string path)
    {
        string name = Guid.NewGuid().ToString();

        if (path.Split('.').Count() > 1)
        {
            name += '.' + path.Split('.')[^1];
        }
        File.Copy(path, $"{Directory.GetCurrentDirectory()}\\FileStore\\{name}");

        return name;
    }
}
