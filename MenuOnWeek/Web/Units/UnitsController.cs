using System.Net;
using System.Xml.Linq;
using Application.Units;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Units;
using MenuOnWeek.Utils;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace MenuOnWeek.Web.Units;

/// <summary>
/// Контроллер единиц измерения
/// </summary>
[ApiController]
public sealed class UnitsController : ControllerBase
{
    private readonly IUnitService unitService;

    /// <summary>
    /// Конструктор UnitController
    /// </summary>
    /// <param name="unitService"></param>
    public UnitsController(IUnitService unitService)
    {
        this.unitService = unitService;
    }

    /// <summary>
    /// Добавляет единицу измерения
    /// </summary>
    [HttpPost(ApiResource.Units)]
    public async Task<IActionResult> Add(UnitCreateRequest request, CancellationToken token)
    {
        ValidationException<string?>.ThrowIfNull(request.Name);

        var command = new CreateUnitCommand(request.Name);

        await unitService.Add(command, token);
        return Ok();
    }

    /// <summary>
    /// Возвращает все единицы измерения, начиная с offset и заканчиная limit
    /// </summary>
    [HttpGet(ApiResource.Units)]
    public async Task<IReadOnlyList<UnitResponse>> GetAll(int? offset, int? limit, CancellationToken token)
    {
        ValidationException<int?>.ThrowIfNull(offset);
        ValidationException<int?>.ThrowIfNull(limit);
        var units = await unitService.Get(offset.Required(), limit.Required(), token);

        return units.Select(x => new UnitResponse
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();
    }

    /// <summary>
    /// Обновляет единицу измерения
    /// </summary>
    [HttpPut(ApiResource.Units)]
    public async Task<IActionResult> Update(UnitUpdateRequest request, CancellationToken token)
    {
        ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, request.Id, "Идентификатор не может быть пустым");
        ValidationException<string?>.ThrowIfNull(request.Name);

        await unitService.Update(new UpdateUnitCommand(
        
            request.Id ?? Guid.NewGuid(),
            request.Name ?? ""
        ),
        token);

        return Ok();
    }

    /// <summary>
    /// Удаляет единицу измерения
    /// </summary>
    [HttpDelete(ApiResource.UnitsById)]
    public async Task<IActionResult> Remove(Guid? id, CancellationToken token)
    {
        ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, id, "Идентификатор не может быть пустым");

        await unitService.Remove(id ?? Guid.NewGuid(), token);
        return Ok();
    }


    /// <summary>
    /// Возвращает единицу измерения по id
    /// </summary>
    [HttpGet(ApiResource.UnitsById)]
    public async Task<UnitResponse> GetById(Guid? id, CancellationToken token)
    {
        ValidationException<Guid?>.ThrowIf(x => x is null || x == Guid.Empty, id, "Идентификатор не может быть пустым");

        var units = await unitService.GetById(id ?? Guid.NewGuid(), token);

        return new UnitResponse
        {
            Id = units.Id,
            Name = units.Name,
        };
    }

    /// <summary>
    /// Вовращает единицу измерения поназванию
    /// </summary>
    [HttpGet(ApiResource.UnitsByName)]
    public async Task<UnitResponse> GetByName(string? name, CancellationToken token)
    {
        ValidationException<string?>.ThrowIfNull(name);

        var units = await unitService.GetByName(name ?? String.Empty, token);

        if (units is null)
        {
            HttpContext.Response.StatusCode = 404;
        }

        return new UnitResponse
        {
            Id = units?.Id,
            Name = units?.Name,
        };
    }


    /// <summary>
    /// Вовращает все единицы измерения содержащие подстроку namePart в названии
    /// </summary>
    [HttpGet(ApiResource.UnitsByNamePart)]
    public async Task<IReadOnlyList<UnitResponse>> GetByNamePart(
        string? namePart,
        [FromQuery] int? offset,
        [FromQuery] int? limit,
        CancellationToken token)
    {
        ValidationException<int?>.ThrowIfNull(offset);
        ValidationException<int?>.ThrowIfNull(limit);
        ValidationException<string?>.ThrowIfNull(namePart);

        IReadOnlyList<UnitView> units = await unitService.GetByNamePart(namePart.Required(), offset.Required(), limit.Required(), token);

        return units.Select(x => new UnitResponse
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();
    }

    /// <summary>
    /// Возвращает все все единицы измерения ингредиента
    /// </summary>
    [HttpGet(ApiResource.UnitsByIngredient)]
    public async Task<IReadOnlyList<UnitResponse>> GetByIngredient(Guid? ingredient, CancellationToken token)
    {
        if (ingredient is null || ingredient == Guid.Empty)
        {
            throw new ArgumentNullException();
        }
        var units = await unitService.GetByIngredient(ingredient ?? Guid.NewGuid(), token);

        if (units is null)
        {
            HttpContext.Response.StatusCode = 404;
            throw new KeyNotFoundException();
        }

        return units.Select(x => new UnitResponse
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();
    }

}
