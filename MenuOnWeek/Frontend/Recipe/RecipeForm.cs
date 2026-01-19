using System.Threading.Tasks;
using MenuOnWeek.Clients.Files;
using MenuOnWeek.Clients.Ingredients;
using MenuOnWeek.Clients.Units;
using MenuOnWeek.Contracts.Ingredients;
using MenuOnWeek.Contracts.Recipes;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend.Recipe;

public partial class RecipeForm : UserControl
{
    private IIngredientClient ingredientClient;
    private IUnitClient unitClient;
    public IFileClient fileClient;
    private RecipeResponse currentRecipe;
    private string? currentImage;
    private bool isImageChanged = false;
    private Image defaultImage;

    private List<IngredientResponse> usingIngredients = new List<IngredientResponse>();

    public RecipeForm()
    {
        defaultImage = System.Drawing.Image.
            FromFile($"{Directory.GetCurrentDirectory()}\\no-photo--lg.png");
        ingredientClient = Program.ServiceProvider.GetRequiredService<IIngredientClient>();
        unitClient = Program.ServiceProvider.GetRequiredService<IUnitClient>();
        fileClient = Program.ServiceProvider.GetRequiredService<IFileClient>();
        InitializeComponent();

        currentRecipe = new RecipeResponse()
        {
            Id = Guid.Empty,
            Name = "",
            ImageId = null,
            Price = 0,
            Description = "",
            Ingredients = new List<RecipeIngredientsResponse>()
        };
        GridRefresh();
    }

    public RecipeForm(RecipeResponse recipe)
    {
        defaultImage = Image.FromFile($"{Directory.GetCurrentDirectory()}\\no-photo--lg.png");
        ingredientClient = Program.ServiceProvider.GetRequiredService<IIngredientClient>();
        unitClient = Program.ServiceProvider.GetRequiredService<IUnitClient>();
        fileClient = Program.ServiceProvider.GetRequiredService<IFileClient>();
        InitializeComponent();

        currentRecipe = recipe;

        RecipeName.Text = recipe.Name;
        Description.Text = recipe.Description;
        if (recipe.ImageId is not null)
        {
            var image = fileClient.Get(recipe.ImageId, CancellationToken.None).Result;
            MemoryStream memoryStream = new MemoryStream(image);
            ImageBox.Image = Image.FromStream(memoryStream);                         
            currentImage = recipe.ImageId.ToString();
        }
        Price.Text = recipe.Price.ToString();
        GridRefresh();
    }

