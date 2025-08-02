using Domain;
using Application.Units;

namespace Application.Ingredients;

public sealed class IngredientViewCommand
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required int Price { get; set; }

    public required Guid UnitId { get; set; }

    public required Dictionary<UnitViewCommand, double> Table { get; set; } = new Dictionary<UnitViewCommand, double>();

    public override string ToString() => Name;
}
