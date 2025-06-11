using Application.Ingredients;
using MenuOnWeek.Application.Recipes;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend.Recipe;

public sealed partial class RecipeControl : UserControl
{
    private readonly IRecipeService recipeService;
    private RecipeForm? recipeForm;

    public RecipeControl()
    {
        recipeService = Program.ServiceProvider.GetRequiredService<IRecipeService>();

        InitializeComponent();

        RefreshRecipesList();
    }

    private void RefreshRecipesList()
    {
        var recipes = recipeService.GetAll(0, 100).Select(x => x.Name).ToArray();
        RecipesList.Items.Clear();
        RecipesList.Items.AddRange(recipes);
    }

    private void RecipesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RecipesList.SelectedItem is null)
        {
            return;
        }

        var recipe = recipeService.GetByName(RecipesList.SelectedItem.ToString().Required());

        if (recipeForm is not null)
        {
            Controls.Remove(recipeForm);
        }
        recipeForm = new RecipeForm(recipe.Required());
        recipeForm.Location = new Point(135, 3);
        Controls.Add(recipeForm);
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        var addRecipeForm = new AddRecipeForm();
        addRecipeForm.ShowDialog();

        RefreshRecipesList();
    }

    private void UpdateButton_Click(object sender, EventArgs e)
    {
        if (recipeForm is not null)
        {
            var recipeDto = recipeForm.GetRecipeDto();

            var updateRequest = new RecipeUpdateModel()
            {
                Id = recipeService.GetByName((RecipesList.SelectedItem as string).Required()).Id,
                Name = recipeDto.Name,
                Description = recipeDto.Description,
                Image = recipeDto.Image,
                Ingredients = recipeDto.Ingredients.Select(x => (x.Key, new QuantityModel()
                {
                    UnitId = x.Value.UnitId,
                    Count = x.Value.Count
                })).ToDictionary(),
                IsImageChanged = recipeDto.IsImageChanged
            };
            recipeService.Update(updateRequest);
            int recipeIndex = RecipesList.SelectedIndex;
            RefreshRecipesList();
            RecipesList.SelectedIndex = recipeIndex;
        }
    }

    private void RecipesList_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Delete)
        {
            if (RecipesList.SelectedItem is not null)
            {
                recipeService.Remove(recipeService.GetByName(RecipesList.SelectedItem.Required().ToString().Required()).Id);

                RefreshRecipesList();
                Controls.Remove(recipeForm);
            }
        }
    }
}
