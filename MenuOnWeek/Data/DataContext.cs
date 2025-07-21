using Domain;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Utils;

namespace Data;

internal sealed class DataContext
{

    private DataOptions dataOptions;
    public List<Menu> Menus { get; init; } = new List<Menu>();

    public List<Recipe> Recipes { get; init; } = new List<Recipe>();

    public List<Ingredient> Ingredients { get; init; } = new List<Ingredient>();

    public List<Unit> Units { get; init; } = new List<Unit>();

    public DataContext(IOptions<DataOptions> dataOptions)
    {
        this.dataOptions = dataOptions.Value;

        string json = File.ReadAllText(this.dataOptions.UnitDataStore.Required());
        Units = JsonSerializer.Deserialize<List<Unit>>(json).Required();

        json = File.ReadAllText(this.dataOptions.IngredientDataStore.Required());
        Ingredients = JsonSerializer.Deserialize<List<Ingredient>>(json).Required();

        for (int i = 0; i < Ingredients.Count; i++)
        {
            Ingredients[i].Unit = Units.Single(x => x.Id == Ingredients[i].UnitId);

            for (int j = 0; j < Ingredients[i].RowTable.Count; j++)
            {
                Ingredients[i].Table.Add(
                    Units.Single(x => x.Id == Ingredients[i].RowTable.Keys.ElementAt(j)),
                    Ingredients[i].RowTable.Values.ElementAt(j));
            }
        }

        json = File.ReadAllText(this.dataOptions.RecipeDataStore.Required());
        Recipes = JsonSerializer.Deserialize<List<Recipe>>(json).Required();

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

        json = File.ReadAllText(this.dataOptions.MenuDataStore.Required());
        Menus = JsonSerializer.Deserialize<List<Menu>>(json).Required();

        for (int i = 0; i < Menus.Count; i++)
        {
            for (int j = 0; j < Menus[i].Recipes.Count; j++)
            {
                Menus[i].Recipes[j].Recipe = Recipes.Single(x => x.Id == Menus[i].Recipes[j].RecipeId);
            }
        }
    }

    public void Save()
    {
        string json = JsonSerializer.Serialize(Units);
        File.WriteAllText(dataOptions.UnitDataStore.Required(), json);

        Ingredients.ForEach(x => x.RowTable = x.Table.Select(y => (y.Key.Id, y.Value)).ToDictionary());

        json = JsonSerializer.Serialize(Ingredients);
        File.WriteAllText(dataOptions.IngredientDataStore.Required(), json);

        Recipes.ForEach(x => x.RawIngredients = x.Ingredients.Select(y => (y.Key.Id, y.Value)).ToDictionary());

        json = JsonSerializer.Serialize(Recipes);
        File.WriteAllText(dataOptions.RecipeDataStore.Required(), json);

        Menus.ForEach(x => x.Recipes.ForEach(y => y.RecipeId = y.Recipe.Id));

        json = JsonSerializer.Serialize(Menus);
        File.WriteAllText(dataOptions.MenuDataStore.Required(), json);
    }
}
