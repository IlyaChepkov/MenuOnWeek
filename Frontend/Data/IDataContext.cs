using Domain;

namespace Data;

public interface IDataContext
{
    List<Menu> Menus { get; init; }

    List<Recipe> Recipes { get; init; }

    List<Ingredient> Ingredients { get; init; }

    List<Unit> Units { get; init; }

    void Save();
}
