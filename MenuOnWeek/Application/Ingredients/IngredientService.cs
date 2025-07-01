// Ignore Spelling: Repository

using Application.Units;
using Data;
using Domain;
using Utils;

namespace Application.Ingredients;

internal sealed class IngredientService : IIngredientService
{
    private readonly IIngredientRepository ingredientRepository;

    private readonly IUnitRepository unitRepository;
    private readonly (Guid id, int transform)[][] baseUnits =
        [
            [
                (Guid.Parse("c35a7e51-1795-4925-a81a-8ce764697671"), 1),
                (Guid.Parse("593a73bb-986c-415d-88b2-52f31ee2b8fb"), 1000)
            ],
            [
                (Guid.Parse("04cb064b-655b-4bfd-9cbd-3a9b41719127"), 1),
                (Guid.Parse("79277f2c-c5ce-45a2-91c0-c4167dab0f9f"), 1000)
            ]
        ];

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
                .Select(x => (unitRepository.GetAll(y => y.Id == x.Key.Id).Single(), x.Value))
                .ToDictionary(x => x.Item1, x => x.Item2));

        MainUnitBaseChecker(ingredient);
        TableUnitBaseChecker(ingredient);

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
        var currentUnit = ingredient.Unit.Required();

        ingredient.UnitId = updateRequest.UnitId;
        ingredient.Unit = unitRepository.GetAll(x => x.Id == updateRequest.UnitId).Single();
        ingredient.Name = updateRequest.Name;
        ingredient.Price = updateRequest.Price;
        if (currentUnit.Id != updateRequest.UnitId)
        {
            for (int i = 0; i < updateRequest.Table.Keys.Count; i++)
            {
                if (updateRequest.Table.Keys.ElementAt(i).Id == updateRequest.UnitId)
                {
                    updateRequest.Table.Remove(updateRequest.Table.Keys.ElementAt(i));
                    break;
                }
            }
        }
        ingredient.Table = updateRequest.Table
            .Select(x => (unitRepository.GetAll(y => y.Id == x.Key.Id).Single(), x.Value))
            .ToDictionary(x => x.Item1, x => x.Item2);

        MainUnitBaseChecker(ingredient);
        TableUnitBaseChecker(ingredient);

        ingredientRepository.Update(ingredient);
    }

    IngredientViewModel IIngredientService.GetById(Guid id)
    {
        var ingredient = ingredientRepository.GetAll(x => x.Id == id).Single();
        return ingredient.ConvertToIngredientViewModel();
    }

    IngredientViewModel? IIngredientService.GetByName(string name)
    {
        var ingredient = ingredientRepository.GetAll(x => x.Name == name).SingleOrDefault();
        if (ingredient is null)
        {
            return null;
        }
        return new IngredientViewModel()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Price = ingredient.Price,
            Table = ingredient.Table
               .Select(y => (y.Value, new UnitViewModel() { Id = y.Key.Id, Name = y.Key.Name }))
               .ToDictionary(y => y.Item2, y => y.Item1),
            UnitId = ingredient.UnitId
        };
        ;
    }

    IReadOnlyList<IngredientViewModel> IIngredientService.GetByPartName(string namePart, int offset, int limit)
    {
        return ingredientRepository
            .GetAll(x => x.Name.ToLower().Contains(namePart.ToLower()))
            .Skip(offset)
            .Take(limit)
            .Select(x => x.ConvertToIngredientViewModel()).ToList();
    }
    
    private void MainUnitBaseChecker(Ingredient ingredient)
    {
        for (int i = 0; i < baseUnits.Length; i++)
        {
            for (int j = 0; j < baseUnits[i].Length; j++)
            {
                if (baseUnits[i][j].id == ingredient.UnitId)
                {
                    for (int k = 0; k < baseUnits[i].Length; k++)
                    {
                        if (k != j && ingredient.Table.Keys.All(x => x.Id != baseUnits[i][k].id))
                        {
                            ingredient.Table.Add(unitRepository.GetAll(x => x.Id == baseUnits[i][k].id).Single(),
                                baseUnits[i][k].transform / (double)baseUnits[i][j].transform);
                        }
                    }
                    return;
                }
            }
        }
    }

    private void TableUnitBaseChecker(Ingredient ingredient)
    {
        for (int i = 0; i < ingredient.Table.Keys.Count; i++)
        {
            var unit = ingredient.Table.Keys.ElementAt(i);
            for (int j = 0; j < baseUnits.Length; j++)
            {
                for (int k = 0; k < baseUnits[j].Length; k++)
                {
                    if (unit.Id == baseUnits[j][k].id)
                    {
                        for (int a = 0; a < baseUnits[j].Length; a++)
                        {
                            if (a != k && baseUnits[i][a].id != ingredient.UnitId && ingredient.Table.Keys.All(x => x.Id != baseUnits[j][a].id))
                            {
                                double transform = ingredient.Table[unit];
                                ingredient.Table.Add(unitRepository.GetAll(x => x.Id == baseUnits[i][a].id).Single(),
                                    baseUnits[j][a].transform / (double)baseUnits[j][k].transform * transform);
                            }
                        }
                        return;
                    }
                }
            }
        }
    }
    
}
