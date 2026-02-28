using MenuOnWeek.Clients.Ingredients;
using MenuOnWeek.Clients.Recipes;
using MenuOnWeek.Contracts.Ingredients;
using MenuOnWeek.Frontend.Ingredient;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend;

public partial class IngredientsControl : UserControl
{
    private IngredientForm? ingredientForm;
    private readonly IRecipeClient recipeClient;
    private readonly IIngredientClient ingredientClient;

    public IngredientsControl()
    {
        recipeClient = Program.ServiceProvider.GetRequiredService<IRecipeClient>();
        ingredientClient = Program.ServiceProvider.GetRequiredService<IIngredientClient>();
        InitializeComponent();

        RefreshIngridentList();
    }

    private void RefreshIngridentList()
    {
        var ingredientsTask = ingredientClient.GetAll(0, 100, CancellationToken.None);
        IngredientsList.Items.Clear();
        ingredientsTask.Wait();
        IReadOnlyList<IngredientResponse> ingredients = ingredientsTask.Result.Required();
        IngredientsList.Items.AddRange(ingredients.Select(x => x)
            .OrderBy(x => x.Name)
            .Select(x => x.Name.Required())
            .ToArray());
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        AddIngredientForm ingredientForm = new AddIngredientForm();
        ingredientForm.ShowDialog();

        RefreshIngridentList();
    }

    private void IngredientsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IngredientsList.SelectedItem is null)
        {
            return;
        }

        var ingredient = ingredientClient.GetByName(
            IngredientsList.SelectedItem.ToString().Required(),
            CancellationToken.None).Result;

        if (ingredientForm is not null)
        {
            Controls.Remove(ingredientForm);
        }
        ingredientForm = new IngredientForm(ingredient.Required());
        ingredientForm.Location = new Point(120, 5);
        Controls.Add(ingredientForm);
    }

    private void UpdateButton_Click(object sender, EventArgs e)
    {

        if (ingredientForm is null)
        {
            return;
        }

        IngredientDto ingredientDto = ingredientForm.GetIngredientDto();

        if (String.IsNullOrEmpty(ingredientDto.Name))
        {
            statusStrip1.Items[0].Text = "У ингредиента нет имени";
            return;
        }
        if (ingredientDto.Price == 0)
        {
            statusStrip1.Items[0].Text = "У ингредиента нет цены";
            return;
        }
        if (Guid.Empty == ingredientDto.UnitId)
        {
            statusStrip1.Items[0].Text = "У ингредиента нет единицы измерения";
            return;
        }
        if (ingredientDto.Table.Any(x => x.Key == Guid.Empty || x.Value == 0))
        {
            statusStrip1.Items[0].Text = "Заполнены не все ячейки таблицы";
            return;
        }

        statusStrip1.Items[0].Text = "";

        IngredientUpdateRequest ingredient = new IngredientUpdateRequest()
        {
            Id = ingredientClient.GetByName(IngredientsList.SelectedItem.Required().ToString().Required(), CancellationToken.None).Result.Required().Id,
            Name = ingredientDto.Name,
            Price = ingredientDto.Price,
            UnitId = ingredientDto.UnitId,
            Units = ingredientDto.Table.Select(x => new IngredientUnitsCreateOrUpdateRequest() { UnitId = x.Key, Coeficient = x.Value }).ToList()
        };

        ingredientClient.Update(ingredient, CancellationToken.None);

        int index = IngredientsList.SelectedIndex;

        RefreshIngridentList();
        IngredientsList.SelectedIndex = index;

    }

    private void IngredientsList_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Delete)
        {
            if (IngredientsList.SelectedItem is not null)
            {
                if (recipeClient.GetAll(0, 1000, CancellationToken.None).Result.Required()
            .Any(x => x.Ingredients
                .Any(y => y.IngredientId ==
                    ingredientClient.GetByName(IngredientsList.SelectedItem.ToString().Required(),
                    CancellationToken.None).Result.Required().Id)))

                {
                    statusStrip1.Items[0].Text = "Этот элемент используется";
                    return;
                }
                ingredientClient.Remove(ingredientClient.GetByName(IngredientsList.SelectedItem.ToString().Required(), CancellationToken.None).Result.Required().Id, CancellationToken.None);

                RefreshIngridentList();
                Controls.Remove(ingredientForm);
            }
        }
    }
}
