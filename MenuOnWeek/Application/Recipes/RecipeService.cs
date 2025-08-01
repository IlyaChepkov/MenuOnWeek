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

    public void Add(RecipeCreateModel entity)
    {
        string? imageId = null;
        if (!String.IsNullOrWhiteSpace(entity.Image))
        {
            imageId = fileRepository.Add(entity.Image);
        }
        // entity.Image

        var recipe = Recipe.Create(entity.Name, imageId, entity.Description);

        recipeRepository.Add(recipe);

        recipeIngredientsRepository.AddRange(entity.Ingredients.Select(x => RecipeIngredients.Create(recipe.Id, x.Key, x.Value.UnitId, x.Value.Count)).ToList());


    }

    public IReadOnlyList<RecipeViewModel> GetAll(int offset, int limit)
    {
        return
            recipeRepository.
            GetAll(x => true).
            Skip(offset).Take(limit).
            ToList().Select(x => x.ConvertToRecipeViewModel())
            .ToList();
    }

    public void Remove(Guid id)
    {
        recipeRepository.Remove(recipeRepository.GetAll(x => x.Id == id).Single());
    }

    public void Update(RecipeUpdateModel updateRequest)
    {
        var recipe = recipeRepository.GetById(updateRequest.Id);

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

        recipeIngredientsRepository.RemoveRange(deleteList);

        List<RecipeIngredients> updateList = recipe.RecipeIngredients.Where(x => updateRequest.Ingredients.ContainsKey(x.IngredientId)).ToList();
        updateList.ForEach(x =>
        {
            x.UnitId = updateRequest.Ingredients.Single(y => y.Key == x.IngredientId).Value.UnitId;
            x.Count = updateRequest.Ingredients.Single(y => y.Key == x.IngredientId).Value.Count;
        });

        recipeIngredientsRepository.UpdateRange(updateList);

        List<RecipeIngredients> addList = updateRequest.Ingredients
            .Where(x => !recipe.RecipeIngredients.Any(y => y.IngredientId == x.Key))
            .Select(x => RecipeIngredients.Create(recipe.Id, x.Key, x.Value.UnitId, x.Value.Count))
            .ToList();

        recipeIngredientsRepository.AddRange(addList);

        recipeRepository.Update(recipe);
    }

    public RecipeViewModel GetById(Guid id)
    {
        Recipe recipe = recipeRepository.GetAll(x => x.Id == id).Single();
        return new RecipeViewModel()
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

    public RecipeViewModel GetByName(string name)
    {
        Recipe recipe = recipeRepository.GetAll(x => x.Name == name).Single();
        return new RecipeViewModel()
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
