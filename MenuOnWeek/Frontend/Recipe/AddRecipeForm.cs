using MenuOnWeek.Clients.Recipes;
using MenuOnWeek.Contracts.Recipes;
using Microsoft.Extensions.DependencyInjection;

namespace MenuOnWeek.Frontend.Recipe;

public partial class AddRecipeForm : Form
{
    private IRecipeClient recipeClient;
    private RecipeForm recipeForm;
    public AddRecipeForm()
    {
        recipeClient = Program.ServiceProvider.GetRequiredService<IRecipeClient>();
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

        var createRequest = new RecipeCreateRequest()
        {

            Name = recipeDto.Name,
            Image = recipeDto.Image,
            Description = recipeDto.Description,
            Ingredients = recipeDto.Ingredients.Select(x => new RecipeIngredientsCreateOrUpdateRequest()
            {
                IngredientId = x.Key,
                UnitId = x.Value.UnitId,
                Count = x.Value.Count
            }).ToList()

        };



        recipeClient.Add(createRequest, CancellationToken.None);
        Close();
    }
}
