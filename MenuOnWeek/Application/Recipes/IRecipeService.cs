namespace MenuOnWeek.Application.Recipes;

public interface IRecipeService
{
    void Add(RecipeCreateModel entity);

    IReadOnlyList<RecipeViewModel> GetAll(int offset, int limit);

    void Update(RecipeUpdateModel entity);

    void Remove(Guid id);

    RecipeViewModel GetById(Guid id);

    RecipeViewModel GetByName(string name);
}
