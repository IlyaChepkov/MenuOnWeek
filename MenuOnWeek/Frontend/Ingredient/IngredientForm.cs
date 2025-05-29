using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application.Ingredients;
using Application.Units;
using Domain;
using MenuOnWeek.Frontend.Ingredient;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend;

public partial class IngredientForm : UserControl
{
    private Dictionary<UnitViewModel, double> usingUnits;
    private IUnitService unitService;

    private IngredientViewModel currentIngredient;

    public IngredientForm()
    {
        InitializeComponent();
        unitService = Program.ServiceProvider.GetRequiredService<IUnitService>();
        currentIngredient = new IngredientViewModel()
        {
            Id = Guid.Empty,
            Name = "",
            Price = 0,
            UnitId = Guid.Empty,
            Table = new Dictionary<UnitViewModel, double>()
        };
        usingUnits = new Dictionary<UnitViewModel, double>();
    }

    public IngredientForm(IngredientViewModel ingredient)
    {
        InitializeComponent();
        unitService = Program.ServiceProvider.GetRequiredService<IUnitService>();
        currentIngredient = ingredient;
        IngredientName.Text = ingredient.Name;
        UnitsList.Items.Add(unitService.Required().GetById(ingredient.UnitId));
        UnitsList.SelectedIndex = 0;
        PriceNumericUpDown.Value = ingredient.Price;
        usingUnits = currentIngredient.Table;
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
                currentIngredient.UnitId = unit.Id;
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

        if (currentIngredient.UnitId != unit.Required().Id)
        {
            for (int i = 0; i < currentIngredient.Table.Keys.Count; i++)
            {
                if (currentIngredient.Table.Keys.ElementAt(i).Id == unit.Required().Id)
                {
                    var removeRequest = currentIngredient.Table.Keys.ElementAt(i);
                    currentIngredient.Table.Add(unitService.GetById(currentIngredient.UnitId),
                        1 / currentIngredient.Table[removeRequest]);
                    currentIngredient.Table.Remove(removeRequest);
                    
                    break;
                }
            }
        }

        currentIngredient.UnitId = unit.Required().Id;

       GridRefresh();
    } //

    private void GridRefresh()
    {
        usingUnits = currentIngredient.Table;
        UnitsTable.Rows.Clear();

        UnitsTable.Rows
            .AddRange(new DataGridViewRow[currentIngredient.Table.Count]
                .Select(x => x = new DataGridViewRow()).ToArray());

        for (int i = 0; i < UnitsTable.Rows.Count; i++)
        {
            var comboBoxCell = (UnitsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();
            comboBoxCell.DataSource = unitService
                .GetAll(0, 100)
                .Where(x => x.Id != currentIngredient.UnitId && usingUnits.All(y => y.Key != x || usingUnits.Keys.ElementAt(i) == x))
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
                unitService.GetAll(0, 100).Where(x => x.Id != currentIngredient.UnitId).Select(x => x.Name).ToList();
    }

    internal IngredientDto GetIngredientDto()
    {
        Dictionary<UnitViewModel, double> table = new Dictionary<UnitViewModel, double>();

        for (int i = 0; i < UnitsTable.Rows.Count - 1; i++)
        {
            UnitViewModel unit = unitService.GetByName(UnitsTable.Rows[i].Cells[0].Value.ToString().Required()).Required();
            double value = Double.Parse(UnitsTable.Rows[i].Cells[1].Value.ToString().Required().Replace('.', ','));

            table.Add(unit, value);
        }

        return new IngredientDto(
            IngredientName.Text,
            (int)PriceNumericUpDown.Value,
            (UnitsList.SelectedItem as UnitViewModel).Required().Id,
            table);
    }
}
