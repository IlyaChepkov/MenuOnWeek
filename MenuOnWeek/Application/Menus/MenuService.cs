using Data;
using Domain;
using MenuOnWeek.Application.Recipes;
using MenuOnWeek.Domain;

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

    public void Add(CreateMenuModel entity)
    {
        var menu = Menu.Create(
            entity.Name,
            entity.MenuType);
        menuRepository.Add(menu);

        menuRecipesRepository.AddRange(entity.MenuRecipes.Select(x => MenuRecipes.Create(menu.Id, recipeRepository.GetById(x.RecipeId), x.ServeCount, x.Date, x.Meal)).ToList());
    }

    public IReadOnlyList<MenuViewModel> GetAll(int offset, int limit)
    {
        return menuRepository.
            GetAll(x => true).
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

    public void Remove(Guid entity)
    {
        var menu = menuRepository.GetAll(x => x.Id == entity).Single();
        menuRepository.Remove(menu);
    }

    public void Update(MenuUpdateModel updateRequest)
    {
        var menu = menuRepository.GetById(updateRequest.Id);
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

        menuRecipesRepository.RemoveRange(deleteList);

        List<MenuRecipes> updateList = menu.MenuRecipes.Where(x => updateRequest.MenuRecipes.Any(y => y.RecipeId == x.RecipeId)).ToList();
        updateList.ForEach(x =>
        {
            x.Date = updateRequest.MenuRecipes.Single(y => y.RecipeId == x.RecipeId).Date;
            x.Meal = updateRequest.MenuRecipes.Single(y => y.RecipeId == x.RecipeId).Meal;
            x.Serve = updateRequest.MenuRecipes.Single(y => y.RecipeId == x.RecipeId).ServeCount;
        });

        menuRecipesRepository.UpdateRange(updateList);

        List<MenuRecipes> addList = updateRequest.MenuRecipes
            .Where(x => !menu.MenuRecipes.Any(y => y.RecipeId == x.RecipeId))
            .Select(x => MenuRecipes.Create(menu.Id, recipeRepository.GetById(x.RecipeId), x.ServeCount, x.Date, x.Meal))
            .ToList();

        menuRecipesRepository.AddRange(addList);

        menuRepository.Update(menu);
    }

    public MenuViewModel GetByName(string name)
    {
        var menu = menuRepository.GetAll(x => x.Name == name).Single();
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

