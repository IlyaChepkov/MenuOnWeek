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
using Microsoft.Extensions.DependencyInjection;

namespace MenuOnWeek.Frontend.Ingredient;

public sealed partial class AddIngredientForm : Form
{

    IngredientForm ingredientForm;
    IIngredientService ingredientService;

    public AddIngredientForm()
    {
        ingredientService = Program.ServiceProvider.GetRequiredService<IIngredientService>();
        InitializeComponent();

        ingredientForm = new IngredientForm();
        Controls.Add(ingredientForm);


    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        var ingredientDto = ingredientForm.GetIngredientDto();
        var createRequest = new CreateIngredientModel()
        {
            Name = ingredientDto.Name,
            Price = ingredientDto.Price,
            UnitId = ingredientDto.UnitId,
            Table = ingredientDto.Table
        };

        ingredientService.Add(createRequest);

        Close();
    }
}
