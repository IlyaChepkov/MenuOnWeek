using Application.Ingredients;
using Application.Units;
using MenuOnWeek.Application.Recipes;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend.Recipe;

public partial class RecipeForm : UserControl
{

    private IRecipeService recipeService;
    private IIngredientService ingredientService;
    private IUnitService unitService;
    private RecipeViewModel currentRecipe;
    private string? currentImage;
    private bool isImageChanged = false;
    private Image defaultImage;

    private List<IngredientViewModel> usingIngredients = new List<IngredientViewModel>();

    public RecipeForm()
    {
        defaultImage = System.Drawing.Image.
                FromFile("C:\\Users\\user\\Documents\\MenuOnWeek\\MenuOnWeek\\Frontend\\no-photo--lg.png");
        recipeService = Program.ServiceProvider.GetRequiredService<IRecipeService>();
        ingredientService = Program.ServiceProvider.GetRequiredService<IIngredientService>();
        unitService = Program.ServiceProvider.GetRequiredService<IUnitService>();
        InitializeComponent();

        currentRecipe = new RecipeViewModel()
        {
            Id = Guid.Empty,
            Name = "",
            Image = null,
            Description = "",
            Price = 0,
            Ingredients = new Dictionary<Guid, QuantityViewModel>()
        };
        GridRefresh();
    }

    public RecipeForm(RecipeViewModel recipe)
    {
        defaultImage = System.Drawing.Image.
                FromFile("C:\\Users\\user\\Documents\\MenuOnWeek\\MenuOnWeek\\Frontend\\no-photo--lg.png");
        recipeService = Program.ServiceProvider.GetRequiredService<IRecipeService>();
        ingredientService = Program.ServiceProvider.GetRequiredService<IIngredientService>();
        unitService = Program.ServiceProvider.GetRequiredService<IUnitService>();
        InitializeComponent();

        currentRecipe = recipe;

        RecipeName.Text = recipe.Name;
        Description.Text = recipe.Description;
        if (recipe.Image is not null)
        {
            var image = $"{Directory.GetCurrentDirectory()}\\FileStore\\{recipe.Image.ToString().Required()}";
            Image.Image = System.Drawing.Image.                          // тута ошибка
                FromFile(image);
            currentImage = image;
        }
        Price.Text = recipe.Price.ToString();
        GridRefresh();
    }

    private void GridRefresh()
    {
        usingIngredients =
            currentRecipe.Ingredients.Keys.
            Select(x => ingredientService.
            GetById(x)).ToList();

        IngredientsTable.Rows.Clear();

        IngredientsTable.Rows
            .AddRange(new DataGridViewRow[currentRecipe.Ingredients.Count]
                .Select(x => x = new DataGridViewRow()).ToArray());

        for (int i = 0; i < IngredientsTable.Rows.Count; i++)
        {
            var ingredientComboBoxCell = (IngredientsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();
            ingredientComboBoxCell.DataSource = ingredientService
                .GetAll(0, 100)
                .Select(x => x.Name)
                .ToList();



            if (i + 1 < IngredientsTable.Rows.Count)
            {
                var unitComboBoxCell = (IngredientsTable.Rows[i].Cells[2] as DataGridViewComboBoxCell).Required();
                unitComboBoxCell.DataSource = unitService
                    .GetByIngredient(usingIngredients[i].Id)
                   .Select(x => x.Name)
                   .ToList();

                var currentIngredient = ingredientService.
                    GetById(currentRecipe.Ingredients.Keys.Single(x => x == usingIngredients[i].Id));

                ingredientComboBoxCell.Value = currentIngredient.Name;
                unitComboBoxCell.Value = unitService.GetById(currentRecipe.Ingredients[currentIngredient.Id].UnitId).Name;

                var countCell = (IngredientsTable.Rows[i].Cells[1] as DataGridViewTextBoxCell).Required();
                countCell.Value = currentRecipe.Ingredients[usingIngredients[i].Id].Count;
            }
        }
    }

    private void IngredientsTable_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
        (IngredientsTable.Rows[^1].Cells[0] as DataGridViewComboBoxCell).Required().DataSource =
                ingredientService.GetAll(0, 100).Where(x => usingIngredients.All(y => y.Id != x.Id)).Select(x => x.Name).ToList();
    }

    private void IngredientsTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == 0)
        {
            if (IngredientsTable.Rows[e.RowIndex].Cells[0].Value is not null)
            {
                (IngredientsTable.Rows[e.RowIndex].Cells[2] as DataGridViewComboBoxCell).Required().DataSource =
                   unitService.GetByIngredient(ingredientService.
                      GetByName(IngredientsTable.Rows[e.RowIndex].Cells[0].Value.ToString().Required()).Id).
                      Select(x => x.Name).ToList();
            }
        }
    }

    public RecipeDto GetRecipeDto()
    {
        Dictionary<Guid, QuantityDto> ingredients = new Dictionary<Guid, QuantityDto>();

        for (int i = 0; i < IngredientsTable.Rows.Count - 1; i++)
        {
            var row = IngredientsTable.Rows[i];
            ingredients.Add(ingredientService.GetByName(row.Cells[0].Value.ToString().Required()).Id,
                new QuantityDto(unitService.GetByName(row.Cells[2].Value.ToString().Required()).Required().Id,
                    Double.Parse(row.Cells[1].Value.ToString().Required().Replace('.', ','))));
        }
        return new RecipeDto(RecipeName.Text, Description.Text, currentImage, ingredients, isImageChanged);
    }

    private void Image_MouseClick(object sender, MouseEventArgs e)
    {
        switch (e.Button)
        {
            case MouseButtons.Left:
                {
                    saveFileDialog.ShowDialog();
                    if (!String.IsNullOrEmpty(saveFileDialog.FileName))
                    {
                        Image.Image = System.Drawing.Image.
                            FromFile(saveFileDialog.FileName);
                        currentImage = saveFileDialog.FileName;
                        isImageChanged = true;
                    }
                }
                break;

            case MouseButtons.Right:
                {
                    Image.Image = defaultImage;
                    currentImage = null;
                }
                break;
        }
        isImageChanged = true;
    }
}
