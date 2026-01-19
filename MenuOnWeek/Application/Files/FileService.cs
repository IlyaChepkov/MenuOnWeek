using Microsoft.Extensions.Logging;
using File = MenuOnWeek.Domain.Files.File;

namespace MenuOnWeek.Application.Files;

internal sealed class FileService : IFileService
{
    private readonly IFileRepository fileRepository;
    private readonly ILogger logger;

    public FileService(
        IFileRepository fileRepository,
        ILogger<FileService> logger)
    {
        this.fileRepository = fileRepository;
        this.logger = logger;
    }

    public async Task<Guid> Add(string name, Stream fileContent, CancellationToken token)
    {


        var file = File.Create(name);
        await fileRepository.Add(file, fileContent, token);
        logger.LogInformation("Создан файл с id {Name}", name.Split('.').First());
        return file.Id;
    }

    public Task<FileWithName?> GetById(Guid id, CancellationToken token)
    {
        return fileRepository.GetById(id, token);
    }
}
