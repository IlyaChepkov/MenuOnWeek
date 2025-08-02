using System.Data;
using Application.Ingredients;
using Application.Units;
using MenuOnWeek.Frontend.Ingredient;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend;

public partial class IngredientForm : UserControl
{
    private List<UnitViewCommand> usingUnits = new();
    private IUnitService unitService;

    private IngredientViewCommand currentIngredient;

    public IngredientForm()
    {
        InitializeComponent();
        unitService = Program.ServiceProvider.GetRequiredService<IUnitService>();
        currentIngredient = new IngredientViewCommand()
        {
            Id = Guid.Empty,
            Name = "",
            Price = 0,
            UnitId = Guid.Empty,
            Table = new Dictionary<UnitViewCommand, double>()
        };
        usingUnits = new List<UnitViewCommand>();
    }

    public IngredientForm(IngredientViewCommand ingredient)
    {
        InitializeComponent();
        unitService = Program.ServiceProvider.GetRequiredService<IUnitService>();
        currentIngredient = ingredient;
        IngredientName.Text = ingredient.Name;
        UnitsList.Items.Add(unitService.Required().GetById(ingredient.UnitId, CancellationToken.None));
        UnitsList.SelectedIndex = 0;
        PriceNumericUpDown.Value = ingredient.Price;
        usingUnits = currentIngredient.Table.Keys.ToList();
        GridRefresh();
    }

    private void UnitsList_TextUpdate(object sender, EventArgs e)
    {
        UnitsList.Items.Clear();
        UnitsList.Items.AddRange(unitService.GetByNamePart(UnitsList.Text, 0, 5, CancellationToken.None).Result.ToArray());
        UnitsList.SelectionStart = UnitsList.Text.Length;
        UnitsList.DroppedDown = true;
    }

    private void UnitsList_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            var unit = unitService.GetByName(UnitsList.Text, CancellationToken.None);
            if (unit is not null)
            {
                currentIngredient.UnitId = unit.Result.Required().Id;
            }
            else
            {
                var createUnit = new CreateUnitCommand(UnitsList.Text);
                unitService.Add(createUnit, CancellationToken.None);
                UnitsList.Items.Add(new UnitViewCommand(unitService.GetByName(createUnit.Name.Required(), CancellationToken.None).Result.Required().Id, Name = createUnit.Name));
            }
        }
    }

    private void UnitsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var unit = UnitsList.SelectedItem as UnitViewCommand;

        if (currentIngredient.UnitId != unit.Required().Id)
        {
            for (int i = 0; i < currentIngredient.Table.Keys.Count; i++)
            {
                if (currentIngredient.Table.Keys.ElementAt(i).Id == unit.Required().Id)
                {
                    var removeRequest = currentIngredient.Table.Keys.ElementAt(i);
                    currentIngredient.Table.Add(unitService.GetById(currentIngredient.UnitId, CancellationToken.None).Result,
                        1 / currentIngredient.Table[removeRequest]);
                    currentIngredient.Table.Remove(removeRequest);

                    break;
                }
            }
        }

        currentIngredient.UnitId = unit.Required().Id;

        GridRefresh();
    }

    private void GridRefresh()
    {
        UnitsTable.Rows.Clear();
        UnitsTable.Rows
            .AddRange(new DataGridViewRow[currentIngredient.Table.Count]
                .Select(x => x = new DataGridViewRow()).ToArray());

        usingUnits = currentIngredient.Table.Keys.ToList();

        for (int i = 0; i < UnitsTable.Rows.Count; i++)
        {
            var comboBoxCell = (UnitsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();

            var dataSource = unitService.GetAll(0, 100, CancellationToken.None).Result.Where(x => x.Id != currentIngredient.UnitId
                    && usingUnits.All(y => y.Id != x.Id)).Select(x => x.Name).ToList();

            if (i + 1 < UnitsTable.Rows.Count)
            {
                dataSource.Add(usingUnits[i].Name);
            }

            comboBoxCell.DataSource = dataSource;


            if (i + 1 < UnitsTable.Rows.Count)
            {
                comboBoxCell.Value = usingUnits[i].Name;

                var valueCell = (UnitsTable.Rows[i].Cells[1] as DataGridViewCell).Required();

                valueCell.Value = currentIngredient.Table.Values.ElementAt(i);
            }
        }
    }

    internal IngredientDto GetIngredientDto()
    {
        Dictionary<Guid, double> table = new Dictionary<Guid, double>();

        for (int i = 0; i < UnitsTable.Rows.Count - 1; i++)
        {

            Guid unitId = Guid.Empty;
            if (UnitsTable.Rows[i].Cells[0].Value is not null)
            {
                unitId = unitService.GetByName(UnitsTable.Rows[i].Cells[0].Value.ToString().Required(), CancellationToken.None).Result.Required().Id;
            }
            double value = 0;
            if (UnitsTable.Rows[i].Cells[1].Value is not null)
            {
                Double.TryParse(UnitsTable.Rows[i].Cells[1].Value.ToString().Required().Replace('.', ','), out value);
            }
            if (table.All(x => x.Key != unitId))
            {
                table.Add(unitId, value);
            }
        }

        Guid id = Guid.Empty;
        if (UnitsList.SelectedItem is not null)
        {
            id = (UnitsList.SelectedItem as UnitViewCommand).Required().Id;
        }

        return new IngredientDto(
            IngredientName.Text,
            (int)PriceNumericUpDown.Value,
            id,
            table);
    }

    private void UnitsTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == 0)
        {
            if (UnitsTable.Rows[e.RowIndex].Cells[0].Value is not null)
            {
                if (e.RowIndex + 1 < usingUnits.Count)
                {
                    usingUnits.RemoveAt(e.RowIndex);
                }
                usingUnits.Add(unitService.GetByName(UnitsTable.Rows[e.RowIndex].Cells[0].Value.ToString().Required(), CancellationToken.None).Result.Required());

                for (int i = 0; i < UnitsTable.Rows.Count; i++)
                {
                    var cell = (UnitsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();

                    var list = unitService.GetAll(0, 100, CancellationToken.None).Result.Where(x => x.Id != currentIngredient.UnitId
                            && usingUnits.All(y => y.Id != x.Id)).Select(x => x.Name).ToList();

                    if (i + 1 < UnitsTable.Rows.Count)
                    {
                        list.Add((string)cell.Value);
                    }

                    cell.DataSource = list;
                }
            }
        }
    }

    private void UnitsTable_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
    {
        if (usingUnits.Count > 0 && UnitsTable.Rows.Count > 1)
        {
            usingUnits.RemoveAt(e.RowIndex);

            for (int i = 0; i < UnitsTable.Rows.Count; i++)
            {
                var cell = (UnitsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();

                var list = unitService.GetAll(0, 100, CancellationToken.None).Result.Where(x => x.Id != currentIngredient.UnitId
                        && usingUnits.All(y => y.Id != x.Id)).Select(x => x.Name).ToList();

                if (i + 1 < UnitsTable.Rows.Count)
                {
                    list.Add((string)cell.Value);
                }

                cell.DataSource = list;
            }
        }
    }


}
