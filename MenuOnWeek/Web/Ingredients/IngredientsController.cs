using Application.Ingredients;
using Application.Units;
using Domain;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Ingredients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace MenuOnWeek.Web.Ingredients;

[ApiController]
public  class IngredientsController : ControllerBase
{
    private readonly IIngredientService ingredientService;
    private readonly IUnitService unitService;
    
    public IngredientsController(IIngredientService ingredientService, IUnitService unitService)
    {
        this.ingredientService = ingredientService;
        this.unitService = unitService;
    }

    [HttpPost(ApiResource.Ingredients)]
    public async Task<IActionResult> Add(IngredientCreateRequest request, CancellationToken token)
    {
        if (request.UnitId is null || request.UnitId == Guid.Empty)
        {
            throw new ArgumentNullException();
        }
        if (String.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentNullException();
        }
        if (request.Price is null || request.Price < 1)
        {
            throw new ArgumentNullException();
        }
        if (request.Units.Any(x => x.UnitId is null || x.UnitId == Guid.Empty || x.Coeficient is null || x.Coeficient < 0)  )
        {
            throw new ArgumentNullException();
        }
        var table =  request.Units.Required().Select(x => (unitService.GetById(x.UnitId.GetValueOrDefault(), token).Result, x.Coeficient.GetValueOrDefault())).ToDictionary();

        var command = new CreateIngredientCommand(request.Name, request.Price.Value, request.UnitId.Value, table);

        await ingredientService.Add(command, token);
        return Ok();
    }
    [HttpGet(ApiResource.Ingredients)]
    public async Task<IReadOnlyList<IngredientResponse>> GetAll(int? offset, int? limit, CancellationToken token)
    {
        if (!limit.HasValue || !offset.HasValue)
        {
            throw new ArgumentNullException();
        }
        if (limit.Value < 1 || offset.Value < 0)
        {
            throw new ArgumentException();
        }

        var ingredients = await ingredientService.GetAll(offset.Value, limit.Value, token);

        return ingredients.Select(x => new IngredientResponse() { Id = x.Id, Name = x.Name, UnitId = x.UnitId, Units = x.Table.Select(y => new IngredientUnitsResponse() {UnitId = y.Key.Id, Coeficient = y.Value }).ToList() }).ToList();
    }
    [HttpPut(ApiResource.Ingredients)]
    public async Task<IActionResult> Update(IngredientUpdateRequest request, CancellationToken token)
    {
        if (request.Id is null || request.Id == Guid.Empty)
        {
            throw new ArgumentNullException();
        }
        if (request.UnitId is null || request.UnitId == Guid.Empty)
        {
            throw new ArgumentNullException();
        }
        if (String.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentNullException();
        }
        if (request.Price is null || request.Price < 1)
        {
            throw new ArgumentNullException();
        }
        if (request.Units.Any(x => x.UnitId is null || x.UnitId == Guid.Empty || x.Coeficient is null || x.Coeficient < 0))
        {
            throw new ArgumentNullException();
        }

        var command = new UpdateIngredientCommand() { Id = request.Id.GetValueOrDefault(), Name = request.Name, Price = request.Price.GetValueOrDefault(), UnitId = request.UnitId.GetValueOrDefault(), Table = request.Units.Select(x => (unitService.GetById(x.UnitId.GetValueOrDefault(), token).Result, x.Coeficient.GetValueOrDefault())).ToDictionary()};

        await ingredientService.Update(command, token);

        return Ok();
    }
    [HttpDelete(ApiResource.Ingredients)]
    public async Task<IActionResult> Remove(Guid? id, CancellationToken token)
    {
        if (id is null || id == Guid.Empty)
        {
            throw new ArgumentNullException();
        }

        await ingredientService.Remove(id.GetValueOrDefault(), token);
        return Ok();
    }
    [HttpGet(ApiResource.IngredientsById)]
    public async Task<IngredientResponse> GetById(Guid? id, CancellationToken token)
    {
        if (id is null || id == Guid.Empty)
        {
            throw new ArgumentNullException();
        }

        var ingredient = await ingredientService.GetById(id.GetValueOrDefault(), token);

        return new IngredientResponse() {Id = ingredient.Id, Name = ingredient.Name, UnitId = ingredient.UnitId, Units =  ingredient.Table.Select(y => new IngredientUnitsResponse() { UnitId = y.Key.Id, Coeficient = y.Value }).ToList()};
    }

    [HttpGet(ApiResource.IngredientsByName)]
    public async Task<IngredientResponse?> GetByName(string name, CancellationToken token)
    {
        if (String.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException();
        }

        var ingredient = await ingredientService.GetByName(name, token);

        if (ingredient is null)
        {
            HttpContext.Response.StatusCode = 404;
            return null;
        }
        return new IngredientResponse() { Id = ingredient.Id, Name = ingredient.Name, UnitId = ingredient.UnitId, Units = ingredient.Table.Select(y => new IngredientUnitsResponse() { UnitId = y.Key.Id, Coeficient = y.Value }).ToList() };
    }

    [HttpGet(ApiResource.IngredientsByNamePart)]
    public async Task<IReadOnlyList<IngredientResponse>> GetByPartName(string? namePart, int? offset, int? limit, CancellationToken token)
    {
        if (!limit.HasValue || !offset.HasValue)
        {
            throw new ArgumentNullException();
        }
        if (limit.Value < 1 || offset.Value < 0)
        {
            throw new ArgumentException();
        }
        if (String.IsNullOrWhiteSpace(namePart))
        {
            throw new ArgumentNullException();
        }

        var ingredients = await ingredientService.GetByPartName(namePart, offset.GetValueOrDefault(), limit.GetValueOrDefault(), token);

        return ingredients.Select(x => new IngredientResponse() { Id = x.Id, Name = x.Name, UnitId = x.UnitId, Units = x.Table.Select(y => new IngredientUnitsResponse() { UnitId = y.Key.Id, Coeficient = y.Value }).ToList() }).ToList();
    }
}
