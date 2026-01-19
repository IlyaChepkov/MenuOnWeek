using MenuOnWeek.Clients.Ingredients;
using MenuOnWeek.Contracts.Ingredients;
using Microsoft.Extensions.DependencyInjection;

namespace MenuOnWeek.Frontend.Ingredient;

public sealed partial class AddIngredientForm : Form
{

    IngredientForm ingredientForm;
    IIngredientClient ingredientClient;


    public AddIngredientForm()
    {
        ingredientClient = Program.ServiceProvider.GetRequiredService<IIngredientClient>();
        InitializeComponent();

        ingredientForm = new IngredientForm();
        Controls.Add(ingredientForm);


    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        var ingredientDto = ingredientForm.GetIngredientDto();

        var createRequest = new IngredientCreateRequest()
        {
            Name = ingredientDto.Name,
            Price = ingredientDto.Price,
            UnitId = ingredientDto.UnitId,
            Units = ingredientDto.Table.Select(x => new IngredientUnitsCreateOrUpdateRequest() { UnitId = x.Key, Coeficient = x.Value }).ToList()
        };

        ingredientClient.Add(createRequest, CancellationToken.None);

        Close();
    }
}
