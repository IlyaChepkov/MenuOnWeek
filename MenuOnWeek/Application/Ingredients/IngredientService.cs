using Data;
using Domain;
using Application.Units;
using System.Xml.Linq;

namespace Application.Ingredients;

internal sealed class IngredientService : IIngredientService
{
    private readonly IIngredientRepository ingredientRepository;

    private readonly IUnitRepository unitRepository;

    public IngredientService(IIngredientRepository ingredientRepository, IUnitRepository unitRepository)
    {
        this.ingredientRepository = ingredientRepository;
        this.unitRepository = unitRepository;
    }

    public void Add(CreateIngredientModel createRequest)
    {
        var unit = unitRepository.GetAll(x => x.Id == createRequest.UnitId).Single();
        var ingredient = Ingredient
            .Create(createRequest.Name, createRequest.Price, unit, createRequest.Table
                .Select(x => (new Unit(x.Key.Name) { Id = x.Key.Id, }, x.Value))
                .ToDictionary(x => x.Item1, x => x.Item2));
        ingredientRepository.Add(ingredient);
    }

    public IReadOnlyList<IngredientViewModel> GetAll(int offset, int limit)
    {
        return ingredientRepository.
            GetAll(x => true).
            Skip(offset).
            Take(limit).
            Select(x => x.ConvertToIngredientViewModel()).
            ToList();
    }

    public void Remove(Guid id)
    {
        var ingredient = ingredientRepository.GetAll(x => x.Id == id).Single();
        ingredientRepository.Remove(ingredient);
    }

    public void Update(UpdateIngredientModel updateRequest)
    {
        var ingredient = ingredientRepository.GetAll(x => x.Id == updateRequest.Id).Single();

        ingredient.UnitId = updateRequest.UnitId;
        ingredient.Unit = unitRepository.GetAll(x => x.Id == updateRequest.UnitId).Single();
        ingredient.Name = updateRequest.Name;
        ingredient.Price = updateRequest.Price;
        ingredient.Table = updateRequest.Table
            .Select(x => (new Unit(x.Key.Name) { Id = x.Key.Id, }, x.Value))
            .ToDictionary(x => x.Item1, x => x.Item2);

        ingredientRepository.Update(ingredient);
    }

    IngredientViewModel IIngredientService.GetById(Guid id)
    {
        var ingredient = ingredientRepository.GetAll(x => x.Id == id).Single();
        return ingredient.ConvertToIngredientViewModel();
    }

    IngredientViewModel IIngredientService.GetByName(string name)
    {
        var ingredient = ingredientRepository.GetAll(x => x.Name == name).Single();
        return ingredient.ConvertToIngredientViewModel();
    }

    IReadOnlyList<IngredientViewModel> IIngredientService.GetByPartName(string namePart, int offset, int limit)
    {
        return ingredientRepository
            .GetAll(x => x.Name.ToLower().Contains(namePart.ToLower()))
            .Skip(offset)
            .Take(limit)
            .Select(x => x.ConvertToIngredientViewModel()).ToList();
    }
}
