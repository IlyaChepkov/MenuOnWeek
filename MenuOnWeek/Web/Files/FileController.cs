using MenuOnWeek.Contracts;
using Microsoft.AspNetCore.Mvc;
using MenuOnWeek.Application.Files;
using MenuOnWeek.Utils;
using Utils;

namespace MenuOnWeek.Web.Files;

/// <summary>
/// Контроллер для файлов
/// </summary>
[ApiController]
public class FileController : Controller
{
    private readonly IFileService fileService;

    /// <summary>
    /// Конструктор FileController
    /// </summary>
    public FileController(IFileService fileService)
    {
        this.fileService = fileService;
    }

    /// <summary>
    /// Добавляет файл
    /// </summary>
    [HttpPost(ApiResource.Files)]
    public async Task<Guid> Add(IFormFile formFile, CancellationToken token)
    {
        using (var stream = formFile.OpenReadStream())
        {
            return await fileService.Add(formFile.FileName, stream, token);
        }
    }

    /// <summary>
    /// Возвращает файл по id
    /// </summary>
    [HttpGet(ApiResource.FilesById)]
    public async Task<FileResult> Get(Guid? id, CancellationToken token)
    {
        ValidationException<Guid?>.ThrowIfNull(id);
            
        var file = await fileService.GetById(id.Required(), token);
        return File(file.Required().Content, "application/octet-stream", file.Required().Name);
    }
}
