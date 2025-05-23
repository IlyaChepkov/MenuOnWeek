using System.Data;
using System.Xaml.Permissions;
using Application.Ingredients;
using Application.Units;
using MenuOnWeek.Frontend.Ingredient;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend;

public partial class IngredientsControl : UserControl
{
    private readonly IIngredientService ingredientService;
    private readonly IUnitService unitService;
    private IngredientForm? ingredientForm;

    public IngredientsControl()
    {
        ingredientService = Program.ServiceProvider.GetRequiredService<IIngredientService>();
        unitService = Program.ServiceProvider.GetRequiredService<IUnitService>();

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
        /*
        CreateIngredientModel createIngredientModel = new CreateIngredientModel()
        {
            Name = IngredientName.Text,
            UnitId = currentUnitId,
            Price = (int)PriceNumericUpDown.Value,
            Table = new Dictionary<UnitViewModel, double>()
        };

        for (int i = 0; i < UnitsTable.Rows.Count - 1; i++)
        {
            UnitViewModel unit = unitService.GetByName(UnitsTable.Rows[i].Cells[0].Value.ToString().Required()).Required();
            double value = Double.Parse(UnitsTable.Rows[i].Cells[1].Value.ToString().Required().Replace('.', ','));

            createIngredientModel.Table.Add(unit, value);
        }

        ingredientService.Add(createIngredientModel);

        RefreshIngridentList();
        */
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

        IngredientDto dto = ingredientForm.GetIngredientDTO();
        
        UpdateIngredientModel ingredient = new UpdateIngredientModel()
        {
            Id = (IngredientsList.SelectedItem as IngredientViewModel).Required().Id,
            Name = dto.Name,
            Price = dto.Price,
            UnitId = dto.UnitId,
            Table = dto.Table
        };
        /*
        for (int i = 0; i < UnitsTable.Rows.Count - 1; i++)
        {
            UnitViewModel unit = unitService.GetByName(UnitsTable.Rows[i].Cells[0].Value.ToString().Required()).Required();
            double value = Double.Parse(UnitsTable.Rows[i].Cells[1].Value.ToString().Required().Replace('.', ','));

            ingredient.Table.Add(unit, value);
        }
        */

        ingredientService.Update(ingredient);

        int index = IngredientsList.SelectedIndex;

        RefreshIngridentList();
        IngredientsList.SelectedIndex = index;
        
    }

    private void IngredientsList_KeyDown(object sender, KeyEventArgs e)
    {
        /*
        if (e.KeyCode == Keys.Delete)
        {
            ingredientService.Remove(CurrentIngredient.Id);
            IngredientName.Clear();
            PriceNumericUpDown.Value = PriceNumericUpDown.Minimum;
            UnitsList.Items.Clear();
            UnitsList.Text = "";

            RefreshIngridentList();
            GridRefresh();
        }
        */
    }
}
