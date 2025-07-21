using Application.Ingredients;
using Data;
using Domain;
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

    public void Add(CreateUnitModel createRequest)
    {
        if (GetByName(createRequest.Name) is not null)
        {
            throw new ArgumentException("единица измерения с таким именем существует");
        }
        var unit = Unit.Create(createRequest.Name);
        unitRepository.Add(unit);
    }

    public IReadOnlyList<UnitViewModel> GetAll(int offset, int limit)
    {
        return unitRepository
            .GetAll(x => true)
            .Skip(offset)
            .Take(limit)
            .Select(x => new UnitViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
    }

    public void Remove(Guid id)
    {
        var unit = unitRepository.GetAll(x => x.Id == id).Single();
        unitRepository.Remove(unit);
    }

    public void Update(UpdateUnitModel updateRequest)
    {
        var unit = unitRepository.GetAll(x => x.Id == updateRequest.Id).Single();
        unit.Name = updateRequest.Name;
        unitRepository.Update(unit);
    }

    public UnitViewModel? GetByName(string name)
    {
        var unit = unitRepository.GetAll(x => x.Name.
        ToLower() == name.ToLower()).SingleOrDefault();
        if (unit == null)
        {
            return null;
        }
        else
        {
            return new UnitViewModel()
            {
                Name = unit.Name,
                Id = unit.Id
            };
        }
    }

    public IReadOnlyList<UnitViewModel> GetByNamePart(string namePart, int offset, int limit)
    {
        return unitRepository.
            GetAll(x => x.Name.ToLower().
            Contains(namePart.ToLower())).
            Skip(offset).
            Take(limit).
            Select(x => new UnitViewModel()
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();
    }

    public UnitViewModel GetById(Guid id)
    {
        var unit = unitRepository.GetAll(x => x.Id == id).Single();
        return new UnitViewModel() { Name = unit.Name, Id = unit.Id };
    }

    public IReadOnlyList<UnitViewModel> GetByIngredient(Guid ingredientId)
    {
        Ingredient ingredient = ingredientRepository.GetAll(x=> x.Id == ingredientId).Single();
        List<UnitViewModel> units =
        [
            new UnitViewModel() { Id = ingredient.UnitId, Name = ingredient.Unit.Required().Name },
            .. ingredient.Table.Keys.Select(x => new UnitViewModel() { Id = x.Id, Name = x.Name}),
        ];
        return units;
    }
}
