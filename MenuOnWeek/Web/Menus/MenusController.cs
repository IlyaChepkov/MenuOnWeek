using Domain;
using MenuOnWeek.Application.Menus;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Menus;
using Microsoft.AspNetCore.Mvc;

namespace MenuOnWeek.Web.Menus;

[ApiController]
public class MenusController : ControllerBase
{
    private readonly IMenuService menuService;

    public MenusController(IMenuService menuService)
    {
        this.menuService = menuService;
    }

    [HttpPost(ApiResource.Menus)]
    public async Task<IActionResult> Add(MenuCreateRequest request, CancellationToken token)
    {
        if (String.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentNullException(nameof(request.Name));
        }
        if (request.MenuType == null)
        {
            throw new ArgumentNullException();
        }
        if(request.MenuRecipes.Any(x => (x.Serve is null || x.Serve < 0) || x.RecipeId is null || x.RecipeId == Guid.Empty))
        {
            throw new ArgumentNullException();
        }

        var command = new CreateMenuCommand() { Name = request.Name, MenuType = request.MenuType.Value, MenuRecipes = request.MenuRecipes.Select(x => new MenuElementModel() { RecipeId = x.RecipeId.GetValueOrDefault(), Date = x.DaysOfWeek, Meal = x.Meal, ServeCount = x.Serve.GetValueOrDefault() }).ToList() };

        await menuService.Add(command, token);
        return Ok();
    }

    [HttpGet(ApiResource.Menus)]
    public async Task<IReadOnlyList<MenuResponse>> GetAll(int? offset, int? limit, CancellationToken token)
    {
        if (!limit.HasValue || !offset.HasValue)
        {
            throw new ArgumentNullException();
        }
        if (limit.Value < 1 || offset.Value < 0)
        {
            throw new ArgumentException();
        }

        var menus = await menuService.GetAll(offset.Value, limit.Value, token);

        return menus.Select(x => new MenuResponse() { Id = x.Id, MenuType = x.MenuType, Name = x.Name, Price = (int)x.Price, MenuRecipes = x.Recipes.Select(y => new MenuRecipesResponse() { RecipeId = y.RecipeId, DaysOfWeek = y.Date, Meal = y.Meal, Serve = y.ServeCount }).ToList()}).ToList();
    }

    [HttpPut(ApiResource.Menus)]
    public async Task<IActionResult> Update(MenuUpdateRequest request, CancellationToken token)
    {
        if (request.Id is null || request.Id == Guid.Empty)
        {
            throw new ArgumentNullException();
        }
        if (String.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentNullException(nameof(request.Name));
        }
        if (request.MenuType == null)
        {
            throw new ArgumentNullException();
        }
        if (request.MenuRecipes.Any(x => (x.Serve is null || x.Serve < 0) || x.RecipeId is null || x.RecipeId == Guid.Empty))
        {
            throw new ArgumentNullException();
        }

        var command = new MenuUpdateModel() { Id = request.Id.GetValueOrDefault(), Name = request.Name, MenuType = request.MenuType.GetValueOrDefault(), MenuRecipes = request.MenuRecipes.Select(x => new MenuElementModel() { RecipeId = x.RecipeId.GetValueOrDefault(), Date = x.DaysOfWeek, Meal = x.Meal, ServeCount = x.Serve.GetValueOrDefault() }).ToList() };

        await menuService.Update(command, token);

        return Ok();
    }

    [HttpDelete(ApiResource.Menus)]
    public async Task<IActionResult> Remove(Guid? id, CancellationToken token)
    {
        if (id is null || id == Guid.Empty)
        {
            throw new ArgumentNullException();
        }

         await menuService.Remove(id.GetValueOrDefault(), token);
        return Ok();
    }

    [HttpGet(ApiResource.MenusByName)]
    public async Task<MenuResponse?> GetByName(string? name, CancellationToken token)
    {
        if (String.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException();
        }

        var menu = await menuService.GetByName(name, token);

        if (menu is null)
        {
            return null;
        }
        return new MenuResponse() { Id = menu.Id, Price = (int)menu.Price, Name = menu.Name, MenuType = menu.MenuType, MenuRecipes = menu.Recipes.Select(y => new MenuRecipesResponse() { RecipeId = y.RecipeId, DaysOfWeek = y.Date, Meal = y.Meal, Serve = y.ServeCount }).ToList() };
    }
}
