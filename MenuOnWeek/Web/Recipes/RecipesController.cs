using MenuOnWeek.Application.Recipes;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Recipes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuOnWeek.Web.Recipes;

[ApiController]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService recipeService;

    public RecipesController(IRecipeService recipeService)
    {
        this.recipeService = recipeService;
    }

    [HttpPost(ApiResource.Recipes)]
    public async Task<IActionResult> Add(RecipeCreateRequest request, CancellationToken token)
    {
        if (String.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentNullException();
        }
        if (request.Ingredients.Any(x => x.UnitId is null || x.UnitId == Guid.Empty || x.IngredientId is null || x.IngredientId == Guid.Empty || x.Count < 1))
        {
            throw new ArgumentNullException();
        }

        var command = new RecipeCreateCommand() { Name = request.Name, Description = request.Description ?? "", Image = request.Image, Ingredients = request.Ingredients.Select(x => (x.IngredientId.GetValueOrDefault(), new QuantityCommand() { UnitId = x.UnitId.GetValueOrDefault(), Count = x.Count.GetValueOrDefault() })).ToDictionary() };

        await recipeService.Add(command, token);

        return Ok();
    }

    [HttpGet(ApiResource.Recipes)]
    public async Task<IReadOnlyList<RecipeResponse>> GetAll(int? offset, int? limit, CancellationToken token)
    {
        if (!limit.HasValue || !offset.HasValue)
        {
            throw new ArgumentNullException();
        }
        if (limit.Value < 1 || offset.Value < 0)
        {
            throw new ArgumentException();
        }

        var recipes = await recipeService.GetAll(limit.Value, offset.Value, token);

        return recipes.Select(x => new RecipeResponse() { Id = x.Id, Name = x.Name, Description = x.Description, Ingredients = x.Ingredients.Select(y => new RecipeIngredientsResponse() { IngredientId = y.Key, UnitId = y.Value.UnitId, Count = (int)y.Value.Count }).ToList()}).ToList();
    }

    [HttpPut(ApiResource.Recipes)]
    public async Task<IActionResult> Update(RecipeUpdateRequest request, CancellationToken token)
    {
        if (request.Id is null || request.Id == Guid.Empty)
        {
            throw new ArgumentNullException();
        }
        if (String.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentNullException();
        }
        if (request.Ingredients.Any(x => x.UnitId is null || x.UnitId == Guid.Empty || x.IngredientId is null || x.IngredientId == Guid.Empty || x.Count < 1))
        {
            throw new ArgumentNullException();
        }

        var command = new RecipeUpdateCommand() {Id = request.Id.Value, Name = request.Name, Description = request.Description ?? "", Image = request.Image ?? "", Ingredients = request.Ingredients.Select(x => (x.IngredientId.GetValueOrDefault(), new QuantityCommand() { UnitId = x.UnitId.GetValueOrDefault(), Count = x.Count.GetValueOrDefault() })).ToDictionary() };

        await recipeService.Update(command, token);

        return Ok();
    }

    [HttpDelete(ApiResource.Recipes)]
    public async Task<IActionResult> Remove(Guid? id, CancellationToken token)
    {
        if (id is null || id == Guid.Empty)
        {
            throw new ArgumentNullException();
        }

       await recipeService.Remove(id.Value, token);
       return Ok();
    }

    [HttpGet(ApiResource.RecipesById)]
    public async Task<RecipeResponse> GetById(Guid? id, CancellationToken token)
    {
        if (id is null || id == Guid.Empty)
        {
            throw new ArgumentNullException();
        }

        var recipe = await recipeService.GetById(id.Value, token);

        return new RecipeResponse() { Id = recipe.Id, Name = recipe.Name, Description = recipe.Description, Ingredients = recipe.Ingredients.Select(y => new RecipeIngredientsResponse() { IngredientId = y.Key, UnitId = y.Value.UnitId, Count = (int)y.Value.Count }).ToList() };
    }

    [HttpGet(ApiResource.RecipesByName)]
    public async Task<RecipeResponse?> GetByName(string? name, CancellationToken token)
    {
        if (String.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException();
        }

        var recipe = await recipeService.GetByName(name, token);

        if (recipe == null)
        {
            return null;
        }

        return new RecipeResponse() { Id = recipe.Id, Name = recipe.Name, Description = recipe.Description, Ingredients = recipe.Ingredients.Select(y => new RecipeIngredientsResponse() { IngredientId = y.Key, UnitId = y.Value.UnitId, Count = (int)y.Value.Count }).ToList() };
    }
}
