namespace MenuOnWeek.Application.Recipes;

public sealed class RecipeViewModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required Guid? Image { get; set; }

    public required string Description { get; set; }

    public required Dictionary<Guid, QuantityViewModel> Ingredients { get; set; }
}
