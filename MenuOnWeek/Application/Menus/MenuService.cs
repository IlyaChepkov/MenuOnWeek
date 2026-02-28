using System.Linq;
using Data;
using MenuOnWeek.Application.Recipes;
using MenuOnWeek.Domain.Menus;
using MenuOnWeek.Utils;
using Microsoft.Extensions.Logging;
using Utils;

namespace MenuOnWeek.Application.Menus;

internal sealed class MenuService : IMenuService
{
    private readonly IMenuRepository menuRepository;
    private readonly IRecipeRepository recipeRepository;
    private readonly IMenuRecipesRepository menuRecipesRepository;
    private readonly ILogger logger;

    public MenuService(
        IMenuRepository menuRepository,
        IRecipeRepository recipeRepository,
        IMenuRecipesRepository menuRecipesRepository,
        ILogger<MenuService> logger)
    {
        this.menuRepository = menuRepository;
        this.recipeRepository = recipeRepository;
        this.menuRecipesRepository = menuRecipesRepository;
        this.logger = logger;
    }

    public async Task Add(CreateMenuCommand createRequest, CancellationToken token)
    {
        var name = await GetByName(createRequest.Name, token);
        ValidationException<MenuView?>.ThrowIf(x => x is not null, name, "Меню с таким именем уже существует");

        var menu = Menu.Create(
            createRequest.Name,
            createRequest.MenuType);
        await menuRepository.Add(menu, CancellationToken.None);

        await menuRecipesRepository.
            AddRange(createRequest.MenuRecipes.Select
                (x => MenuRecipes.Create
                    (menu.Id,
                    recipeRepository.GetById(x.RecipeId, token).Result,
                    x.ServeCount,
                    x.Date,
                    x.Meal)).ToList(),
                token);
        logger.LogInformation("Добавленно меню с id {id} и названием {Name}",menu.Id ,menu.Name);
    }

    public async Task<IReadOnlyList<MenuView>> GetAll(int offset, int limit, CancellationToken token)
    {
        ValidationException<int>.ThrowIf(x => x < 0, offset, "Начальное значение не может быть меньше 0");
        ValidationException<int>.ThrowIf(x => x < 1, limit, "Размер выборки не может быть меньше 1");

        var menus = await menuRepository.Get(offset, limit, token);
        return menus.
            Select(x => new MenuView(
            
                x.Id,
                x.Name,
                x.MenuType,
                x.Price,
                x.MenuRecipes.Select(y => new MenuElementView(
                
                    y.RecipeId,
                    y.Serve,
                    y.Date,
                    y.Meal
                )
                ).ToList()
                
            ))
            .ToList();
    }

    public async Task Remove(Guid entity, CancellationToken token)
    {
        var menu = await menuRepository.GetById(entity, token);
        await menuRepository.Remove(menu, token);
        logger.LogInformation("Удалено меню с id {Id}", menu.Id);
    }

    public async Task Update(MenuUpdateCommand updateRequest, CancellationToken token)
    {
        var name = await GetByName(updateRequest.Name, token);
        ValidationException<MenuView?>.ThrowIf(x => x is not null && x.Id != updateRequest.Id, name, "Меню с таким именем уже существует");

        var menu = await menuRepository.GetById(updateRequest.Id, token);
        menu.Name = updateRequest.Name;

        menu.MenuType = updateRequest.MenuType;
        await menuRecipesRepository.RemoveRange(await menuRecipesRepository.Get(0, Int32.MaxValue, CancellationToken.None), CancellationToken.None);
        var menuRecipes = updateRequest.MenuRecipes.Select(async x => MenuRecipes.Create(
            menu.Id,
            await recipeRepository.GetById(x.RecipeId, CancellationToken.None),
            x.ServeCount,
            x.Date,
            x.Meal
            )).Select(x => x.Result).ToList();
        await menuRecipesRepository.AddRange(menuRecipes,
            CancellationToken.None);

        await menuRepository.Update(menu, token);
        logger.LogInformation("Обновлено меню с id {Id}", menu.Id);
    }

    public async Task<MenuView?> GetByName(string name, CancellationToken token)
    {
        var menu = await menuRepository.GetByName(name, token);

        if (menu is null)
        {
            return null;
        }
        return new MenuView(
        
            menu.Id,
            menu.Name,
            menu.MenuType,
            menu.Price,
            menu.MenuRecipes.Select(x => new MenuElementView(
            
                x.RecipeId,
                x.Serve,
                x.Date,
                x.Meal
                
            )).ToList()
            
        );
    }
}

