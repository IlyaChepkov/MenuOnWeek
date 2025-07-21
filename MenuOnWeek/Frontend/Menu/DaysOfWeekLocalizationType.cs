using System.ComponentModel;
using System.Reflection;
using MenuOnWeek.Domain;
using Utils;

namespace MenuOnWeek.Frontend.Menu;

internal sealed class DaysOfWeekLocalizationType
{
    private DaysOfWeek? daysOfWeekType;
    public string DayName { get; private set; }
    public DaysOfWeek? DaysOfWeekType => daysOfWeekType;
    public DaysOfWeekLocalizationType(DaysOfWeek? type)
    {
        daysOfWeekType = type;
        DayName = ToString();
        //RegionName = GetLocString(); 
    }

    public override string ToString()
    {
        if (daysOfWeekType is not null)
        {
            FieldInfo fi = typeof(DaysOfWeek).GetField(daysOfWeekType.ToString().Required()).Required();
            DescriptionAttribute[] descAttrs = (fi.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                   as DescriptionAttribute[]).Required();
            if (descAttrs.Length > 0)
            {
                return descAttrs[0].Description.ToString();
            }
            return daysOfWeekType.ToString().Required();
        }
        else
        {
            return "";
        }
    }
}
