using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Domain;
public sealed class IngredientUnits
{
    private IngredientUnits(Guid ingredientId, Guid unitId, double coeficient)
    {
        IngredientId = ingredientId;
        UnitId = unitId;
        Coeficient = coeficient;
    }

    public Ingredient? Ingredient { get; set; }

    public Guid IngredientId { get; set; }

    public Unit? Unit { get; set; }

    public Guid UnitId { get; set; }

    public double Coeficient { get; set; }

    public static IngredientUnits Create(Guid ingredientId, Guid unitId, double coeficient)
    {
        return new IngredientUnits(ingredientId, unitId, coeficient);
    }
}
