using System.Net.Http.Headers;
using System.Net.Http.Json;
using MenuOnWeek.Contracts;
using Microsoft.Extensions.Options;
using Utils;

namespace MenuOnWeek.Clients.Files;

internal sealed class FileClient : IFileClient
{
    private readonly HttpClient httpClient;
    private readonly Uri baseUri;

    public FileClient(HttpClient httpClient, IOptions<ServerConnectionOptions> options)
    {
        this.httpClient = httpClient;
        baseUri = options.Value.Uri.Required();
    }

    public async Task<Guid> Add(string fileName, byte[] fileContent, CancellationToken token)
    {
        var formFile = new ByteArrayContent(fileContent);
        formFile.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
        var multipartContent = new MultipartFormDataContent
        {
            { formFile, "formFile", fileName }
        };
        var result = await httpClient.PostAsync(baseUri + ApiResource.Files, multipartContent, token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<Guid>(token).ConfigureAwait(false); 
    }
    public async Task<byte[]> Get(Guid? id, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.FilesById.Replace("{id}", id.ToString()), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadAsByteArrayAsync(token).ConfigureAwait(false);
    }

    private string GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".webp" => "image/webp",
            _ => "application/octet-stream"
        };
    }
}
