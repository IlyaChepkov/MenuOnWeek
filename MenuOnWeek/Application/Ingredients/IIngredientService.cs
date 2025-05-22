using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ingredients;

public interface IIngredientService
{
    void Add(CreateIngredientModel entity);

    IReadOnlyList<IngredientViewModel> GetAll(int offset, int limit);

    void Update(UpdateIngredientModel entity);

    void Remove(Guid entity);

    IngredientViewModel GetById(Guid id);

    IngredientViewModel GetByName(string name);

    IReadOnlyList<IngredientViewModel> GetByPartName(string namePart, int offset, int limit);
}
