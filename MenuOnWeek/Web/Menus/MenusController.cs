using MenuOnWeek.Application.Menus;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Menus;
using MenuOnWeek.Domain;
using MenuOnWeek.Domain.Menus;
using MenuOnWeek.Utils;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace MenuOnWeek.Web.Menus;

/// <summary>
/// Контроллер меню
/// </summary>
[ApiController]
public class MenusController : ControllerBase
{
    private readonly IMenuService menuService;

    /// <summary>
    /// Конструктор MenusController
    /// </summary>
    /// <param name="menuService"></param>
    public MenusController(IMenuService menuService)
    {
        this.menuService = menuService;
    }

    /// <summary>
    /// Добавляет меню
    /// </summary>
    [HttpPost(ApiResource.Menus)]
    public async Task<IActionResult> Add(MenuCreateRequest request, CancellationToken token)
    {
        ValidationException<string?>.ThrowIfNull(request.Name);
        ValidationException<MenuType?>.ThrowIfNull(request.MenuType);
        ValidationException<IReadOnlyList<MenuRecipesCreateOrUpdateRequest>>.ThrowIf(x => x.Any(x => (x.Serve is null || x.Serve < 0) || x.RecipeId is null || x.RecipeId == Guid.Empty),
            request.MenuRecipes,
            "Одно или несколько значений MenuRecipes не заполнены");
        

        var command = new CreateMenuCommand(
            request.Name.Required(),
            request.MenuType.Required(),
            request.MenuRecipes.Select(x => new CreateOrUpdateMenuElementCommand(
                x.RecipeId.GetValueOrDefault(),
                x.Serve.GetValueOrDefault(),
                x.DaysOfWeek,
                x.Meal
            )).ToList()
        );

        await menuService.Add(command, token);
        return Ok();
    }

    /// <summary>
    /// Возвращает все меню, начиная с offset и заканчивая limit
    /// </summary>
    [HttpGet(ApiResource.Menus)]
    public async Task<IReadOnlyList<MenuResponse>> GetAll(int? offset, int? limit, CancellationToken token)
    {
        ValidationException<int?>.ThrowIfNull(offset);
        ValidationException<int?>.ThrowIfNull(limit);

        var menus = await menuService.GetAll(offset.Required(), limit.Required(), token);

        return menus.Select(x => new MenuResponse() { Id = x.Id, MenuType = x.MenuType, Name = x.Name, Price = (int)x.Price, MenuRecipes = x.Recipes.Select(y => new MenuRecipesResponse() { RecipeId = y.RecipeId, DaysOfWeek = y.Date, Meal = y.Meal, Serve = y.ServeCount }).ToList()}).ToList();
    }

    /// <summary>
    /// Обновляет меню
    /// </summary>
    [HttpPut(ApiResource.Menus)]
    public async Task<IActionResult> Update(MenuUpdateRequest request, CancellationToken token)
    {
        ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, request.Id, "Идентификатор не может быть пустым");
        ValidationException<string?>.ThrowIfNull(request.Name);
        ValidationException<MenuType?>.ThrowIfNull(request.MenuType);
        ValidationException<IReadOnlyList<MenuRecipesCreateOrUpdateRequest>>.ThrowIf(x => x.Any(x => (x.Serve is null || x.Serve < 0) || x.RecipeId is null || x.RecipeId == Guid.Empty),
            request.MenuRecipes,
            "Одно или несколько значений MenuRecipes не заполнены");
        

        var command = new MenuUpdateCommand(
            request.Id.GetValueOrDefault(),
            request.Name.Required(),
            request.MenuType.GetValueOrDefault(),
            request.MenuRecipes.Select(x => new CreateOrUpdateMenuElementCommand(
                x.RecipeId.GetValueOrDefault(),
                x.Serve.GetValueOrDefault(),
                x.DaysOfWeek,
                x.Meal
            )).ToList()
        );

        await menuService.Update(command, token);

        return Ok();
    }

    /// <summary>
    /// Удаляет меню
    /// </summary>
    [HttpDelete(ApiResource.MenusById)]
    public async Task<IActionResult> Remove(Guid? id, CancellationToken token)
    {
        ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, id, "Идентификатор не может быть пустым");

        await menuService.Remove(id.GetValueOrDefault(), token);
        return Ok();
    }

    /// <summary>
    /// Возвращает меню по названию
    /// </summary>
    [HttpGet(ApiResource.MenusByName)]
    public async Task<MenuResponse?> GetByName(string? name, CancellationToken token)
    {
        ValidationException<string?>.ThrowIfNull(name);

        var menu = await menuService.GetByName(name.Required(), token);

        if (menu is null)
        {
            return null;
        }
        return new MenuResponse() { Id = menu.Id, Price = (int)menu.Price, Name = menu.Name, MenuType = menu.MenuType, MenuRecipes = menu.Recipes.Select(y => new MenuRecipesResponse() { RecipeId = y.RecipeId, DaysOfWeek = y.Date, Meal = y.Meal, Serve = y.ServeCount }).ToList() };
    }
}
