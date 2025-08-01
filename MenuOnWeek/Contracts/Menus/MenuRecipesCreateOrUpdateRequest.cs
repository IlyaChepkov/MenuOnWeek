using MenuOnWeek.Domain;

namespace MenuOnWeek.Contracts.Menus;

/// <summary>
/// Блюдо используемое в меню
/// </summary>
public sealed class MenuRecipesCreateOrUpdateRequest
{
    /// <summary>
    /// Id рецепта
    /// </summary>
    public Guid? RecipeId { get; set; }

    /// <summary>
    /// Прием пищи
    /// </summary>
    public Meal? Meal { get; set; }

    /// <summary>
    /// День недели
    /// </summary>
    public DayOfWeek? DayOfWeek { get; set; }

    /// <summary>
    /// Количество порций
    /// </summary>
    public int? Serve { get; set; }
}
