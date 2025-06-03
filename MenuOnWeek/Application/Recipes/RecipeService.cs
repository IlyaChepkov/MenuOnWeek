using Application.Ingredients;
using Data;
using Domain;
using MenuOnWeek.Data;

namespace MenuOnWeek.Application.Recipes;

public sealed class RecipeService : IRecipeService
{

    private readonly IUnitRepository unitRepository;
    private readonly IIngredientRepository ingredientRepository;
    private readonly IRecipeRepository recipeRepository;
    private readonly IFileRepository fileRepository;

    public RecipeService(IRecipeRepository recipeRepository, IFileRepository fileRepository, IUnitRepository unitRepository, IIngredientRepository ingredientRepository)
    {
        this.recipeRepository = recipeRepository;
        this.fileRepository = fileRepository;
        this.unitRepository = unitRepository;
        this.ingredientRepository = ingredientRepository;
    }

    public void Add(RecipeCreateModel entity)
    {
        Guid? imageId = null;
        if (!String.IsNullOrWhiteSpace(entity.Image))
        {
            imageId = fileRepository.Add(entity.Image);
        }
        // entity.Image

        var recipe = Recipe.Create(entity.Name, imageId, entity.Description);

        recipeRepository.Add(recipe);
    }

    public IReadOnlyList<RecipeViewModel> GetAll(int offset, int limit)
    {
        return
            recipeRepository.
            GetAll(x => true).
            Skip(offset).Take(limit).
            ToList().Select(x => new RecipeViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Description = x.Description,
                Ingredients = x.Ingredients.
                    Select(y => (y.Key.Id ,new QuantityViewModel()
                        { UnitId = y.Value.UnitId,
                        Count = y.Value.Count})).
                    ToDictionary(x => x.Item1, x => x.Item2)})
            .ToList();
    }

    public void Remove(Guid id)
    {
        recipeRepository.Remove(recipeRepository.GetAll(x => x.Id == id).Single());
    }

    public void Update(RecipeUpdateModel entity)
    {
        var recipe = recipeRepository.GetAll(x => x.Id == entity.Id).Single();

        recipe.Name = entity.Name;
        recipe.Description = entity.Description;
        recipe.Ingredients = entity.Ingredients.
            Select(x => (x.Key, new Quantity(x.Value.Count, unitRepository.
                GetAll(y => y.Id == x.Value.UnitId).
                Single()){ UnitId = x.Value.UnitId, })).
            ToDictionary(x => ingredientRepository.GetAll(y => y.Id == x.Item1).Single(), x => x.Item2);

        if (entity.IsImageChanged)
        {
            Guid? imageId = null;
            if (!String.IsNullOrWhiteSpace(entity.Image))
            {
                imageId = fileRepository.Add(entity.Image);
            }
            recipe.Image = imageId;
        }

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
            Ingredients = recipe.Ingredients.Select(x =>
            (x.Key.Id, new QuantityViewModel()
            {
                UnitId = x.Value.UnitId,
                Count = x.Value.Count
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
            Ingredients = recipe.Ingredients.Select(x =>
            (x.Key.Id, new QuantityViewModel()
            {
                UnitId = x.Value.UnitId,
                Count = x.Value.Count
            })).ToDictionary()
        };
    }
}
