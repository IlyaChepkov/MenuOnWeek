using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    internal class Quantity
    {
        public Quantity(int count, Unit unit)
        {
            Count = count;
            Unit = unit;
        }

        /// <summary>
        /// Количество
        /// </summary>
        public int Count {  get; set; }

        /// <summary>
        /// Единицы измерения
        /// </summary>
        public Unit Unit { get; set; }

    }
}
