using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    internal class MenuElement
    {

        public MenuElement(Recipe recipe, int serve, DateTime date)
        {

            Recipe = recipe;
            Serve = serve;
            Date = date;

        }

        /// <summary>
        /// Рецепт
        /// </summary>
        public Recipe Recipe {  get; set; }

        /// <summary>
        /// Количество порций
        /// </summary>
        public int Serve {  get; set; }

        /// <summary>
        /// Дата подачи
        /// </summary>
        public DateTime Date { get; set; }

    }
}
