using Data;
using Domain;

namespace MenuOnWeek.Application.Menus;

internal sealed class MenuService : IMenuService
{
    private readonly IMenuRepository menuRepository;
    private readonly IRecipeRepository recipeRepository;
    private readonly IMenuRecipesRepository menuRecipesRepository;

    public MenuService(IMenuRepository menuRepository, IRecipeRepository recipeRepository, IMenuRecipesRepository menuRecipesRepository)
    {
        this.menuRepository = menuRepository;
        this.recipeRepository = recipeRepository;
        this.menuRecipesRepository = menuRecipesRepository;
    }

    public async Task Add(CreateMenuCommand entity, CancellationToken token)
    {
        var menu = Menu.Create(
            entity.Name,
            entity.MenuType);
        await menuRepository.Add(menu, CancellationToken.None);

        await menuRecipesRepository.AddRange(entity.MenuRecipes.Select(x => MenuRecipes.Create(menu.Id, recipeRepository.GetById(x.RecipeId, token).Result, x.ServeCount, x.Date, x.Meal)).ToList(), token);
    }

    public async Task<IReadOnlyList<MenuViewModel>> GetAll(int offset, int limit, CancellationToken token)
    {
        var menus = await menuRepository.GetAll(token);
        return menus.
            Skip(offset).
            Take(limit).
            Select(x => new MenuViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Recipes = x.MenuRecipes.Select(y => new MenuElementViewModel()
                {
                    RecipeId = y.RecipeId,
                    Meal = y.Meal,
                    ServeCount = y.Serve,
                    Date = y.Date
                }
                ).ToList(),
                MenuType = x.MenuType
            })
            .ToList();
    }

    public async Task Remove(Guid entity, CancellationToken token)
    {
        var menu = await menuRepository.GetById(entity, token);
        await menuRepository.Remove(menu, token);
    }

    public async Task Update(MenuUpdateModel updateRequest, CancellationToken token)
    {
        var menu = await menuRepository.GetById(updateRequest.Id, token);
        menu.Name = updateRequest.Name;
        //menu.MenuRecipes = entity.MenuRecipes.Select(x => MenuRecipes.Create(
        //    menu.Id,
        //    recipeRepository.GetAll(y => y.Id == x.RecipeId).Single(),
        //    x.ServeCount,
        //    x.Date,
        //    x.Meal))
        //    .ToList();
        menu.MenuType = updateRequest.MenuType;

        List<MenuRecipes> deleteList = menu.MenuRecipes.Where(x => !updateRequest.MenuRecipes.Any(y => y.RecipeId == x.RecipeId)).ToList();

        await menuRecipesRepository.RemoveRange(deleteList, token);

        List<MenuRecipes> updateList = menu.MenuRecipes.Where(x => updateRequest.MenuRecipes.Any(y => y.RecipeId == x.RecipeId)).ToList();
        updateList.ForEach(x =>
        {
            x.Date = updateRequest.MenuRecipes.Single(y => y.RecipeId == x.RecipeId).Date;
            x.Meal = updateRequest.MenuRecipes.Single(y => y.RecipeId == x.RecipeId).Meal;
            x.Serve = updateRequest.MenuRecipes.Single(y => y.RecipeId == x.RecipeId).ServeCount;
        });

        await menuRecipesRepository.UpdateRange(updateList, token);

        List<MenuRecipes> addList = updateRequest.MenuRecipes
            .Where(x => !menu.MenuRecipes.Any(y => y.RecipeId == x.RecipeId))
            .Select(x => MenuRecipes.Create(menu.Id, recipeRepository.GetById(x.RecipeId, token).Result, x.ServeCount, x.Date, x.Meal))
            .ToList();

        await menuRecipesRepository.AddRange(addList, token);

        await menuRepository.Update(menu, token);
    }

    public async Task<MenuViewModel?> GetByName(string name, CancellationToken token)
    {
        var menu = await menuRepository.GetByName(name, token);

        if (menu is null)
        {
            return null;
        }
        return new MenuViewModel()
        {
            Id = menu.Id,
            Name = menu.Name,
            Price = menu.Price,
            Recipes = menu.MenuRecipes.Select(x => new MenuElementViewModel()
            {
                RecipeId = x.RecipeId,
                Meal = x.Meal,
                ServeCount = x.Serve,
                Date = x.Date
            }).ToList(),
            MenuType = menu.MenuType
        };
    }
}

