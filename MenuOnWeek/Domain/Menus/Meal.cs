using System.ComponentModel;

namespace MenuOnWeek.Domain.Menus;

/// <summary>
/// Прием пищи
/// </summary>
public enum Meal
{
    /// <summary>
    /// Завтрак
    /// </summary>
    [Description("Завтрак")]
    Breakfast,

    /// <summary>
    /// Обед
    /// </summary>
    [Description("Обед")]
    Lunch,

    /// <summary>
    /// Ужин
    /// </summary>
    [Description("Ужин")]
    Dinner
}
