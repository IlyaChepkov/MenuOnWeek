using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public interface IRecipeService
{
    void Add(Recipe entity);

    IReadOnlyList<Recipe> GetAll(Func<Recipe, bool> predicate, int offset, int limit);

    void Update(Recipe entity);

    void Remove(Recipe entity);
}
