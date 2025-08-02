using System.Windows.Forms;
using Application.Ingredients;
using MenuOnWeek.Application.Menus;
using MenuOnWeek.Application.Recipes;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend.Recipe;

public sealed partial class RecipeControl : UserControl
{
    private readonly IRecipeService recipeService;
    private readonly IMenuService menuService;
    private RecipeForm? recipeForm;

    public RecipeControl()
    {
        recipeService = Program.ServiceProvider.GetRequiredService<IRecipeService>();
        menuService = Program.ServiceProvider.GetRequiredService<IMenuService>();

        InitializeComponent();

        RefreshRecipesList();
    }

    private void RefreshRecipesList()
    {
        var recipes = recipeService.GetAll(0, 100, CancellationToken.None).Result.Required().Select(x => x.Name).OrderBy(x => x).ToArray();
        RecipesList.Items.Clear();
        RecipesList.Items.AddRange(recipes);
    }

    private void RecipesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RecipesList.SelectedItem is null)
        {
            return;
        }

        var recipe = recipeService.GetByName(RecipesList.SelectedItem.ToString().Required(), CancellationToken.None);

        if (recipeForm is not null)
        {
            Controls.Remove(recipeForm);
        }
        recipeForm = new RecipeForm(recipe.Result.Required());
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

            var updateRequest = new RecipeUpdateCommand()
            {
                Id = recipeService.GetByName((RecipesList.SelectedItem as string).Required(), CancellationToken.None).Result.Required().Id,
                Name = recipeDto.Name,
                Description = recipeDto.Description,
                Image = recipeDto.Image,
                Ingredients = recipeDto.Ingredients.Select(x => (x.Key, new QuantityCommand()
                {
                    UnitId = x.Value.UnitId,
                    Count = x.Value.Count
                })).ToDictionary(),
                IsImageChanged = recipeDto.IsImageChanged
            };
            recipeService.Update(updateRequest, CancellationToken.None);
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
                if (menuService.GetAll(0, 1000, CancellationToken.None).Result.Required().Any(x => x.Recipes.Any(y => y.RecipeId == recipeService.GetByName((RecipesList.SelectedItem as string).Required(), CancellationToken.None).Result.Required().Id)))
                {
                    statusStrip1.Items[0].Text = "Этот рецепт используется";
                    return;
                }
                recipeService.Remove(recipeService.GetByName(RecipesList.SelectedItem.Required().ToString().Required(), CancellationToken.None).Result.Required().Id, CancellationToken.None);

                RefreshRecipesList();
                Controls.Remove(recipeForm);
            }
        }
    }
}
