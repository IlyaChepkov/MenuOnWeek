using System.Xml.Linq;
using Data;
using MenuOnWeek.Data.Ingredients;
using MenuOnWeek.Domain.Units;
using MenuOnWeek.Utils;
using Microsoft.Extensions.Logging;
using Utils;

namespace Application.Units;
 
internal sealed class UnitService : IUnitService
{
    private readonly IUnitRepository unitRepository;
    private readonly IIngredientRepository ingredientRepository;
    private ILogger logger;


    public UnitService(IUnitRepository unitRepository,
        IIngredientRepository ingredientRepository,
        ILogger<UnitService> logger)
    {
        this.unitRepository = unitRepository;
        this.ingredientRepository = ingredientRepository;
        this.logger = logger;
    }

    public async Task Add(CreateUnitCommand createRequest, CancellationToken token)
    {
        var name = await GetByName(createRequest.Name.Required(), token);
        ValidationException<UnitView?>.ThrowIf(x => x is not null, name, "Единица с таким именем уже существует");

        var unit = Unit.Create(createRequest.Name.Required());
        await unitRepository.Add(unit, CancellationToken.None);

        logger.LogInformation("Добавлена единица измерения с id {Id}, и названием {Name}", unit.Id, unit.Name);
    }

    public async Task<IReadOnlyList<UnitView>> Get(int offset, int limit, CancellationToken token)
    {
        ValidationException<int>.ThrowIf(x => x < 0, offset, "Начальное значение не может быть меньше 0");
        ValidationException<int>.ThrowIf(x => x < 1, limit, "Размер выборки не может быть меньше 1");

        var units = await unitRepository.Get(offset, limit, token);
        return units.Select(x => new UnitView(x.Id, x.Name)).ToList();
    }

    public async Task Remove(Guid id, CancellationToken token)
    {
        var unit = unitRepository.GetById(id, token);

        await unitRepository.Remove(unit.Result, token);
        logger.LogInformation("Удалена единица измерения с id {Id}", id);
    }

    public async Task Update(UpdateUnitCommand updateRequest, CancellationToken token)
    {
        var unit = unitRepository.GetById(updateRequest.Id, token).Result;
        var name = unitRepository.GetByName(updateRequest.Name.Required(), token).Result;

        ValidationException<Unit?>.ThrowIf(x => x is not null, name, "Единица с таким именем уже существует");

        unit.Name = updateRequest.Name.Required();
        await unitRepository.Update(unit, token);
        logger.LogInformation("Обновлена единица измерения с id {Id}", unit.Id);
    }

    public async Task<UnitView?> GetByName(string name, CancellationToken token)
    {
        var unit = await unitRepository.GetByName(name, token);
        if (unit == null)
        {
            return null;
        }
        else
        {
            return new UnitView(
                 unit.Id,
                 unit.Name
            );
        }
    }

    public async Task<IReadOnlyList<UnitView>> GetByNamePart(string namePart, int offset, int limit, CancellationToken token)
    {
        ValidationException<int>.ThrowIf(x => x < 0, offset, "Начальное значение не может быть меньше 0");
        ValidationException<int>.ThrowIf(x => x < 1, limit, "Размер выборки не может быть меньше 1");

        var units = await unitRepository.GetByPartName(namePart, offset, limit, token);

        return units
            .Select(x => new UnitView(
                 x.Id,
                 x.Name
            ))
            .ToList();
    }

    public async Task<UnitView> GetById(Guid id, CancellationToken token)
    {
        var unit = await unitRepository.GetById(id, token);
        return new UnitView(unit.Id, unit.Name);
    }

    public async Task<IReadOnlyList<UnitView>> GetByIngredient(Guid ingredientId, CancellationToken token)
    {
        var ingredient = await ingredientRepository.GetById(ingredientId, token);
        List<UnitView> units =
        [
            new UnitView( ingredient.UnitId, ingredient.Unit.Required().Name ),
            .. ingredient.IngredientUnits.Select(x => new UnitView( x.UnitId, x.Unit?.Name)),
        ];
        return units;
    }
}
