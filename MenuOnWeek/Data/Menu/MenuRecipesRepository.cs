using Data;
using MenuOnWeek.Application.Menus;
using MenuOnWeek.Domain.Menus;

namespace MenuOnWeek.Data.Menu
{
    internal sealed class MenuRecipesRepository : BaseRepository<MenuRecipes>, IMenuRecipesRepository
    {
        public MenuRecipesRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
