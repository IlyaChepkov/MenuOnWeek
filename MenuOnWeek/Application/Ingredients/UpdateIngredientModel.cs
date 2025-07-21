using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Units;

namespace Application.Ingredients;

public sealed class UpdateIngredientModel
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required int Price { get; set; }

    public required Guid UnitId { get; set; }

    public required Dictionary<UnitViewModel, double> Table { get; set; } = new Dictionary<UnitViewModel, double>();
}
