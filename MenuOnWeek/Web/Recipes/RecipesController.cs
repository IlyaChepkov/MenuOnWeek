 using MenuOnWeek.Application.Recipes;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Recipes;
using MenuOnWeek.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace MenuOnWeek.Web.Recipes;

/// <summary>
/// Контроллер рецептов
/// </summary>
[ApiController]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService recipeService;

    /// <summary>
    /// Конструктор RecipeController
    /// </summary>
    /// <param name="recipeService"></param>
    public RecipesController(IRecipeService recipeService)
    {
        this.recipeService = recipeService;
    }

    /// <summary>
    /// Добавляет рецепт
    /// </summary>
    [HttpPost(ApiResource.Recipes)]
    public async Task<IActionResult> Add(RecipeCreateRequest request, CancellationToken token)
    {
        ValidationException<string?>.ThrowIfNull(request.Name);
        ValidationException<IReadOnlyList<RecipeIngredientsCreateOrUpdateRequest>>.ThrowIf(
            x => x.Any(x => x.UnitId is null || x.UnitId == Guid.Empty || x.IngredientId is null || x.IngredientId == Guid.Empty || x.Count < 1),
            request.Ingredients,
            "Одно или несколько значений RecipeIngredients не заполнены");


        var command = new RecipeCreateCommand(
            request.Name.Required(),
            request.Image,
            request.Description ?? "",
            request.Ingredients.Select(x => (x.IngredientId.GetValueOrDefault(),
            new UpdateQuantityCommand
            (
                x.Count.GetValueOrDefault(),
                x.UnitId.GetValueOrDefault()
            )
            )).ToDictionary()
        );

        await recipeService.Add(command, token);

        return Ok();
    }

    /// <summary>
    /// Возвращает все рецепты, начиная с offset и заканчивая limit
    /// </summary>
    [HttpGet(ApiResource.Recipes)]
    public async Task<IReadOnlyList<RecipeResponse>> GetAll(int? offset, int? limit, CancellationToken token)
    {
        ValidationException<int?>.ThrowIfNull(offset);
        ValidationException<int?>.ThrowIfNull(limit);

        var recipes = await recipeService.Get(offset.Required(), limit.Required(), token);

        return recipes.Select(x => new RecipeResponse()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Ingredients = x.Ingredients.Select(y => new RecipeIngredientsResponse() { IngredientId = y.Key, UnitId = y.Value.UnitId, Count = (int)y.Value.Count }).ToList()}
        ).ToList();
    }

    /// <summary>
    /// Обновляет рецепт
    /// </summary>
    [HttpPut(ApiResource.Recipes)]
    public async Task<IActionResult> Update(RecipeUpdateRequest request, CancellationToken token)
    {
        ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, request.Id, "Идентификатор не может быть пустым");
        ValidationException<string?>.ThrowIfNull(request.Name);
        ValidationException<IReadOnlyList<RecipeIngredientsCreateOrUpdateRequest>>.ThrowIf(
            x => x.Any(x => x.UnitId is null || x.UnitId == Guid.Empty || x.IngredientId is null || x.IngredientId == Guid.Empty || x.Count < 1),
            request.Ingredients,
            "Одно или несколько значений RecipeIngredients не заполнены");
        
        var command = new RecipeUpdateCommand(
            request.Id.Required(),
            request.Name.Required(),
            request.Image,
            request.Description ?? "",
            request.Ingredients.Select(x => (x.IngredientId.GetValueOrDefault(), new UpdateQuantityCommand(
                x.Count.GetValueOrDefault(),
                x.UnitId.GetValueOrDefault()
            ))).ToDictionary()
        );

        await recipeService.Update(command, token);

        return Ok();
    }

    /// <summary>
    /// Удаляет рецепт
    /// </summary>
    [HttpDelete(ApiResource.RecipesById)]
    public async Task<IActionResult> Remove(Guid? id, CancellationToken token)
    {
       ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, id, "Идентификатор не может быть пустым");

       await recipeService.Remove(id.Required(), token);
       return Ok();
    }

    /// <summary>
    /// Возвращает рецепт по id
    /// </summary>
    [HttpGet(ApiResource.RecipesById)]
    public async Task<RecipeResponse> GetById(Guid? id, CancellationToken token)
    {
        ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, id, "Идентификатор не может быть пустым");

        var recipe = await recipeService.GetById(id.Required(), token);

        return new RecipeResponse()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Ingredients = recipe.Ingredients.Select(y => new RecipeIngredientsResponse() { IngredientId = y.Key, UnitId = y.Value.UnitId, Count = (int)y.Value.Count }).ToList()
        };
    }

    /// <summary>
    /// Возвращает рецепт по имени
    /// </summary>
    [HttpGet(ApiResource.RecipesByName)]
    public async Task<RecipeResponse?> GetByName(string? name, CancellationToken token)
    {
        ValidationException<string?>.ThrowIfNull(name);

        var recipe = await recipeService.GetByName(name.Required(), token);

        if (recipe == null)
        {
            return null;
        }

        return new RecipeResponse()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            ImageId = recipe.FileId,
            Price = (int)Math.Round(recipe.Price),
            Ingredients = recipe.Ingredients.Select(y => new RecipeIngredientsResponse() { IngredientId = y.Key, UnitId = y.Value.UnitId, Count = (int)y.Value.Count }).ToList()
        };
    }
}
