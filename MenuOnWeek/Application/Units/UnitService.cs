using Data;
using Domain;

namespace Application.Units;

internal sealed class UnitService : IUnitService
{
    private readonly IUnitRepository unitRepository;

    public UnitService(IUnitRepository unitRepository)
    {
        this.unitRepository = unitRepository;
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

    UnitViewModel IUnitService.GetById(Guid id)
    {
        var unit = unitRepository.GetAll(x => x.Id == id).Single();
        return new UnitViewModel() { Name = unit.Name, Id = unit.Id };
    }

}