    private async void GridRefresh()
    {


        IngredientsTable.Rows.Clear();

        IngredientsTable.Rows
            .AddRange(new DataGridViewRow[currentRecipe.Ingredients.Count]
                .Select(x => x = new DataGridViewRow()).ToArray());

         usingIngredients =
            currentRecipe.Ingredients.
            Select( x => ingredientClient.
                GetById(x.IngredientId, CancellationToken.None).Result.Required()).
            ToList();

        for (int i = 0; i < IngredientsTable.Rows.Count; i++)
        {
            var ingredientComboBoxCell = (IngredientsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();

            var list = ingredientClient.GetAll(0, 100, CancellationToken.None).Result.Required().Where(x => usingIngredients.All(y => y.Id != x.Id)).Select(x => x.Name).ToList();
            if (i + 1 < IngredientsTable.Rows.Count)
            {
                list.Add(usingIngredients[i].Name);
            }
            ingredientComboBoxCell.DataSource = list;

            if (i + 1 < IngredientsTable.Rows.Count)
            {
                var currentIngredient = await ingredientClient.
                    GetById(currentRecipe.Ingredients.Single(x => x.IngredientId == usingIngredients[i].Id).IngredientId, CancellationToken.None);
                ingredientComboBoxCell.Value = currentIngredient.Required().Name;

                var unitComboBoxCell = (IngredientsTable.Rows[i].Cells[2] as DataGridViewComboBoxCell).Required();
                unitComboBoxCell.DataSource = unitClient.GetByIngredient(ingredientClient.GetByName((string)ingredientComboBoxCell.Value.Required(), CancellationToken.None).Result.Required().Id, CancellationToken.None).Result.Required().Select(x => x.Name).ToList();



                var temp = await unitClient.GetById(currentRecipe.Ingredients.Single(x => x.IngredientId == currentIngredient.Id).UnitId, CancellationToken.None).Required();
                unitComboBoxCell.Value = temp.Name;

                var countCell = (IngredientsTable.Rows[i].Cells[1] as DataGridViewTextBoxCell).Required();
                countCell.Value = currentRecipe.Ingredients.Single(x => x.IngredientId == currentIngredient.Id).Count;
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
                usingIngredients.Add(
                    ingredientClient.GetByName(
                        IngredientsTable.Rows[e.RowIndex].Cells[0].Value.ToString().Required(),
                        CancellationToken.None).Result.Required()
                    );

                for (int i = 0; i < IngredientsTable.Rows.Count; i++)
                {
                    var ingredientComboBoxCell = (IngredientsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();

                    var list = ingredientClient.GetAll(0, 100, CancellationToken.None).Result.Required().Where(x => usingIngredients.All(y => y.Id != x.Id)).Select(x => x.Name).ToList();

                    if (i + 1 < IngredientsTable.Rows.Count)
                    {
                        list.Add((string)ingredientComboBoxCell.Value);

                        if (ingredientComboBoxCell.Value is not null)
                        {
                            var unitComboBoxCell = (IngredientsTable.Rows[i].Cells[2] as DataGridViewComboBoxCell).Required();
                            unitComboBoxCell.DataSource = unitClient.GetByIngredient(
                                 ingredientClient.GetByName(
                                    (string)ingredientComboBoxCell.Value,
                                    CancellationToken.None
                                ).Result.Required().Id,
                                CancellationToken.None).Result.Required().Select(x => x.Name).ToList();
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
            if (row.Cells[0].Value is not null && ingredientClient.
                    GetByName(row.Cells[0].Value.
                        ToString().
                        Required(), CancellationToken.None) is not null)
            {
                ingredient = ingredientClient.
                    GetByName(row.Cells[0].Value.
                        ToString().
                        Required(), CancellationToken.None).Result.Required().Id.Required();
            }

            int count = 0;
            if (row.Cells[1].Value is not null && Int32.TryParse(row.Cells[1].Value.ToString().Required(), out count))
            {
                count = Int32.Parse(row.Cells[1].Value.ToString().Required());
            }

            Guid unit = Guid.Empty;
            if (row.Cells[2].Value is not null && unitClient.
                        GetByName(row.Cells[2].Value.ToString().Required(), CancellationToken.None) is not null)
            {
                unit = unitClient.GetByName(row.Cells[2].Value.ToString().Required(), CancellationToken.None).Result.Required().Id.Required();
            }
            ingredients.Add(ingredient,
                    new QuantityDto(
                        unit,
                        count));
        }
        Guid.TryParse((currentImage ?? ".").Split('.').First(), out var result);
        return new RecipeDto(RecipeName.Text, Description.Text, result, ingredients, isImageChanged);
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
                        ImageBox.Image = Image.
                            FromFile(saveFileDialog.FileName);
                        isImageChanged = true;

                        currentImage = fileClient.Add(saveFileDialog.FileName.Split('\\').Last(),
                            File.ReadAllBytes(saveFileDialog.FileName),
                            CancellationToken.None).Result.ToString();

                        
                    }
                }
                break;

            case MouseButtons.Right:
                {
                    ImageBox.Image = defaultImage;
                    currentImage = null;
                    isImageChanged = true;
                }
                break;
        }
        
    }

    private void IngredientsTable_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
    {
        if (usingIngredients.Count > 0)
        {
            usingIngredients.RemoveAt(e.RowIndex);

            for (int i = 0; i < IngredientsTable.Rows.Count; i++)
            {
                var ingredientComboBoxCell = (IngredientsTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();

                var list =  ingredientClient.GetAll(0, 100, CancellationToken.None).Result.Required().Where(x => usingIngredients.All(y => y.Id != x.Id)).Select(x => x.Name).ToList();

                if (i + 1 < IngredientsTable.Rows.Count)
                {
                    list.Add((string)ingredientComboBoxCell.Value);

                    if (ingredientComboBoxCell.Value is not null)
                    {
                        var unitComboBoxCell = (IngredientsTable.Rows[i].Cells[2] as DataGridViewComboBoxCell).Required();
                        unitComboBoxCell.DataSource = unitClient.GetByIngredient(ingredientClient.GetByName((string)ingredientComboBoxCell.Value, CancellationToken.None).Result.Required().Id, CancellationToken.None).Result.Required().Select(x => x.Name).ToList();
                    }
                }

                ingredientComboBoxCell.DataSource = list;
            }
        }
    }
}
