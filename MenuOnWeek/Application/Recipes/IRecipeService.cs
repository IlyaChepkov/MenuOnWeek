namespace MenuOnWeek.Application.Recipes;

public interface IRecipeService
{
    Task Add(RecipeCreateCommand entity, CancellationToken token);

    Task<IReadOnlyList<RecipeViewCommand>> GetAll(int offset, int limit, CancellationToken token);

    Task Update(RecipeUpdateCommand entity, CancellationToken token);

    Task Remove(Guid id, CancellationToken token);

    Task<RecipeViewCommand> GetById(Guid id, CancellationToken token);

    Task<RecipeViewCommand?> GetByName(string name, CancellationToken token);
}
