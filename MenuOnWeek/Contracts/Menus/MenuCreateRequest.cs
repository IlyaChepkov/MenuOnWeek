using MenuOnWeek.Domain;

namespace MenuOnWeek.Contracts.Menus;
/// <summary>
/// Меню
/// </summary>
public sealed class MenuCreateRequest
{

    /// <summary>
    /// Название меню
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Тип меню
    /// </summary>
    public MenuType? MenuType { get; set; }

    /// <summary>
    /// Блюда меню
    /// </summary>
    public IReadOnlyList<MenuRecipesCreateOrUpdateRequest> MenuRecipes { get; set; } = new List<MenuRecipesCreateOrUpdateRequest>();
}
