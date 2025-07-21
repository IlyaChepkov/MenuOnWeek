using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MenuOnWeek.Domain;

namespace Domain;

public sealed class MenuElement
{

    public MenuElement(Recipe recipe, int serve, DaysOfWeek? date, Meal? meal)
    {

        Recipe = recipe;
        Serve = serve;
        Date = date;
        Meal = meal;
    }

    /// <summary>
    /// Id рецепта
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// Рецепт
    /// </summary>
    [JsonIgnore]
    public Recipe Recipe {  get; set; }

    /// <summary>
    /// Количество порций
    /// </summary>
    public int Serve {  get; set; }

    /// <summary>
    /// Дата подачи
    /// </summary>
    public DaysOfWeek? Date { get; set; }


    public Meal? Meal { get; set; }
}
