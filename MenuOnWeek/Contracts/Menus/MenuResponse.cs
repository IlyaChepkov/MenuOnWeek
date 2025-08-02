using MenuOnWeek.Domain;

namespace MenuOnWeek.Contracts.Menus;

/// <summary>
/// Меню
/// </summary>
public sealed class MenuResponse
{
    /// <summary>
    /// Id меню
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Название меню
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Тип меню
    /// </summary>
    public MenuType? MenuType { get; set; }

    /// <summary>
    /// Цена меню
    /// </summary>
    public int? Price { get; set; }

    /// <summary>
    /// Блюда меню
    /// </summary>
    public IReadOnlyList<MenuRecipesResponse> MenuRecipes { get; set; } = Array.Empty<MenuRecipesResponse>();
}
