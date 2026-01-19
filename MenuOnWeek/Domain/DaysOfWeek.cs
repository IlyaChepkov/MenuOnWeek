using System.ComponentModel;

namespace MenuOnWeek.Domain;

/// <summary>
/// Дни недели
/// </summary>
public enum DaysOfWeek
{
    /// <summary>
    /// Понедельник
    /// </summary>
    [Description("Понедельник")]
    Monday,

    /// <summary>
    /// Вторник
    /// </summary>
    [Description("Вторник")]
    Tuesday,

    /// <summary>
    /// Среда
    /// </summary>
    [Description("Среда")]
    Wednesday,

    /// <summary>
    /// Четверг
    /// </summary>
    [Description("Четверг")]
    Thursday,

    /// <summary>
    /// Пятница
    /// </summary>
    [Description("Пятница")]
    Friday,

    /// <summary>
    /// Суббота
    /// </summary>
    [Description("Суббота")]
    Saturday,

    /// <summary>
    /// Воскресенье
    /// </summary>
    [Description("Воскресенье")]
    Sunday,
}
