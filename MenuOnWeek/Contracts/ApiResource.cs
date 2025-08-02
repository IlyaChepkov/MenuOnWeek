namespace MenuOnWeek.Contracts
{
    public static class ApiResource
    {
        private const string Prefix = "api/v1";

        public const string Units = Prefix + "/units";

        public const string UnitsById = Units + "/{id}";

        public const string UnitsByName = Units + "/{name}/by-name";

        public const string UnitsByNamePart = Units + "/{name-part}/by-name-part";

        public const string UnitsByIngredient = Units + "/{ingredient}/by-ingredient";

        public const string Ingredients = Prefix + "/ingredients";

        public const string IngredientsById = Ingredients + "/{id}";

        public const string IngredientsByName = Ingredients + "/{name}/by-name";

        public const string IngredientsByNamePart = Ingredients + "/{name-part}/by-name-part";

        public const string Recipes = Prefix + "/recipes";

        public const string RecipesById = Recipes + "/{id}";

        public const string RecipesByName = Recipes + "/{name}/by-name";

        public const string Menus = Prefix + "/menus";

        public const string MenusById = Menus + "/{id}";

        public const string MenusByName = Menus + "/{name}/by-name";
    }
}
