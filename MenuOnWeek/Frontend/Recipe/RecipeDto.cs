using System.Security.Cryptography.X509Certificates;
using Application.Ingredients;
using MenuOnWeek.Frontend.Ingredient;

namespace MenuOnWeek.Frontend.Recipe;

public sealed record RecipeDto(string Name,
    string Description,
    string? Image,
    Dictionary<Guid, QuantityDto> Ingredients,
    bool IsImageChanged

);
