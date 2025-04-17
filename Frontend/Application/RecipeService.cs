using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

internal sealed class RecipeService : IRecipeService
{
    private readonly IRecipeRepository recipeRepository;

    public RecipeService(IRecipeRepository recipeRepository)
    {
        this.recipeRepository = recipeRepository;
    }

    public void Add(Recipe entity)
    {
        recipeRepository.Add(entity);
    }

    public IReadOnlyList<Recipe> GetAll(Func<Recipe, bool> predicate, int offset, int limit)
    {
        return recipeRepository.
            GetAll(predicate).Skip(offset).Take(limit).ToList();
    }

    public void Remove(Recipe entity)
    {
        recipeRepository.Remove(entity);
    }

    public void Update(Recipe entity)
    {
        recipeRepository.Update(entity);
    }
}
