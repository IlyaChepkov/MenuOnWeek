using System.Xml.Linq;
using Application.Ingredients;
using Application.Units;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Ingredients;
using MenuOnWeek.Domain;
using MenuOnWeek.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace MenuOnWeek.Web.Ingredients;

/// <summary>
/// Контроллер ингредиентов
/// </summary>
[ApiController]
public  class IngredientsController : ControllerBase
{
    private readonly IIngredientService ingredientService;
    private readonly IUnitService unitService;

    /// <summary>
    /// Конструктор IngredientsController
    /// </summary>
    public IngredientsController(IIngredientService ingredientService, IUnitService unitService)
    {
        this.ingredientService = ingredientService;
        this.unitService = unitService;
    }


    /// <summary>
    /// Добавляет ингредиент
    /// </summary>
    [HttpPost(ApiResource.Ingredients)]
    public async Task<IActionResult> Add(IngredientCreateRequest request, CancellationToken token)
    {
        ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, request.UnitId, "Идентификатор базовой единицы измерения не может быть пустым");
        ValidationException<string?>.ThrowIfNull(request.Name);
        ValidationException<int?>.ThrowIfNull(request.Price);
        ValidationException<IReadOnlyList<IngredientUnitsCreateOrUpdateRequest>>.ThrowIf(x => x.Any(x => x.UnitId is null || x.UnitId == Guid.Empty || x.Coeficient is null),
            request.Units,
            "Одно или несколько значений IngredientUnits не заполнены");

        var table =  request.Units.Required().Select(x => (unitService.GetById(x.UnitId.GetValueOrDefault(), token).Result, x.Coeficient.GetValueOrDefault())).ToDictionary();

        var command = new CreateIngredientCommand(request.Name.Required(), request.Price.Required(), request.UnitId.Required(), table);

        await ingredientService.Add(command, token);
        return Ok();
    }

    /// <summary>
    /// Возвращает все ингредиенты, начиная с offset и заканчиная limit
    /// </summary>
    [HttpGet(ApiResource.Ingredients)]
    public async Task<IReadOnlyList<IngredientResponse>> GetAll(int? offset, int? limit, CancellationToken token)
    {
        ValidationException<int?>.ThrowIfNull(offset);
        ValidationException<int?>.ThrowIfNull(limit);

        var ingredients = await ingredientService.GetAll(offset.Required(), limit.Required(), token);

        return ingredients.Select(x => new IngredientResponse()
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            UnitId = x.UnitId,
            Units = x.IngredientUnits.Select(y => new IngredientUnitsResponse()
                {
                    UnitId = y.Key.Id,
                    Coeficient = y.Value
                }).ToList()
        }).ToList();
    }

    /// <summary>
    /// Обновляет ингредиент
    /// </summary>
    [HttpPut(ApiResource.Ingredients)]
    public async Task<IActionResult> Update(IngredientUpdateRequest request, CancellationToken token)
    {
        ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, request.Id, "Идентификатор не может быть пустым");
        ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, request.UnitId, "Идентификатор базовой единицы измерения не может быть пустым");
        ValidationException<string?>.ThrowIfNull(request.Name);
        ValidationException<int?>.ThrowIfNull(request.Price);
        ValidationException<IReadOnlyList<IngredientUnitsCreateOrUpdateRequest>>.ThrowIf(
            x => x.Any(x => x.UnitId is null || x.UnitId == Guid.Empty || x.Coeficient is null || x.Coeficient < 0),
            request.Units,
            "Одно или несколько значений IngredientUnits не заполнены");

        var command = new UpdateIngredientCommand(
        
            request.Id.GetValueOrDefault(),
            request.Name.Required(),
            request.Price.GetValueOrDefault(),
            request.UnitId.GetValueOrDefault(),
            request.Units.Select(x => (unitService.GetById(x.UnitId.GetValueOrDefault(), token).Result,
            x.Coeficient.GetValueOrDefault())).ToDictionary()
        );

        await ingredientService.Update(command, token);

        return Ok();
    }

    /// <summary>
    /// Удаляет ингредиент
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpDelete(ApiResource.IngredientsById)]
    public async Task<IActionResult> Remove(Guid? id, CancellationToken token)
    {
        ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, id, "Идентификатор не может быть пустым");

        await ingredientService.Remove(id.GetValueOrDefault(), token);
        return Ok();
    }

    /// <summary>
    /// Возвращает ингредиент по id
    /// </summary>
    [HttpGet(ApiResource.IngredientsById)]
    public async Task<IngredientResponse> GetById(Guid? id, CancellationToken token)
    {
        ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, id, "Идентификатор не может быть пустым");

        var ingredient = await ingredientService.GetById(id.GetValueOrDefault(), token);

        return new IngredientResponse() {Id = ingredient.Id, Name = ingredient.Name, Price = ingredient.Price, UnitId = ingredient.UnitId, Units =  ingredient.IngredientUnits.Select(y => new IngredientUnitsResponse() { UnitId = y.Key.Id, Coeficient = y.Value }).ToList()};
    }

    /// <summary>
    /// Возвращает ингредиент по названию
    /// </summary>
    [HttpGet(ApiResource.IngredientsByName)]
    public async Task<IngredientResponse?> GetByName(string name, CancellationToken token)
    {
        ValidationException<string?>.ThrowIfNull(name);

        var ingredient = await ingredientService.GetByName(name, token);

        if (ingredient is null)
        {
            HttpContext.Response.StatusCode = 404;
            return null;
        }
        return new IngredientResponse() { Id = ingredient.Id, Name = ingredient.Name, Price = ingredient.Price, UnitId = ingredient.UnitId, Units = ingredient.IngredientUnits.Select(y => new IngredientUnitsResponse() { UnitId = y.Key.Id, Coeficient = y.Value }).ToList() };
    }

    /// <summary>
    /// Вовращает все ингредиенты содержащие подстроку namePart в названии
    /// </summary>
    [HttpGet(ApiResource.IngredientsByNamePart)]
    public async Task<IReadOnlyList<IngredientResponse>> GetByPartName(string? namePart, int? offset, int? limit, CancellationToken token)
    {
        ValidationException<int?>.ThrowIfNull(offset);
        ValidationException<int?>.ThrowIfNull(limit);

        ValidationException<string?>.ThrowIfNull(namePart);

        var ingredients = await ingredientService.GetByPartName(namePart.Required(), offset.GetValueOrDefault(), limit.GetValueOrDefault(), token);

        return ingredients.Select(x => new IngredientResponse() { Id = x.Id, Name = x.Name, Price = x.Price, UnitId = x.UnitId, Units = x.IngredientUnits.Select(y => new IngredientUnitsResponse() { UnitId = y.Key.Id, Coeficient = y.Value }).ToList() }).ToList();
    }
}
