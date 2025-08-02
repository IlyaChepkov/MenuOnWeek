using System.Threading.Tasks;
using Data;
using Domain;
using MenuOnWeek.Data.Ingredients;
using Utils;

namespace Application.Units;

internal sealed class UnitService : IUnitService
{
    private readonly IUnitRepository unitRepository;
    private readonly IIngredientRepository ingredientRepository;

    public UnitService(IUnitRepository unitRepository, IIngredientRepository ingredientRepository)
    {
        this.unitRepository = unitRepository;
        this.ingredientRepository = ingredientRepository;
    }

    public async Task Add(CreateUnitCommand createRequest, CancellationToken token)
    {
        var name = await GetByName(createRequest.Name.Required(), token);
        if (name is not null)
        {
            throw new ArgumentException("единица измерения с таким именем существует");
        }
        var unit = Unit.Create(createRequest.Name.Required());
        await unitRepository.Add(unit, CancellationToken.None);
    }

    public async Task<IReadOnlyList<UnitViewCommand>> GetAll(int offset, int limit, CancellationToken token)
    {
        var units = await unitRepository.GetAll(token);
        return units
            .Skip(offset)
            .Take(limit)
            .Select(x => new UnitViewCommand(
            
                x.Id,
                x.Name
            )).ToList();
    }

    public async Task Remove(Guid id, CancellationToken token)
    {
        var unit = unitRepository.GetById(id, token);
        await unitRepository.Remove(unit.Result, token);
    }

    public async Task Update(UpdateUnitCommand updateRequest, CancellationToken token)
    {
        var unit = unitRepository.GetById(updateRequest.Id, token).Result;
        unit.Name = updateRequest.Name.Required();
        await unitRepository.Update(unit, token);
    }

    public async Task<UnitViewCommand?> GetByName(string name, CancellationToken token)
    {
        var unit = await unitRepository.GetByName(name, token);
        if (unit == null)
        {
            return null;
        }
        else
        {
            return new UnitViewCommand(
                 unit.Id,
                 unit.Name
            );
        }
    }

    public async Task<IReadOnlyList<UnitViewCommand>> GetByNamePart(string namePart, int offset, int limit, CancellationToken token)
    {
        var units = await unitRepository.GetByPartName(namePart, token);

        return units
            .Skip(offset)
            .Take(limit)
            .Select(x => new UnitViewCommand(
                 x.Id,
                 x.Name
            ))
            .ToList();
    }

    public async Task<UnitViewCommand> GetById(Guid id, CancellationToken token)
    {
        var unit = await unitRepository.GetById(id, token);
        return new UnitViewCommand(unit.Id, unit.Name);
    }

    public async Task<IReadOnlyList<UnitViewCommand>> GetByIngredient(Guid ingredientId, CancellationToken token)
    {
        var ingredient = await ingredientRepository.GetById(ingredientId, token);
        List<UnitViewCommand> units =
        [
            new UnitViewCommand( ingredient.UnitId, ingredient.Unit.Required().Name ),
            .. ingredient.IngredientUnits.Select(x => new UnitViewCommand( x.UnitId, x.Unit?.Name)),
        ];
        return units;
    }
}
