using Application.Ingredients;
using MenuOnWeek.Frontend.Ingredient;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend;

public partial class IngredientsControl : UserControl
{
    private readonly IIngredientService ingredientService;
    private IngredientForm? ingredientForm;

    public IngredientsControl()
    {
        ingredientService = Program.ServiceProvider.GetRequiredService<IIngredientService>();

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

        IngredientDto dto = ingredientForm.GetIngredientDto();

        UpdateIngredientModel ingredient = new UpdateIngredientModel()
        {
            Id = (IngredientsList.SelectedItem as IngredientViewModel).Required().Id,
            Name = dto.Name,
            Price = dto.Price,
            UnitId = dto.UnitId,
            Table = dto.Table
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
                ingredientService.Remove((IngredientsList.SelectedItem as IngredientViewModel).Required().Id);

                RefreshIngridentList();
                Controls.Remove(ingredientForm);
            }
        }
    }
}
