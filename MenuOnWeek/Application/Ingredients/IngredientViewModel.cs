using Domain;
using Application.Units;

namespace Application.Ingredients;

public sealed class IngredientViewModel
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required int Price { get; set; }

    public required Guid UnitId { get; set; }

    public required Dictionary<UnitViewModel, double> Table { get; set; } = new Dictionary<UnitViewModel, double>();

    public override string ToString() => Name;
}
