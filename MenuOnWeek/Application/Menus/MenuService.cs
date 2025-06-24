using Data;
using Domain;

namespace MenuOnWeek.Application.Menus;

internal sealed class MenuService : IMenuService
{
    private readonly IMenuRepository menuRepository;
    private readonly IRecipeRepository recipeRepository;

    public MenuService(IMenuRepository menuRepository, IRecipeRepository recipeRepository)
    {
        this.menuRepository = menuRepository;
        this.recipeRepository = recipeRepository;
    }

    public void Add(CreateMenuModel entity)
    {
        var menu = Menu.Create(
            entity.Name,
            entity.Recipes.Select(
                x => new MenuElement(recipeRepository.GetAll(y => y.Id == x.RecipeId).Single(),
                x.ServeCount,
                x.Date,
                x.Meal))
            .ToList(),
            entity.MenuType);
        menuRepository.Add(menu);
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
                Recipes = x.Recipes.Select(y => new MenuElementViewModel()
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

    public void Update(MenuUpdateModel entity)
    {
        var menu = menuRepository.GetAll(x => x.Id == entity.Id).Single();
        menu.Name = entity.Name;
        menu.Recipes = entity.Recipes.Select(
                x => new MenuElement(recipeRepository.GetAll(y => y.Id == x.RecipeId).Single(),
                x.ServeCount,
                x.Date,
                x.Meal))
            .ToList();
        menu.MenuType = entity.MenuType;
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
            Recipes = menu.Recipes.Select(x => new MenuElementViewModel()
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

