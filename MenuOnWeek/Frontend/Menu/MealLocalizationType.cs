using System.ComponentModel;
using System.Reflection;
using System.Xml.Linq;
using MenuOnWeek.Domain;
using Utils;

namespace MenuOnWeek.Frontend.Menu;

internal class MealLocalizationType
{
    private Meal? mealType;
    public string MealName { get; private set; }
    public Meal? MealType => mealType;
    public MealLocalizationType(Meal? type)
    {
        mealType = type;
        MealName = ToString();
    }

    override public string ToString()
    {
        if (MealType is not null)
        {
            FieldInfo? fi = typeof(Meal).GetField(mealType.ToString().Required()).Required();
            DescriptionAttribute[] descAttrs = (fi.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                   as DescriptionAttribute[]).Required();
            if (descAttrs.Length > 0)
            {
                return descAttrs[0].Description.ToString();
            }
            return mealType.ToString().Required();
        }
        else
        {
            return "";
        }
    }
}
