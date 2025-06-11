using Application.Ingredients;
using MenuOnWeek.Application.Recipes;
using Microsoft.Extensions.DependencyInjection;

namespace MenuOnWeek.Frontend.Recipe;

public partial class AddRecipeForm : Form
{
    private IRecipeService recipeService;
    private RecipeForm recipeForm;
    public AddRecipeForm()
    {
        recipeService = Program.ServiceProvider.GetRequiredService<IRecipeService>();
        InitializeComponent();
        recipeForm = new RecipeForm();
        Controls.Add(recipeForm);
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        var recipeDto = recipeForm.GetRecipeDto();

        var createRequest = new RecipeCreateModel()
        {
            Name = recipeDto.Name,
            Description = recipeDto.Description,
            Image = recipeDto.Image,
            Ingredients = recipeDto.Ingredients.Select(x => (x.Key, new QuantityModel()
                {
                Count = x.Value.Count,
                UnitId = x.Value.UnitId
            })).ToDictionary(x => x.Item1, x => x.Item2)

        };



        recipeService.Add(createRequest);
        Close();
    }
}
