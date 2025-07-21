namespace MenuOnWeek.Application.Recipes;

public sealed class RecipeUpdateModel
{

    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string? Image { get; set; }

    public required string Description { get; set; }

    public Dictionary<Guid, QuantityModel> Ingredients { get; set; } = new Dictionary<Guid, QuantityModel>();

    public bool IsImageChanged { get; set; }
}
