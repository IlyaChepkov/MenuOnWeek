using System.ComponentModel;

namespace MenuOnWeek.Domain;
public enum Meal
{
    [Description("Завтрак")]
    Breakfast,
    [Description("Обед")]
    Lunch,
    [Description("Ужин")]
    Dinner
}
