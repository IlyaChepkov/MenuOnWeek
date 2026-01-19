using Application.Units;
using Data;
using MenuOnWeek.Application.Files;
using MenuOnWeek.Domain.Recipes;
using MenuOnWeek.Utils;
using Microsoft.Extensions.Logging;
using Utils;
using File = MenuOnWeek.Domain.Files.File;

namespace MenuOnWeek.Application.Recipes;

internal sealed class RecipeService : IRecipeService
{
    private readonly IRecipeRepository recipeRepository;
    private readonly IRecipeIngredientsRepository recipeIngredientsRepository;
    private readonly IFileRepository fileRepository;
    private ILogger logger;

    public RecipeService(IRecipeRepository recipeRepository,
        IRecipeIngredientsRepository recipeIngredientsRepository,
        IFileRepository fileRepository,
        ILogger<RecipeService> logger)
    {
        this.recipeRepository = recipeRepository;
        this.recipeIngredientsRepository = recipeIngredientsRepository;
        this.fileRepository = fileRepository;
        this.logger = logger;
    }

    public async Task Add(RecipeCreateCommand createRequest, CancellationToken token)
    {
        var name = await GetByName(createRequest.Name, token);
        ValidationException<RecipeView?>.ThrowIf(x => x is not null, name, "Рецепт с таким именем уже существует");

        Guid? fileId = null;
        if (createRequest.FileId is not null && createRequest.FileId != Guid.Empty)
        {
            FileWithName? file = await fileRepository.GetById(createRequest.FileId.Required(), token);
            fileId = Guid.Parse(file.Required().Name.Split('.').First());
        }

        var recipe = Recipe.Create(createRequest.Name,
            fileId,
            createRequest.Description);

        await recipeRepository.Add(recipe, token);

        await recipeIngredientsRepository.AddRange(createRequest.Ingredients.Select(x => RecipeIngredients.Create(recipe.Id, x.Key, x.Value.UnitId, x.Value.Count)).ToList(), token);

        logger.LogInformation("Добавлен рецепт с id {Id} и названием {Name}", recipe.Id, recipe.Name);
    }

    public async Task<IReadOnlyList<RecipeView>> Get(int offset, int limit, CancellationToken token)
    {
        ValidationException<int>.ThrowIf(x => x < 0, offset, "Начальное значение не может быть меньше 0");
        ValidationException<int>.ThrowIf(x => x < 1, limit, "Размер выборки не может быть меньше 1");

        var recipes = await recipeRepository.
            Get(offset, limit, token);
        return
            recipes
            .Select(x => x.ConvertToRecipeViewModel())
            .ToList();
    }

    public async Task Remove(Guid id, CancellationToken token)
    {
        await recipeRepository.Remove(recipeRepository.GetById(id, token).Result, token);

        logger.LogInformation("Удален рецепт с id {id}", id);
    }

    public async Task Update(RecipeUpdateCommand updateRequest, CancellationToken token)
    {
        var name = await GetByName(updateRequest.Name, token);
        ValidationException<RecipeView?>.ThrowIf(x => x is not null && x.Id != updateRequest.Id, name, "Рецепт с таким именем уже существует");

        Guid? fileId = null;
        if (updateRequest.FileId is not null  && updateRequest.FileId != Guid.Empty)
        {
            FileWithName? file = await fileRepository.GetById(updateRequest.FileId.Value, token);
            fileId = Guid.Parse(file.Required().Name.Split('.').First());
        }

        var recipe = await recipeRepository.GetById(updateRequest.Id, token);

        recipe.Name = updateRequest.Name;
        recipe.Description = updateRequest.Description;
        recipe.FileId = fileId;

        List<RecipeIngredients> deleteList = recipe.RecipeIngredients.Where(x => !updateRequest.Ingredients.ContainsKey(x.IngredientId)).ToList();

        await recipeIngredientsRepository.RemoveRange(deleteList, token);

        List<RecipeIngredients> updateList = recipe.RecipeIngredients.Where(x => updateRequest.Ingredients.ContainsKey(x.IngredientId)).ToList();
        updateList.ForEach(x =>
        {
            x.UnitId = updateRequest.Ingredients.Single(y => y.Key == x.IngredientId).Value.UnitId;
            x.Count = updateRequest.Ingredients.Single(y => y.Key == x.IngredientId).Value.Count;
        });

        await recipeIngredientsRepository.UpdateRange(updateList, token);

        List<RecipeIngredients> addList = updateRequest.Ingredients
            .Where(x => !recipe.RecipeIngredients.Any(y => y.IngredientId == x.Key))
            .Select(x => RecipeIngredients.Create(recipe.Id, x.Key, x.Value.UnitId, x.Value.Count))
            .ToList();

        await recipeIngredientsRepository.AddRange(addList, token);

        await recipeRepository.Update(recipe, token);

        logger.LogInformation("Обновлен рецепт с id {id}", recipe.Id);
    }

    public async Task<RecipeView> GetById(Guid id, CancellationToken token)
    {
        Recipe recipe = await recipeRepository.GetById(id, token);
        return new RecipeView(

            recipe.Id,
            recipe.Name,
            recipe.FileId,
            recipe.Price,
            recipe.Description,
            recipe.RecipeIngredients.Select(x =>
            (x.IngredientId, new QuantityView(
                x.Count,
                x.UnitId
            ))).ToDictionary()
        );
    }

    public async Task<RecipeView?> GetByName(string name, CancellationToken token)
    {
        var recipe = await recipeRepository.GetByName(name, token);
        if (recipe is null)
        {
            return null;
        }
        return new RecipeView(

           recipe.Id,
           recipe.Name,
           recipe.FileId,
           recipe.Price,
           recipe.Description,
           recipe.RecipeIngredients.Select(x =>
           (x.IngredientId, new QuantityView(
               x.Count,
               x.UnitId
           ))).ToDictionary()
       );
    }
}
