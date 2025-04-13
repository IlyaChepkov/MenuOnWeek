using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain;

public sealed class MenuElement
{

    public MenuElement(Recipe recipe, int serve, DateTime date)
    {

        Recipe = recipe;
        Serve = serve;
        Date = date;

    }

    /// <summary>
    /// Id рецепта
    /// </summary>
    public int RecipeId { get; set; }

    /// <summary>
    /// Рецепт
    /// </summary>
    [JsonIgnore]
    public Recipe? Recipe {  get; set; }

    /// <summary>
    /// Количество порций
    /// </summary>
    public int Serve {  get; set; }

    /// <summary>
    /// Дата подачи
    /// </summary>
    public DateTime Date { get; set; }

}
