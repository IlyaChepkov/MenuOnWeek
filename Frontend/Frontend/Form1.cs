using Application;
using Microsoft.Extensions.DependencyInjection;

namespace Frontend
{
    public partial class Form1 : Form
    {
        private readonly IRecipeService recipeService;

        public Form1()
        {
            recipeService = Program.serviceProvider.GetRequiredService<IRecipeService>();
            InitializeComponent();
            RecipesLict.Items.AddRange(recipeService.GetAll(x => true, 0, 10).Select(x => x.Name).ToArray());
        }
    }
}
