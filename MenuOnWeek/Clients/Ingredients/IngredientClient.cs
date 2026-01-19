using System.Net.Http;
using System.Net.Http.Json;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Ingredients;
using MenuOnWeek.Contracts.Units;
using Microsoft.Extensions.Options;
using Utils;

namespace MenuOnWeek.Clients.Ingredients;

internal sealed class IngredientClient : IIngredientClient
{
    private readonly HttpClient httpClient;
    private readonly Uri baseUri;

    public IngredientClient(HttpClient httpClient, IOptions<ServerConnectionOptions> options)
    {
        this.httpClient = httpClient;
        baseUri = options.Value.Uri.Required();
    }

    public async Task Add(IngredientCreateRequest request, CancellationToken token)
    {
        var result = await httpClient.PostAsJsonAsync(baseUri + ApiResource.Ingredients, request, token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return;
    }

    public async Task<IReadOnlyList<IngredientResponse>> GetAll(int offset, int limit, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.Ingredients + ApiResource.GetPaginationQuery(limit, offset), token).ConfigureAwait(false);

        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<IReadOnlyList<IngredientResponse>>(token).ConfigureAwait(false) ?? [];
    }

    public async Task<IngredientResponse> GetById(Guid? id, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.IngredientsById.Replace("{id}", id.ToString()), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<IngredientResponse>(token).ConfigureAwait(false) ?? throw new HttpRequestException();
    }

    public async Task<IngredientResponse?> GetByName(string name, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.IngredientsByName.Replace("{name}", name), token).ConfigureAwait(false);

        if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<IngredientResponse>(token).ConfigureAwait(false) ?? throw new HttpRequestException();
    }

    public async Task<IReadOnlyList<IngredientResponse>> GetByPartName(string? namePart, int offset, int limit, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.IngredientsByNamePart.Replace("{name-part}", namePart) + ApiResource.GetPaginationQuery(limit, offset), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<IReadOnlyList<IngredientResponse>>(token).ConfigureAwait(false) ?? [];
    }

    public async Task Remove(Guid? id, CancellationToken token)
    {
        var result = await httpClient.DeleteAsync(baseUri + ApiResource.IngredientsById.Replace("{id}", id.ToString()), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
    }

    public async Task Update(IngredientUpdateRequest request, CancellationToken token)
    {
        var result = await httpClient.PutAsJsonAsync(baseUri + ApiResource.Ingredients, request, token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
    }
}
