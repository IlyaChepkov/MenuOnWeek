using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

internal sealed class IngredientService : IIngredientService
{
    private readonly IIngredientRepository ingredientRepository;

    public IngredientService(IIngredientRepository ingredientRepository)
    {
        this.ingredientRepository = ingredientRepository;
    }

    public void Add(Ingredient entity)
    {
        ingredientRepository.Add(entity);
    }

    public IReadOnlyList<Ingredient> GetAll(Func<Ingredient, bool> predicate, int offset, int limit)
    {
        return ingredientRepository.
            GetAll(predicate).Skip(offset).Take(limit).ToList();
    }

    public void Remove(Ingredient entity)
    {
        ingredientRepository.Remove(entity);
    }

    public void Update(Ingredient entity)
    {
        ingredientRepository.Update(entity);
    }
}
