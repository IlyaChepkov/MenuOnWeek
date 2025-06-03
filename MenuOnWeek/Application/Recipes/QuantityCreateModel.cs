using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Units;

namespace MenuOnWeek.Application.Recipes;

public sealed class QuantityModel
{
    public int Count { get; set; }

    public Guid UnitId { get; set; } 
}
