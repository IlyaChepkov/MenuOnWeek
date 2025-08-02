namespace MenuOnWeek.Application.Recipes;

public sealed class RecipeViewCommand
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string? Image { get; set; }

    public required double Price { get; set; }

    public required string Description { get; set; }

    public required Dictionary<Guid, QuantityViewModel> Ingredients { get; set; }
}
