using MenuOnWeek.Application.Files;
using Microsoft.EntityFrameworkCore;
using File = MenuOnWeek.Domain.Files.File;

namespace Data;

internal sealed class FileRepository : IFileRepository
{
    private readonly DataContext dataContext;

    public FileRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task Add(File file, Stream fileContent, CancellationToken token)
    {
        using (Stream fileStream = new FileStream(FilePath(file.Name), FileMode.Create))
        {
            try
            {
                await fileContent.CopyToAsync(fileStream, token);
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        await dataContext.Set<File>().AddAsync(file, token);
        await dataContext.SaveChangesAsync(token);
    }

    public async Task<FileWithName?> GetById(Guid id, CancellationToken token)
    {
        var file = await dataContext.Files.SingleOrDefaultAsync(x => x.Id == id, token);
        if (file is null)
        {
            return null;
        }
        var content = await System.IO.File.ReadAllBytesAsync(FilePath(file.Name), token);
        return new FileWithName(file.Name, content);
    }

    private string FilePath(string fileName)
    {
        return $"{Directory.GetCurrentDirectory()}\\FileStore\\{fileName}";
    }
}
