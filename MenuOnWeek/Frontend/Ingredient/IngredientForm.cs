using System.Data;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using MenuOnWeek.Clients.Units;
using MenuOnWeek.Contracts.Ingredients;
using MenuOnWeek.Contracts.Units;
using MenuOnWeek.Frontend.Ingredient;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend;

public partial class IngredientForm : UserControl
{
    private List<UnitResponse> usingUnits = new();
    private readonly IUnitClient unitClient;

    private IngredientResponse currentIngredient;

    public IngredientForm()
    {
        unitClient = Program.ServiceProvider.GetRequiredService<IUnitClient>();

        InitializeComponent();
        
        currentIngredient = new IngredientResponse()
        {

            Id = Guid.Empty,
            Name = "",
            Price = 0,
            UnitId = Guid.Empty,
            Units = new List<IngredientUnitsResponse>()
        };
        usingUnits = new List<UnitResponse>();
    }

    public IngredientForm(IngredientResponse ingredient)
    {
        unitClient = Program.ServiceProvider.GetRequiredService<IUnitClient>();

        InitializeComponent();

        currentIngredient = ingredient;
        IngredientName.Text = ingredient.Name;
        var unitTask = unitClient.Required().GetById(ingredient.UnitId, CancellationToken.None).Required();
        UnitsList.Items.Add(unitTask.Result.Name.Required());
        UnitsList.SelectedIndex = 0;
        PriceNumericUpDown.Value = ingredient.Price.Required();
        usingUnits = currentIngredient.Units.Select(x => unitClient.GetById(x.UnitId, CancellationToken.None).Result).ToList();

        GridRefresh();
    }

    private void UnitsList_TextUpdate(object sender, EventArgs e)
    {
        var names = unitClient.GetByNamePart(UnitsList.Text, 0, 10, CancellationToken.None).Result;

        UnitsList.Items.Clear();
        UnitsList.Items.AddRange(names.Select(x => x.Name.Required()).ToArray());
        UnitsList.SelectionStart = UnitsList.Text.Length;
        UnitsList.DroppedDown = true;
    }

    private void UnitsList_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {

            UnitResponse? unit = unitClient.GetByName(UnitsList.Text, CancellationToken.None).Result;
            if (unit is not null)
            {
                currentIngredient.UnitId = unit.Required().Id;
            }
            else
            {
                var createUnit = new UnitCreateRequest() { Name = UnitsList.Text };
                unitClient.Add(createUnit, CancellationToken.None);
                UnitResponse response = unitClient.GetByName(UnitsList.Text, CancellationToken.None).Result.Required();
                UnitsList.Items.Add(new UnitResponse()
                {
                    Id = unitClient.GetByName(createUnit.Name.Required(), CancellationToken.None).Result.Required().Id.Required(),
                    Name = createUnit.Name
                });
                currentIngredient.UnitId = response.Id;
            }
        }
    }

    private void UnitsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedUnit = UnitsList.SelectedItem;
        string name;
        if (selectedUnit == null)
        {
            name = UnitsList.Text;
        }
        else
        {
           name = selectedUnit.ToString().Required();
        }

        UnitResponse? unit = unitClient.GetByName(
            name,
            CancellationToken.None).Result;

        if (unit is not null && currentIngredient.UnitId != unit.Required().Id)
        {
            /*for (int i = 0; i < currentIngredient.IngredientUnits.Keys.Count; i++)
            {
                if (currentIngredient.IngredientUnits.Keys.ElementAt(i).Id == unit.Required().Id)
                {
                    var removeRequest = currentIngredient.IngredientUnits.Keys.ElementAt(i);
                    currentIngredient.IngredientUnits.Add(unitService.GetById(currentIngredient.UnitId, CancellationToken.None).Result,
                        1 / currentIngredient.IngredientUnits[removeRequest]);
                    currentIngredient.IngredientUnits.Remove(removeRequest);

                    break;
                }
            }
            */
        }

        //currentIngredient.UnitId = unit.Required().Id;

        GridRefresh();
    }

    private void GridRefresh()
    {
        UnitsTable.Rows.Clear();
        UnitsTable.Rows
            .AddRange(new DataGridViewRow[currentIngredient.Units.Count]
                .Select(x => x = new DataGridViewRow()).ToArray());

        usingUnits = currentIngredient.Units.Select(x => unitClient.GetById(x.UnitId, CancellationToken.None).Result).ToList();

        for (int i = 0; i < UnitsTable.Rows.Count; i++)
        {
            var comboBoxCell = (UnitsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();

            var dataSource = unitClient.Get(0, 100, CancellationToken.None).Result.Where(x => x.Id != currentIngredient.UnitId
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

                valueCell.Value = currentIngredient.Units[i].Coeficient;
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
                unitId = unitClient.GetByName(UnitsTable.Rows[i].Cells[0].Value.ToString().Required(), CancellationToken.None).Result.Required().Id.Required();
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
            var name = UnitsList.SelectedItem.Required().ToString();
            id = unitClient.GetByName(
                name,
                CancellationToken.None).Result.Required().Id.Required();
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
                usingUnits.Add(unitClient.GetByName(UnitsTable.Rows[e.RowIndex].Cells[0].Value.ToString().Required(), CancellationToken.None).Result.Required());

                for (int i = 0; i < UnitsTable.Rows.Count; i++)
                {
                    var cell = (UnitsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();

                    var list = unitClient.Get(0, 100, CancellationToken.None).Result.Where(x => x.Id != currentIngredient.UnitId
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

                var list = unitClient.Get(0, 100, CancellationToken.None).Result.Where(x => x.Id != currentIngredient.UnitId
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
