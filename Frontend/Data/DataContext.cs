using Domain;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Data;

internal sealed class DataContext
{

    private DataOptions dataOptions;

    internal DataContext(IOptions<DataOptions> dataOptions)
    {
        this.dataOptions = dataOptions.Value;

        string json = File.ReadAllText(this.dataOptions.UnitDataStore);
        Units = JsonSerializer.Deserialize<List<Unit>>(json);

        json = File.ReadAllText(this.dataOptions.IngredientDataStore);
        Ingredients = JsonSerializer.Deserialize<List<Ingredient>>(json);

        for (int i = 0; i < Ingredients.Count; i++)
        {
            Ingredients[i].Unit = Units.Single(x => x.Id == Ingredients[i].UnitId);

            for (int j = 0; j < Ingredients[i].RawTable.Count; j++)
            {
                Ingredients[i].Table.Add(
                    Units.Single(x => x.Id == Ingredients[i].RawTable.Keys.ElementAt(j)),
                    Ingredients[i].RawTable.Values.ElementAt(j));
            }
        }

        json = File.ReadAllText(this.dataOptions.RecipeDataStore);
        Recipes = JsonSerializer.Deserialize<List<Recipe>>(json);

        for (int i = 0; i < Recipes.Count; i++)
        {
            for (int j = 0; j < Recipes[i].RawIngredients.Count; j++)
            {
                Recipes[i].Ingredients.Add(
                    Ingredients.Single(x => x.Id == Recipes[i].RawIngredients.Keys.ElementAt(j)),
                    Recipes[i].RawIngredients.Values.ElementAt(j));

                var quantity = Recipes[i].RawIngredients.Values.ElementAt(j);
                quantity.Unit = Units.Single(x => x.Id == quantity.UnitId);
            }

        }

        json = File.ReadAllText(this.dataOptions.MenuDataStore);
        Menus = JsonSerializer.Deserialize<List<Menu>>(json);

        for (int i = 0; i < Menus.Count; i++)
        {
            for (int j = 0; j < Menus[i].Recipes.Count; j++)
            {
                Menus[i].Recipes[j].Recipe = Recipes.Single(x => x.Id == Menus[i].Recipes[j].RecipeId);
            }
        }
    }



    public List<Menu> Menus { get; init; } = new List<Menu>();

    public List<Recipe> Recipes { get; init; } = new List<Recipe>();

    public List<Ingredient> Ingredients { get; init; } = new List<Ingredient>();

    public List<Unit> Units { get; init; } = new List<Unit>();

    public void Save()
    {
        string json = JsonSerializer.Serialize(Units);
        File.WriteAllText(dataOptions.UnitDataStore, json);

        json = JsonSerializer.Serialize(Ingredients);
        File.WriteAllText(dataOptions.IngredientDataStore, json);

        json = JsonSerializer.Serialize(Recipes);
        File.WriteAllText(dataOptions.RecipeDataStore, json);

        json = JsonSerializer.Serialize(Menus);
        File.WriteAllText(dataOptions.MenuDataStore, json);
    }
}
