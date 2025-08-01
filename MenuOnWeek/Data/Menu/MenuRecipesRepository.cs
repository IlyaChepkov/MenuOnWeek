using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using MenuOnWeek.Application.Menus;

namespace MenuOnWeek.Data.Menu
{
    internal sealed class MenuRecipesRepository : BaseRepository<MenuRecipes>, IMenuRecipesRepository
    {
        public MenuRecipesRepository(DataContext dataContext) : base(dataContext)
        {
            
        }
    }
}
