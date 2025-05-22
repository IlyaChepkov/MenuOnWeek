namespace Data;

internal sealed class DataOptions
{
    public static string SectionKey { get; } = "DataContext";

    public string? MenuDataStore { get; set; }

    public string? RecipeDataStore { get; set; }

    public string? IngredientDataStore { get; set; }

    public string? UnitDataStore { get; set; }

}
