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
using MenuOnWeek.Application.Recipes;
using Microsoft.Extensions.DependencyInjection;

namespace MenuOnWeek.Frontend.Ingredient;

public sealed partial class AddIngredientForm : Form
{

    IngredientForm ingredientForm;
    IIngredientService ingredientService;
    IUnitService unitService;

    IRecipeService recipeService;

    public AddIngredientForm()
    {
        unitService = Program.ServiceProvider.GetRequiredService<IUnitService>();
        ingredientService = Program.ServiceProvider.GetRequiredService<IIngredientService>();
        recipeService = Program.ServiceProvider.GetRequiredService<IRecipeService>();
        InitializeComponent();

        ingredientForm = new IngredientForm();
        Controls.Add(ingredientForm);


    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        var ingredientDto = ingredientForm.GetIngredientDto();

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

        

        var createRequest = new CreateIngredientModel()
        {
            Name = ingredientDto.Name,
            Price = ingredientDto.Price,
            UnitId = ingredientDto.UnitId,
            Table = ingredientDto.Table.Select(x => (unitService.GetById(x.Key), x.Value)).ToDictionary()
        };

        ingredientService.Add(createRequest);

        Close();
    }
}
