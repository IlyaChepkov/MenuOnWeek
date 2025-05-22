using System.Data;
using System.Xaml.Permissions;
using Application.Ingredients;
using Application.Units;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace Frontend;

public partial class IngredientsControl : UserControl
{

    private Guid currentUnitId;
    private readonly IIngredientService ingredientService;
    private readonly IUnitService unitService;
    private Dictionary<UnitViewModel, double> usingUnits;


    private IngredientViewModel CurrentIngredient => IngredientsList.SelectedItem as IngredientViewModel ?? new IngredientViewModel()
    {
        Id = Guid.Empty,
        Name = "Ингредиент по умолчанию",
        Price = 0,
        UnitId = Guid.Empty,
        Table = new Dictionary<UnitViewModel, double>()
    };

    public IngredientsControl()
    {
        ingredientService = Program.ServiceProvider.GetRequiredService<IIngredientService>();
        unitService = Program.ServiceProvider.GetRequiredService<IUnitService>();
        usingUnits = new Dictionary<UnitViewModel, double>();

        InitializeComponent();

        RefreshIngridentList();

        GridRefresh();
    }

    private void RefreshIngridentList()
    {
        var ingredients = ingredientService.GetAll(0, 100).ToArray();
        IngredientsList.Items.Clear();
        IngredientsList.Items.AddRange(ingredients);
    }

    private void AddButton_Click(object sender, EventArgs e)
    {

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
    }

    private void IngredientsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IngredientsList.SelectedItem is null)
        {
            return;
        }

        var ingredient = IngredientsList.SelectedItem as IngredientViewModel;

        IngredientName.Text = ingredient.Required().Name;
        PriceNumericUpDown.Value = ingredient.Required().Price;

        var unit = unitService.GetById(ingredient.Required().UnitId);
        UnitsList.SelectedItem = unit;
        currentUnitId = unit.Id;
        UnitsList.Text = unitService.GetById(currentUnitId).ToString();

        GridRefresh();
    }

    private void UnitsList_TextUpdate(object sender, EventArgs e)
    {
        UnitsList.Items.Clear();
        UnitsList.Items.AddRange(unitService.GetByNamePart(UnitsList.Text, 0, 5).ToArray());
        UnitsList.SelectionStart = UnitsList.Text.Length;
        UnitsList.DroppedDown = true;
    }

    private void UnitsList_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            var unit = unitService.GetByName(UnitsList.Text);
            if (unit is not null)
            {
                currentUnitId = unit.Id;
            }
            else
            {
                var createUnit = new CreateUnitModel()
                {
                    Name = UnitsList.Text
                };
                unitService.Add(createUnit);
                UnitsList.Items.Add(new UnitViewModel()
                { Id = unitService.GetByName(createUnit.Name).Required().Id, Name = createUnit.Name });
            }
        }
    }

    private void UnitsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var unit = UnitsList.SelectedItem as UnitViewModel;
        currentUnitId = unit.Required().Id;
        GridRefresh();
    }

    private void GridRefresh()
    {
        usingUnits = CurrentIngredient.Table;
        UnitsTable.Rows.Clear();

        UnitsTable.Rows
            .AddRange(new DataGridViewRow[CurrentIngredient.Table.Count]
                .Select(x => x = new DataGridViewRow()).ToArray());

        for (int i = 0; i < UnitsTable.Rows.Count; i++)
        {
            var comboBoxCell = (UnitsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();
            comboBoxCell.DataSource = unitService
                .GetAll(0, 100)
                .Where(x => x.Id != currentUnitId && usingUnits.All(y => y.Key != x || usingUnits.Keys.ElementAt(i) == x))
                .Select(x => x.Name)
                .ToList();

            if (i + 1 < UnitsTable.Rows.Count)
            {
                comboBoxCell.Value = usingUnits.Keys.ElementAt(i).Name;

                var valueCell = (UnitsTable.Rows[i].Cells[1] as DataGridViewCell).Required();

                valueCell.Value = usingUnits.Values.ElementAt(i);
            }
        }
    }

    private void UnitsTable_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
        (UnitsTable.Rows[^1].Cells[0] as DataGridViewComboBoxCell).Required().DataSource =
                unitService.GetAll(0, 100).Where(x => x.Id != currentUnitId).Select(x => x.Name).ToList();
    }

    private void UpdateButton_Click(object sender, EventArgs e)
    {
        UpdateIngredientModel ingredient = new UpdateIngredientModel()
        {
            Id = CurrentIngredient.Id,
            Name = IngredientName.Text,
            Price = (int)PriceNumericUpDown.Value,
            Table = new Dictionary<UnitViewModel, double>(),
            UnitId = currentUnitId
        };

        for (int i = 0; i < UnitsTable.Rows.Count - 1; i++)
        {
            UnitViewModel unit = unitService.GetByName(UnitsTable.Rows[i].Cells[0].Value.ToString().Required()).Required();
            double value = Double.Parse(UnitsTable.Rows[i].Cells[1].Value.ToString().Required().Replace('.', ','));

            ingredient.Table.Add(unit, value);
        }

        ingredientService.Update(ingredient);

        int index = IngredientsList.SelectedIndex;

        RefreshIngridentList();
        IngredientsList.SelectedIndex = index;

        GridRefresh();
    }

    private void IngredientsList_KeyDown(object sender, KeyEventArgs e)
    {
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
    }
}
