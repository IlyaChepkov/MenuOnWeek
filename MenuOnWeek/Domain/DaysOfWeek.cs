using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuOnWeek.Domain;

public enum DaysOfWeek
{
    [Description("Понедельник")]
    Monday,
    [Description("Вторник")]
    Tuesday,
    [Description("Среда")]
    Wednesday,
    [Description("Четверг")]
    Thursday,
    [Description("Пятница")]
    Friday,
    [Description("Суббота")]
    Saturday,
    [Description("Воскресенье")]
    Sunday,
}
