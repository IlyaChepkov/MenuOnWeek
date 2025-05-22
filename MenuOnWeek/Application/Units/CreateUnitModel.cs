using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Units;

/// <summary>
/// Модель создания единицы измерения
/// </summary>
public sealed class CreateUnitModel
{
    /// <summary>
    /// Имя единицы измерения
    /// </summary>
    public required string Name { get; set; }
}
