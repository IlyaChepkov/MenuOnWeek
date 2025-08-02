// Ignore Spelling: Repository

using Application.Units;
using Data;
using Domain;
using MenuOnWeek.Application.Ingredients;
using MenuOnWeek.Data.Ingredients;
using MenuOnWeek.Domain;
using Utils;

namespace Application.Ingredients;

internal sealed class IngredientService : IIngredientService
{
    private readonly IIngredientRepository ingredientRepository;
    private readonly IUnitRepository unitRepository;
    private readonly IIngredientUnitsRepository ingredientUnitsRepository;

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

    public IngredientService(IIngredientRepository ingredientRepository, IUnitRepository unitRepository, IIngredientUnitsRepository ingredientUnitsRepository)
    {
        this.ingredientRepository = ingredientRepository;
        this.unitRepository = unitRepository;
        this.ingredientUnitsRepository = ingredientUnitsRepository;
    }

    public async Task Add(CreateIngredientCommand createRequest, CancellationToken token)
    {
        var unit = await unitRepository.GetById(createRequest.UnitId, token);
        var ingredient = Ingredient
            .Create(createRequest.Name, createRequest.Price, unit);
        var table = createRequest.Table;

        MainUnitBaseChecker(ingredient, token);
        TableUnitBaseChecker(ingredient);

        await ingredientRepository.Add(ingredient, CancellationToken.None);

        await ingredientUnitsRepository.AddRange(createRequest.Table.Select(x => IngredientUnits.Create(ingredient.Id, x.Key.Id, x.Value)).ToList(), token);
    }

    public async Task<IReadOnlyList<IngredientViewCommand>> GetAll(int offset, int limit, CancellationToken token)
    {
        var ingredients = await ingredientRepository.
            GetAll(token);
        return ingredients.
            Skip(offset).
            Take(limit).
            Select(x => x.ConvertToIngredientViewModel()).
            ToList();
    }

    public async Task Remove(Guid id, CancellationToken token)
    {
        var ingredient = await ingredientRepository.GetById(id, token);
        await ingredientRepository.Remove(ingredient, token);
    }

    public async Task Update(UpdateIngredientCommand updateRequest, CancellationToken token)
    {
        var ingredient = await ingredientRepository.GetById(updateRequest.Id, token);
        var currentUnit = ingredient.Unit.Required();

        ingredient.UnitId = updateRequest.UnitId;
        ingredient.Unit = await unitRepository.GetById(updateRequest.UnitId, token);
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
        
        List<IngredientUnits> deleteList = ingredient.IngredientUnits.Where(x => !updateRequest.Table.Any(y => y.Key.Id == x.UnitId)).ToList();

        List<IngredientUnits> addList = updateRequest.Table
            .Where(x => !ingredient.IngredientUnits.Any(y => y.UnitId == x.Key.Id))
            .Select(x => IngredientUnits.Create(ingredient.Id, x.Key.Id, x.Value))
            .ToList();
        ;

        List<IngredientUnits> updateList = ingredient.IngredientUnits.Where(x => updateRequest.Table.Any(y => y.Key.Id == x.IngredientId)).ToList();

        updateList = ingredientUnitsRepository.GetAll(token).Result.Where(x => x.IngredientId == ingredient.Id && updateRequest.Table.Any(y => y.Key.Id == x.UnitId && y.Value != x.Coeficient)).ToList();
            updateList.ForEach(x => x.Coeficient = updateRequest.Table.Single( y => x.UnitId == y.Key.Id).Value);

        await ingredientUnitsRepository.RemoveRange(deleteList, token);

        await ingredientUnitsRepository.UpdateRange(updateList, token);

        await ingredientUnitsRepository.AddRange(addList, token);

        MainUnitBaseChecker(ingredient, token);
        TableUnitBaseChecker(ingredient);

        await ingredientRepository.Update(ingredient, token);
    }

    public async Task<IngredientViewCommand> GetById(Guid id, CancellationToken token)
    {
        var ingredient = await ingredientRepository.GetById(id, token);
        return ingredient.ConvertToIngredientViewModel();
    }

    public async Task<IngredientViewCommand?> GetByName(string name, CancellationToken token)
    {
        var ingredient = await ingredientRepository.GetByName(name, token);
        if (ingredient is null)
        {
            return null;
        }
        return new IngredientViewCommand()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Price = ingredient.Price,
            Table = ingredient.IngredientUnits
               .Select(x => (new UnitViewCommand( x.UnitId, x.Unit.Required().Name ), x.Coeficient))
               .ToDictionary(y => y.Item1, y => y.Item2),
            UnitId = ingredient.UnitId
        };
        ;
    }

    public async Task<IReadOnlyList<IngredientViewCommand>> GetByPartName(string namePart, int offset, int limit, CancellationToken token)
    {
        var ingerdient = await ingredientRepository
            .GetByPartName(namePart, token);
        return ingerdient
            .Skip(offset)
            .Take(limit)
            .Select(x => x.ConvertToIngredientViewModel()).ToList();
    }

    private void MainUnitBaseChecker(Ingredient ingredient, CancellationToken token)
    {
        for (int i = 0; i < baseUnits.Length; i++)
        {
            for (int j = 0; j < baseUnits[i].Length; j++)
            {
                if (baseUnits[i][j].id == ingredient.UnitId)
                {
                    for (int k = 0; k < baseUnits[i].Length; k++)
                    {
                        if (k != j && ingredient.IngredientUnits.All(x => x.UnitId != baseUnits[i][k].id))
                        {
                            ingredientUnitsRepository.Add(IngredientUnits.Create(
                                ingredient.Id,
                                unitRepository.GetById(baseUnits[i][k].id, token).Result.Id,
                                baseUnits[i][k].transform / (double)baseUnits[i][j].transform
                                ), CancellationToken.None);
                        }
                    }
                    return;
                }
            }
        }
    }

    private void TableUnitBaseChecker(Ingredient ingredient)
    {
        for (int i = 0; i < ingredient.IngredientUnits.Count; i++)
        {
            var unit = ingredient.IngredientUnits[i].UnitId;
            for (int j = 0; j < baseUnits.Length; j++)
            {
                for (int k = 0; k < baseUnits[j].Length; k++)
                {
                    if (unit == baseUnits[j][k].id)
                    {
                        for (int a = 0; a < baseUnits[j].Length; a++)
                        {
                            if (a != k && baseUnits[i][a].id != ingredient.UnitId && ingredient.IngredientUnits.All(x => x.UnitId != baseUnits[j][a].id))
                            {
                                double transform = ingredient.IngredientUnits.Single(x => x.UnitId == unit).Coeficient;

                                ingredientUnitsRepository.Add(IngredientUnits.Create(
                                    ingredient.Id,
                                    unit,
                                    baseUnits[j][a].transform / (double)baseUnits[j][k].transform * transform), CancellationToken.None);
                            }
                        }
                        return;
                    }
                }
            }
        }
    }

}
