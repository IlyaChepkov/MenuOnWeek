using Data;
using MenuOnWeek.Domain.Menus;

namespace MenuOnWeek.Application.Menus;

/// <summary>
/// Репозиторий для работы с блюдами в меню
/// </summary>
public interface IMenuRecipesRepository : IBaseRepository<MenuRecipes>
{
}
