namespace MenuOnWeek.Application.Recipes;

public sealed class RecipeUpdateCommand
{

    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string? Image { get; set; }

    public required string Description { get; set; }

    public Dictionary<Guid, QuantityCommand> Ingredients { get; set; } = new Dictionary<Guid, QuantityCommand>();

    public bool IsImageChanged { get; set; }
}
