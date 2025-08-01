using Application.Ingredients;
using Application.Units;
using MenuOnWeek.Application.Recipes;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend.Recipe;

public partial class RecipeForm : UserControl
{
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
            FromFile($"{Directory.GetCurrentDirectory()}\\no-photo--lg.png");
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
                FromFile($"{Directory.GetCurrentDirectory()}\\no-photo--lg.png");
        ingredientService = Program.ServiceProvider.GetRequiredService<IIngredientService>();
        unitService = Program.ServiceProvider.GetRequiredService<IUnitService>();
        InitializeComponent();

        currentRecipe = recipe;

        RecipeName.Text = recipe.Name;
        Description.Text = recipe.Description;
        if (recipe.Image is not null)
        {
            var image = $"{Directory.GetCurrentDirectory()}\\FileStore\\{recipe.Image.ToString().Required()}";
            Image.Image = System.Drawing.Image.                          // тут ошибка
                FromFile(image);
            currentImage = image;
        }
        Price.Text = recipe.Price.ToString();
        GridRefresh();
    }

    private void GridRefresh()
    {


        IngredientsTable.Rows.Clear();

        IngredientsTable.Rows
            .AddRange(new DataGridViewRow[currentRecipe.Ingredients.Count]
                .Select(x => x = new DataGridViewRow()).ToArray());

        usingIngredients =
            currentRecipe.Ingredients.Keys.
            Select(x => ingredientService.
                GetById(x)).
            ToList();

        for (int i = 0; i < IngredientsTable.Rows.Count; i++)
        {
            var ingredientComboBoxCell = (IngredientsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();

            var list = ingredientService.GetAll(0, 100).Where(x => usingIngredients.All(y => y.Id != x.Id)).Select(x => x.Name).ToList();
            if (i + 1 < IngredientsTable.Rows.Count)
            {
                list.Add(usingIngredients[i].Name);
            }
            ingredientComboBoxCell.DataSource = list;

            if (i + 1 < IngredientsTable.Rows.Count)
            {
                var currentIngredient = ingredientService.
                    GetById(currentRecipe.Ingredients.Keys.Single(x => x == usingIngredients[i].Id));
                ingredientComboBoxCell.Value = currentIngredient.Name;

                var unitComboBoxCell = (IngredientsTable.Rows[i].Cells[2] as DataGridViewComboBoxCell).Required();
                unitComboBoxCell.DataSource = unitService.GetByIngredient(ingredientService.GetByName((string)ingredientComboBoxCell.Value).Required().Id).Select(x => x.Name).ToList();




                unitComboBoxCell.Value = unitService.GetById(currentRecipe.Ingredients[currentIngredient.Id].UnitId).Name;

                var countCell = (IngredientsTable.Rows[i].Cells[1] as DataGridViewTextBoxCell).Required();
                countCell.Value = currentRecipe.Ingredients[usingIngredients[i].Id].Count;
            }
        }
    }

    private void IngredientsTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == 0)
        {
            if (IngredientsTable.Rows[e.RowIndex].Cells[0].Value is not null)
            {

                if (e.RowIndex + 2 < IngredientsTable.Rows.Count)
                {
                    usingIngredients.RemoveAt(e.RowIndex);
                }
                usingIngredients.Add(ingredientService.GetByName(IngredientsTable.Rows[e.RowIndex].Cells[0].Value.ToString().Required()).Required());

                for (int i = 0; i < IngredientsTable.Rows.Count; i++)
                {
                    var ingredientComboBoxCell = (IngredientsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();

                    var list = ingredientService.GetAll(0, 100).Where(x => usingIngredients.All(y => y.Id != x.Id)).Select(x => x.Name).ToList();

                    if (i + 1 < IngredientsTable.Rows.Count)
                    {
                        list.Add((string)ingredientComboBoxCell.Value);

                        if (ingredientComboBoxCell.Value is not null)
                        {
                            var unitComboBoxCell = (IngredientsTable.Rows[i].Cells[2] as DataGridViewComboBoxCell).Required();
                            unitComboBoxCell.DataSource = unitService.GetByIngredient(ingredientService.GetByName((string)ingredientComboBoxCell.Value).Required().Id).Select(x => x.Name).ToList();
                        }
                    }

                    ingredientComboBoxCell.DataSource = list;
                }
            }
        }
    }

    public RecipeDto GetRecipeDto()
    {
        Dictionary<Guid, QuantityDto> ingredients = new Dictionary<Guid, QuantityDto>();

        for (int i = 0; i < IngredientsTable.Rows.Count - 1; i++)
        {
            var row = IngredientsTable.Rows[i];

            Guid ingredient = Guid.Empty;
            if (row.Cells[0].Value is not null && ingredientService.
                    GetByName(row.Cells[0].Value.
                        ToString().
                        Required()) is not null)
            {
                ingredient = ingredientService.
                    GetByName(row.Cells[0].Value.
                        ToString().
                        Required()).Required().Id;
            }

            int count = 0;
            if (row.Cells[1].Value is not null && Int32.TryParse(row.Cells[1].Value.ToString().Required(), out count))
            {
                count = Int32.Parse(row.Cells[1].Value.ToString().Required());
            }

            Guid unit = Guid.Empty;
            if (row.Cells[2].Value is not null && unitService.
                        GetByName(row.Cells[2].Value.ToString().Required()) is not null)
            {
                unit = unitService.GetByName(row.Cells[2].Value.ToString().Required()).Required().Id;
            }
            ingredients.Add(ingredient ,
                    new QuantityDto(
                        unit,
                        count));
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

    private void IngredientsTable_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
    {
        if (usingIngredients.Count > 0)
        {
            usingIngredients.RemoveAt(e.RowIndex);

            for (int i = 0; i < IngredientsTable.Rows.Count; i++)
            {
                var ingredientComboBoxCell = (IngredientsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();

                var list = ingredientService.GetAll(0, 100).Where(x => usingIngredients.All(y => y.Id != x.Id)).Select(x => x.Name).ToList();

                if (i + 1 < IngredientsTable.Rows.Count)
                {
                    list.Add((string)ingredientComboBoxCell.Value);

                    if (ingredientComboBoxCell.Value is not null)
                    {
                        var unitComboBoxCell = (IngredientsTable.Rows[i].Cells[2] as DataGridViewComboBoxCell).Required();
                        unitComboBoxCell.DataSource = unitService.GetByIngredient(ingredientService.GetByName((string)ingredientComboBoxCell.Value).Required().Id).Select(x => x.Name).ToList();
                    }
                }

                ingredientComboBoxCell.DataSource = list;
            }
        }
    }
}
