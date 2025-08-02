using System.Threading.Tasks;
using Data;
using Domain;
using MenuOnWeek.Data.Ingredients;
using MenuOnWeek.Domain;

namespace MenuOnWeek.Application.Recipes;

public sealed class RecipeService : IRecipeService
{

    private readonly IUnitRepository unitRepository;
    private readonly IIngredientRepository ingredientRepository;
    private readonly IRecipeRepository recipeRepository;
    private readonly IFileRepository fileRepository;
    private readonly IRecipeIngredientsRepository recipeIngredientsRepository;

    public RecipeService(IRecipeRepository recipeRepository, IFileRepository fileRepository, IUnitRepository unitRepository, IIngredientRepository ingredientRepository, IRecipeIngredientsRepository recipeIngredientsRepository)
    {
        this.recipeRepository = recipeRepository;
        this.fileRepository = fileRepository;
        this.unitRepository = unitRepository;
        this.ingredientRepository = ingredientRepository;
        this.recipeIngredientsRepository = recipeIngredientsRepository;
    }

    public async Task Add(RecipeCreateCommand entity, CancellationToken token)
    {
        string? imageId = null;
        if (!String.IsNullOrWhiteSpace(entity.Image))
        {
            imageId = fileRepository.Add(entity.Image);
        }
        // entity.Image

        var recipe = Recipe.Create(entity.Name, imageId, entity.Description);

        await recipeRepository.Add(recipe, token);

        await recipeIngredientsRepository.AddRange(entity.Ingredients.Select(x => RecipeIngredients.Create(recipe.Id, x.Key, x.Value.UnitId, x.Value.Count)).ToList(), token);


    }

    public async Task<IReadOnlyList<RecipeViewCommand>> GetAll(int offset, int limit, CancellationToken token)
    {
        var recipes = await recipeRepository.
            GetAll(token);
        return
            recipes.
            Skip(offset).Take(limit).
            ToList().Select(x => x.ConvertToRecipeViewModel())
            .ToList();
    }

    public async Task Remove(Guid id, CancellationToken token)
    {
        await recipeRepository.Remove(recipeRepository.GetById(id, token).Result, token);
    }

    public async Task Update(RecipeUpdateCommand updateRequest, CancellationToken token)
    {
        var recipe = await recipeRepository.GetById(updateRequest.Id, token);

        recipe.Name = updateRequest.Name;
        recipe.Description = updateRequest.Description;
        //recipe.RecipeIngredients = entity.Ingredients.
        //    Select(x => (x.Key, new Quantity(x.Value.Count, unitRepository.
        //       GetAll(y => y.Id == x.Value.UnitId).
        //        Single())
        //    { UnitId = x.Value.UnitId, })).
        //    ToDictionary(x => ingredientRepository.GetAll(y => y.Id == x.Item1).Single(), x => x.Item2);

        if (updateRequest.IsImageChanged)
        {
            string? imageId = null;
            if (!String.IsNullOrWhiteSpace(updateRequest.Image))
            {
                imageId = fileRepository.Add(updateRequest.Image);
            }
            recipe.Image = imageId;
        }

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
    }

    public async Task<RecipeViewCommand> GetById(Guid id, CancellationToken token)
    {
        Recipe recipe = await recipeRepository.GetById(id, token);
        return new RecipeViewCommand()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Image = recipe.Image,
            Price = recipe.Price,
            Ingredients = recipe.RecipeIngredients.Select(x =>
            (x.IngredientId, new QuantityViewModel()
            {
                UnitId = x.UnitId,
                Count = x.Count
            })).ToDictionary()
        };
    }

    public async Task<RecipeViewCommand?> GetByName(string name, CancellationToken token)
    {
        var recipe = await recipeRepository.GetByName(name, token);
        if (recipe is null)
        {
            return null;
        }
        return new RecipeViewCommand()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Image = recipe.Image,
            Price = recipe.Price,
            Ingredients = recipe.RecipeIngredients.Select(x =>
            (x.IngredientId, new QuantityViewModel()
            {
                UnitId = x.UnitId,
                Count = x.Count
            })).ToDictionary()
        };
    }
}
