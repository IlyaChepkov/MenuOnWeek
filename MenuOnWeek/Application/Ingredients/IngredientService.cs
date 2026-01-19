using Application.Units;
using Data;
using MenuOnWeek.Application.Ingredients;
using MenuOnWeek.Application.Menus;
using MenuOnWeek.Data.Ingredients;
using MenuOnWeek.Domain.Ingredients;
using MenuOnWeek.Utils;
using Microsoft.Extensions.Logging;
using Utils;

namespace Application.Ingredients;

internal sealed class IngredientService : IIngredientService
{
    private readonly IIngredientRepository ingredientRepository;
    private readonly IUnitRepository unitRepository;
    private readonly IIngredientUnitsRepository ingredientUnitsRepository;
    private readonly ILogger logger;

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

    public IngredientService(IIngredientRepository ingredientRepository,
        IUnitRepository unitRepository,
        IIngredientUnitsRepository ingredientUnitsRepository,
        ILogger<IngredientService> logger)
    {
        this.ingredientRepository = ingredientRepository;
        this.unitRepository = unitRepository;
        this.ingredientUnitsRepository = ingredientUnitsRepository;
        this.logger = logger;
    }

    public async Task Add(CreateIngredientCommand createRequest, CancellationToken token)
    {
        var name = await GetByName(createRequest.Name, token);
        ValidationException<IngredientView?>.ThrowIf(x => x is not null, name, "Ингредиент с таким именем уже существует");

        var unit = await unitRepository.GetById(createRequest.UnitId, token);
        var ingredient = Ingredient
            .Create(createRequest.Name, createRequest.Price, unit);
        var table = createRequest.Table;

        MainUnitBaseChecker(ingredient, token);
        TableUnitBaseChecker(ingredient);

        await ingredientRepository.Add(ingredient, CancellationToken.None);
        await ingredientUnitsRepository.AddRange(createRequest.Table.Select(x => IngredientUnits.Create(ingredient.Id, x.Key.Id, x.Value)).ToList(), token);

        logger.LogInformation("Добавлен ингредиент с id {Id} названием {Name}", ingredient.Id, ingredient.Name);
    }

    public async Task<IReadOnlyList<IngredientView>> GetAll(int offset, int limit, CancellationToken token)
    {
        ValidationException<int>.ThrowIf(x => x < 0, offset, "Начальное значение не может быть меньше 0");
        ValidationException<int>.ThrowIf(x => x < 1, limit, "Размер выборки не может быть меньше 1");

        var ingredients = await ingredientRepository.
            Get(offset, limit, token);
        return ingredients.
            Select(x => x.ConvertToIngredientViewModel()).
            ToList();
    }

    public async Task Remove(Guid id, CancellationToken token)
    {
        var ingredient = await ingredientRepository.GetById(id, token);
        await ingredientRepository.Remove(ingredient, token);
        logger.LogInformation("Удален ингредиент с id {Id}", id);
    }

    public async Task Update(UpdateIngredientCommand updateRequest, CancellationToken token)
    {
        var name = await GetByName(updateRequest.Name, token);
        ValidationException<IngredientView?>.ThrowIf(x => x is not null && x.Id != updateRequest.Id, name, "Ингредиент с таким именем уже существует");

        var ingredient = await ingredientRepository.GetById(updateRequest.Id, token);
        var currentUnit = ingredient.Unit.Required();

        ingredient.UnitId = updateRequest.UnitId;
        ingredient.Unit = await unitRepository.GetById(updateRequest.UnitId, token);
        ingredient.Name = updateRequest.Name;
        ingredient.Price = updateRequest.Price;
        if (currentUnit.Id != updateRequest.UnitId)
        {
            for (int i = 0; i < updateRequest.Table.Keys.Count(); i++)
            {
                if (updateRequest.Table.Keys.ElementAt(i).Id == updateRequest.UnitId)
                {
                    //updateRequest.Table.Remove(updateRequest.Table.Keys.ElementAt(i));
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

        List<IngredientUnits> updateList = ingredient.IngredientUnits
            .Where(x => updateRequest.Table.Any(y => y.Key.Id == x.UnitId))
            .ToList();

        updateList = ingredientUnitsRepository.Get(0, await ingredientUnitsRepository.Count(token), token).Result.Where(x => x.IngredientId == ingredient.Id && updateRequest.Table.Any(y => y.Key.Id == x.UnitId && y.Value != x.Coeficient)).ToList();
            updateList.ForEach(x => x.Coeficient = updateRequest.Table.Single( y => x.UnitId == y.Key.Id).Value);

        await ingredientUnitsRepository.RemoveRange(deleteList, token);

        await ingredientUnitsRepository.UpdateRange(updateList, token);

        await ingredientUnitsRepository.AddRange(addList, token);

        MainUnitBaseChecker(ingredient, token);
        TableUnitBaseChecker(ingredient);

        await ingredientRepository.Update(ingredient, token);

        logger.LogInformation("Обновлен ингредиент с id {Id}", ingredient.Id);
    }

    public async Task<IngredientView> GetById(Guid id, CancellationToken token)
    {
        var ingredient = await ingredientRepository.GetById(id, token);
        return ingredient.ConvertToIngredientViewModel();
    }

    public async Task<IngredientView?> GetByName(string name, CancellationToken token)
    {
        var ingredient = await ingredientRepository.GetByName(name, token);
        if (ingredient is null)
        {
            return null;
        }
        return new IngredientView(

            ingredient.Id,
            ingredient.Name,
            ingredient.Price,
            ingredient.UnitId,
            ingredient.IngredientUnits
               .Select(x => (new UnitView(x.UnitId, x?.Unit?.Name), x.Required().Coeficient))
               .ToDictionary(y => y.Item1, y => y.Item2)
        );
    }

    public async Task<IReadOnlyList<IngredientView>> GetByPartName(string namePart, int offset, int limit, CancellationToken token)
    {
        ValidationException<int>.ThrowIf(x => x < 0, offset, "Начальное значение не может быть меньше 0");
        ValidationException<int>.ThrowIf(x => x < 1, limit, "Размер выборки не может быть меньше 1");

        var ingerdient = await ingredientRepository
            .GetByPartName(namePart, offset, limit, token);
        return ingerdient.Select(x => x.ConvertToIngredientViewModel()).ToList();
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
