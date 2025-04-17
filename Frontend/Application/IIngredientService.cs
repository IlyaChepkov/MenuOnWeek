using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public interface IIngredientService
{
    void Add(Ingredient entity);

    IReadOnlyList<Ingredient> GetAll(Func<Ingredient, bool> predicate, int offset, int limit);

    void Update(Ingredient entity);

    void Remove(Ingredient entity);
}
