using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MenuOnWeek.Application;

namespace Data;

public interface IRecipeRepository : IEntityWithIdRepository<Recipe>
{
    Task<Recipe?> GetByName(string name, CancellationToken token);
}
