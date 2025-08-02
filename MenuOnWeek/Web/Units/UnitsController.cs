using System.Net;
using System.Xml.Linq;
using Application.Units;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Units;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace MenuOnWeek.Web.Units;

[ApiController]
public sealed class UnitsController : ControllerBase
{
    private readonly IUnitService unitService;

    public UnitsController(IUnitService unitService)
    {
        this.unitService = unitService;
    }

    [HttpPost(ApiResource.Units)]
    public async Task<IActionResult> Add(UnitCerateRequest request, CancellationToken token)
    {
        // Валидация. Проверить, что в request нормальные данные
        if (String.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentNullException();
        }
        // Конвератция превратить request в command
        var command = new CreateUnitCommand(request.Name);

        // Вызвать слой Application
        await unitService.Add(command, token);
        return Ok();
    }

    [HttpGet(ApiResource.Units)]
    public async Task<IReadOnlyList<UnitResponse>> GetAll(int? limit, int? offset, CancellationToken token)
    {
        // Валидация. Проверить, что в request нормальные данные
        if (!limit.HasValue || !offset.HasValue)
        {
            throw new ArgumentNullException();
        }
        if (limit.Value < 1 || offset.Value < 0)
        {
            throw new ArgumentException();
        }
        // Конвератция превратить request в command

        // Вызвать слой Application
        var units = await unitService.GetAll(offset.Value, limit.Value, token);

        return units.Select(x => new UnitResponse
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();
    }

    [HttpPut(ApiResource.Units)]
    public async Task<IActionResult> Update(UnitUpdateRequest request, CancellationToken token)
    {
        if (request.Id is null || request.Id == Guid.Empty)
        {
            throw new ArgumentNullException();
        }
        if (String.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentNullException();
        }

        await unitService.Update(new UpdateUnitCommand(
        
            request.Id ?? Guid.NewGuid(),
            request.Name ?? ""
        ),
        token);

        return Ok();
    }

    [HttpDelete(ApiResource.UnitsById)]
    public async Task<IActionResult> Remove(Guid? id, CancellationToken token)
    {
        if (id is null || id == Guid.Empty)
        {
            throw new ArgumentNullException();
        }
        await unitService.Remove(id ?? Guid.NewGuid(), token);
        return Ok();
    }

    [HttpGet(ApiResource.UnitsById)]
    public async Task<UnitResponse> GetById(Guid? id, CancellationToken token)
    {
        if (id is null || id == Guid.Empty)
        {
            throw new ArgumentNullException();
        }
        var units = await unitService.GetById(id ?? Guid.NewGuid(), token);

        return new UnitResponse
        {
            Id = units.Id,
            Name = units.Name,
        };
    }

    [HttpGet(ApiResource.UnitsByName)]
    public async Task<UnitResponse> GetByName(string? name, CancellationToken token)
    {
        if (String.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException();
        }
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



    [HttpGet(ApiResource.UnitsByNamePart)]
    public async Task<IReadOnlyList<UnitResponse>> GetByNamePart(
        string? namePart,
        [FromQuery] int? offset,
        [FromQuery] int? limit,
        CancellationToken token)
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

        var units = await unitService.GetByNamePart(namePart, offset.Value, limit.Value, token);

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

    [HttpGet(ApiResource.UnitsByIngredient)]
    public async Task<IReadOnlyList<UnitResponse>> GetByIngredient(Guid? ingredientId, CancellationToken token)
    {
        if (ingredientId is null || ingredientId == Guid.Empty)
        {
            throw new ArgumentNullException();
        }
        var units = await unitService.GetByIngredient(ingredientId ?? Guid.NewGuid(), token);

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
