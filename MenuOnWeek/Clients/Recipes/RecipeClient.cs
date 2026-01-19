using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Ingredients;
using MenuOnWeek.Contracts.Recipes;
using Microsoft.Extensions.Options;
using Utils;

namespace MenuOnWeek.Clients.Recipes;

internal sealed class RecipeClient : IRecipeClient
{
    private readonly HttpClient httpClient;
    private readonly Uri baseUri;

    public RecipeClient(HttpClient httpClient, IOptions<ServerConnectionOptions> options)
    {
        this.httpClient = httpClient;
        baseUri = options.Value.Uri.Required();
    }

    public async Task Add(RecipeCreateRequest request, CancellationToken token)
    {
        var result = await httpClient.PostAsJsonAsync(baseUri + ApiResource.Recipes, request, token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
    }

    public async Task<IReadOnlyList<RecipeResponse>> GetAll(int offset, int limit, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.Recipes + ApiResource.GetPaginationQuery(limit, offset), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<IReadOnlyList<RecipeResponse>>(token).ConfigureAwait(false) ?? [];
    }

    public async Task<RecipeResponse> GetById(Guid? id, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.RecipesById.Replace("{id}", id.ToString()), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<RecipeResponse>(token).ConfigureAwait(false) ?? throw new HttpRequestException();
    }

    public async Task<RecipeResponse?> GetByName(string? name, CancellationToken token)
    {
        var result = await httpClient.GetAsync(baseUri + ApiResource.RecipesByName.Replace("{name}", name), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        return await result.Content.ReadFromJsonAsync<RecipeResponse>(token).ConfigureAwait(false) ?? throw new HttpRequestException();
    }

    public async Task Remove(Guid? id, CancellationToken token)
    {
        var result = await httpClient.DeleteAsync(baseUri + ApiResource.RecipesById.Replace("{id}", id.ToString()), token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
    }

    public async Task Update(RecipeUpdateRequest request, CancellationToken token)
    {
        var result = await httpClient.PutAsJsonAsync(baseUri + ApiResource.Recipes, request, token).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
    }
}
