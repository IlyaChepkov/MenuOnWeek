namespace Domain
{
    /// <summary>
    /// Сущность ингридиента
    /// </summary>
    public sealed class Ingredient
    {
        public Ingredient(string name, int price, int id, Unit unit)
        {
            Name = name;
            Price = price;
            ID = id;
            Unit = unit;
        }

        /// <summary>
        /// Название ингредиента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена ингридиента 
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// ID ингридиента
        /// </summary>
        public int ID {  get; set; }

        /// <summary>
        /// Единица измерения ингридиента
        /// </summary>
        public Unit Unit {  get; set; }
    }
}
