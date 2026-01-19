namespace MenuOnWeek.Contracts
{
    /// <summary>
    /// ApiResource
    /// </summary>
    public static class ApiResource
    {
        private const string Prefix = "api/v1";

        /// <summary>
        /// Для единиц измерения
        /// </summary>
        public const string Units = Prefix + "/units";

        /// <summary>
        /// Для единиц измерения
        /// </summary>
        public const string UnitsById = Units + "/{id}";

        /// <summary>
        /// Для единиц измерения
        /// </summary>
        public const string UnitsByName = Units + "/{name}/by-name";

        /// <summary>
        /// Для единиц измерения
        /// </summary>
        public const string UnitsByNamePart = Units + "/{namePart}/by-name-part";

        /// <summary>
        /// Для единиц измерения
        /// </summary>
        public const string UnitsByIngredient = Units + "/{ingredient}/by-ingredient";

        /// <summary>
        /// Для ингредиентов
        /// </summary>
        public const string Ingredients = Prefix + "/ingredients";

        /// <summary>
        /// Для ингредиентов
        /// </summary>
        public const string IngredientsById = Ingredients + "/{id}";

        /// <summary>
        /// Для ингредиентов
        /// </summary>
        public const string IngredientsByName = Ingredients + "/{name}/by-name";

        /// <summary>
        /// Для ингредиентов
        /// </summary>
        public const string IngredientsByNamePart = Ingredients + "/{name-part}/by-name-part";

        /// <summary>
        /// Для рецептов
        /// </summary>
        public const string Recipes = Prefix + "/recipes";

        /// <summary>
        /// Для рецептов
        /// </summary>
        public const string RecipesById = Recipes + "/{id}";

        /// <summary>
        /// Для рецептов
        /// </summary>
        public const string RecipesByName = Recipes + "/{name}/by-name";

        /// <summary>
        /// Для меню
        /// </summary>
        public const string Menus = Prefix + "/menus";

        /// <summary>
        /// Для меню
        /// </summary>
        public const string MenusById = Menus + "/{id}";

        /// <summary>
        /// Для меню
        /// </summary>
        public const string MenusByName = Menus + "/{name}/by-name";

        /// <summary>
        /// Для файлов
        /// </summary>
        public const string Files = Prefix + "/files";

        /// <summary>
        /// Для файлов
        /// </summary>
        public const string FilesById = Files + "/{id}";

        /// <summary>
        /// 
        /// </summary>
        public static string GetPaginationQuery(int limit, int offset)
            => $"?offset={offset}&limit={limit}";
    }
}
