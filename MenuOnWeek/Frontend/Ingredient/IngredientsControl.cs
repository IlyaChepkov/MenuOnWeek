using Application.Ingredients;
using Application.Units;
using MenuOnWeek.Application.Recipes;
using MenuOnWeek.Domain;
using MenuOnWeek.Frontend.Ingredient;
using MenuOnWeek.Frontend.Menu;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend;

public partial class IngredientsControl : UserControl
{
    private readonly IRecipeService recipeService;
    private readonly IIngredientService ingredientService;
    private readonly IUnitService unitService;
    private IngredientForm? ingredientForm;

    public IngredientsControl()
    {
        unitService = Program.ServiceProvider.GetRequiredService<IUnitService>();
        ingredientService = Program.ServiceProvider.GetRequiredService<IIngredientService>();
        recipeService = Program.ServiceProvider.GetRequiredService<IRecipeService>();

        InitializeComponent();

        RefreshIngridentList();
    }

    private void RefreshIngridentList()
    {
        var ingredients = ingredientService.GetAll(0, 100).ToArray();
        IngredientsList.Items.Clear();
        IngredientsList.Items.AddRange(ingredients);
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

        var ingredient = IngredientsList.SelectedItem as IngredientViewModel;

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

        UpdateIngredientModel ingredient = new UpdateIngredientModel()
        {
            Id = (IngredientsList.SelectedItem as IngredientViewModel).Required().Id,
            Name = ingredientDto.Name,
            Price = ingredientDto.Price,
            UnitId = ingredientDto.UnitId,
            Table = ingredientDto.Table.Select(x => (unitService.GetById(x.Key), x.Value)).ToDictionary()
        };

        ingredientService.Update(ingredient);

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
                if (recipeService.GetAll(0, 1000)
            .Any(x => x.Ingredients.Keys.Any(y => y == (IngredientsList.SelectedItem as IngredientViewModel).Required().Id)))
                {
                    statusStrip1.Items[0].Text = "Этот элемент используется";
                    return;
                }
                ingredientService.Remove((IngredientsList.SelectedItem as IngredientViewModel).Required().Id);

                RefreshIngridentList();
                Controls.Remove(ingredientForm);
            }
        }
    }
}
