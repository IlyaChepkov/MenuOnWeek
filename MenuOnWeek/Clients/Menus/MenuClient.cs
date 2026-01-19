using System.Net.Http.Json;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Menus;
using MenuOnWeek.Contracts.Recipes;
using Microsoft.Extensions.Options;
using Utils;

namespace MenuOnWeek.Clients.Menus;

internal sealed class MenuClient : IMenuClient
{
    private readonly HttpClient httpClient;
    private readonly Uri baseUri;

    public MenuClient(HttpClient httpClient, IOptions<ServerConnectionOptions> options)
    {
        this.httpClient = httpClient;
        baseUri = options.Value.Uri.Required();
    }

    public async Task Add(MenuCreateRequest request, CancellationToken token)
    {
        var result = await httpClient.PostAsJsonAsync(baseUri + ApiResource.Menus, request, token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
    }

    public async Task<IReadOnlyList<MenuResponse>> GetAll(int offset, int limit, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.Menus + ApiResource.GetPaginationQuery(limit, offset), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<IReadOnlyList<MenuResponse>>(token).ConfigureAwait(false) ?? [];
    }

    public async Task<MenuResponse?> GetByName(string? name, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.MenusByName.Replace("{name}", name), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<MenuResponse>(token).ConfigureAwait(false) ?? throw new HttpRequestException();
    }

    public async Task Remove(Guid? id, CancellationToken token)
    {
        var result = await httpClient.DeleteAsync(baseUri + ApiResource.MenusById.Replace("{id}", id.ToString()), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
    }

    public async Task Update(MenuUpdateRequest request, CancellationToken token)
    {
        var result = await httpClient.PutAsJsonAsync(baseUri + ApiResource.Menus, request, token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
    }
}
