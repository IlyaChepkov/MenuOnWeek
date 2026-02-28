using MenuOnWeek.Data;
using MenuOnWeek.Domain.Menus;
using Microsoft.EntityFrameworkCore;

namespace Data;

internal sealed class MenuRepository : EntityWithIdRepository<Menu>, IMenuRepository
{
    public MenuRepository(DataContext dataContext) : base(dataContext)
    {

    }

    public override async Task<IReadOnlyList<Menu>> Get(int offset, int limit, CancellationToken token)
    {
        return await dataContext.Set<Menu>()
            .AsNoTracking()
            .Include(x => x.MenuRecipes)
            .ThenInclude(x => x.Recipe)
            .ThenInclude(x => x.RecipeIngredients)
            .ThenInclude(x => x.Ingredient)
            .ThenInclude(x => x.IngredientUnits)
            .Skip(offset)
            .Take(limit)
            .ToListAsync(token);
    }

    public Task<Menu?> GetByName(string name, CancellationToken token)
    {
        return dataContext.Set<Menu>()
            .AsNoTracking()
            .Include(x => x.MenuRecipes)
            .ThenInclude(x => x.Recipe)
            .ThenInclude(x => x.RecipeIngredients)
            .ThenInclude(x => x.Ingredient)
            .ThenInclude(x => x.IngredientUnits)
            .SingleOrDefaultAsync(x => x.Name == name, token);
    }

    public async override Task<Menu> GetById(Guid id, CancellationToken token)
    {
        var recipe = await ById(id)
                    .Include(x => x.MenuRecipes)
                    .SingleOrDefaultAsync(token);
        if (recipe is not null)
        {
            return recipe;
        }
        throw new KeyNotFoundException("Рецепта с таким идентификатором не найдено");
    }
}
