namespace Domain
{
    public sealed class Ingredient
    {
        public Ingredient(string name, int price, int id, Unit unit)
        {
            Name = name;
            Price = price;
            ID = id;
            Unit = unit;
            Table = new Dictionary<Unit, double>();
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

        public Dictionary<Unit, double> Table { get; set; }
    }
}
