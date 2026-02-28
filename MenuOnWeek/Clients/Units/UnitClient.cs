using System.Net.Http.Json;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Units;
using Microsoft.Extensions.Options;
using Utils;

namespace MenuOnWeek.Clients.Units;

internal sealed class UnitClient : IUnitClient
{
    private readonly HttpClient httpClient;
    private readonly Uri baseUri;

    public UnitClient(HttpClient httpClient, IOptions<ServerConnectionOptions> options)
    {
        this.httpClient = httpClient;
        baseUri = options.Value.Uri.Required();
    }

    public async Task Add(UnitCreateRequest request, CancellationToken token)
    {
        var result = await httpClient.PostAsJsonAsync(baseUri + ApiResource.Units, request, token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
    }

    public async Task<IReadOnlyList<UnitResponse>> Get(int offset, int limit, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.Units + ApiResource.GetPaginationQuery(limit, offset), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<IReadOnlyList<UnitResponse>>(token).ConfigureAwait(false) ?? [];
    }

    public async Task<UnitResponse> GetById(Guid? id, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.UnitsById.Replace("{id}", id.ToString()), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<UnitResponse>(token).ConfigureAwait(false) ?? throw new HttpRequestException();
    }

    public async Task<IReadOnlyList<UnitResponse>> GetByIngredient(Guid? ingredientId, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.UnitsByIngredient.Replace("{ingredient}", ingredientId.ToString()), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<IReadOnlyList<UnitResponse>>(token).ConfigureAwait(false) ?? [];
    }

    public async Task<UnitResponse?> GetByName(string? name, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.UnitsByName.Replace("{name}", name), token).ConfigureAwait(false);
        if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<UnitResponse>(token).ConfigureAwait(false) ?? throw new HttpRequestException();
    }
    public async Task<IReadOnlyList<UnitResponse>> GetByNamePart(string? namePart, int offset, int limit, CancellationToken token)
    {
        if (namePart == "")
        {
            throw new ArgumentException("Часть имени не может быть пустой");
        }
        var result = await httpClient.GetAsync(baseUri + ApiResource.UnitsByNamePart.Replace("{namePart}", namePart) + ApiResource.GetPaginationQuery(limit, offset), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<IReadOnlyList<UnitResponse>>(token).ConfigureAwait(false) ?? [];
    }
    public async Task Remove(Guid? id, CancellationToken token)
    {
        var result = await httpClient.DeleteAsync(baseUri + ApiResource.UnitsById.Replace("{id}", id.ToString()), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
    }
    public async Task Update(UnitUpdateRequest request, CancellationToken token)
    {
        var result = await httpClient.PutAsJsonAsync(baseUri + ApiResource.Units, request, token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
    }
}
