using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using MenuOnWeek.Application.Ingredients;

namespace MenuOnWeek.Data.Ingredients;

internal sealed class IngredientUnitsRepository : BaseRepository<IngredientUnits>, IIngredientUnitsRepository
{
    public IngredientUnitsRepository(DataContext dataContext) : base(dataContext)
    {
        
    }
}
