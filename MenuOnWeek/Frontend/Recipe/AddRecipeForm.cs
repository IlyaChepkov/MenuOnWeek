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

        if (String.IsNullOrEmpty(recipeDto.Name))
        {
            statusStrip1.Items[0].Text = "У рецепта нет имени";
            return;
        }
        if (recipeDto.Ingredients.Count < 1)
        {
            statusStrip1.Items[0].Text = "У рецепта нет ингредиентов";
            return;
        }
        if (recipeDto.Ingredients.Any(x => x.Key == Guid.Empty || x.Value.Count == 0 || x.Value.UnitId == Guid.Empty))
        {
            statusStrip1.Items[0].Text = "Заполнены не все ячейки таблицы";
            return;
        }

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
