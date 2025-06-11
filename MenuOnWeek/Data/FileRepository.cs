namespace MenuOnWeek.Data;

internal sealed class FileRepository : IFileRepository
{
    public string Add(string path)
    {
        string name = Guid.NewGuid().ToString();

        string extension = "";

        if (path.Split('.').Count() > 1)
        {
            extension = '.' + path.Split('.')[^1];
        }
        File.Copy(path, $"{Directory.GetCurrentDirectory()}\\FileStore\\{name}{extension}");

        return name + extension;
    }
}
