namespace MenuOnWeek.Application.Recipes;

public sealed class RecipeCreateModel
{
    public required string Name { get; set; }

    public required string Image { get; set; }

    public required string Description { get; set; }

    public required Dictionary<Guid, QuantityModel> Ingredients { get; set; }
}
