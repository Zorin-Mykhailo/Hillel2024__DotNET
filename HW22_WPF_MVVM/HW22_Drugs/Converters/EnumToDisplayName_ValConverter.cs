using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HW22_Drugs.Converters;

public class EnumToDisplayName_ValConverter : IValueConverter
{
    public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        => value switch
        {
            null => String.Empty,
            Enum enumValue => enumValue.GetDisplayName(),
            { } => throw new ArgumentException($"Не поддерживаемый тип данных параметра {nameof(value)}")
        };



    public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}





[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class FriendlyNameAttribute : Attribute
{
    /// <summary> Дружественное название элемента перечисления </summary>
    public String NameByDefault { get; }

    public FriendlyNameAttribute(String byDefault)
    {
        NameByDefault = byDefault;
    }
}




public static class Ext_Enum
{
    private static readonly Dictionary<Type, Dictionary<String, String>> DictEnumValueToDisplayNames = new();
    //private static Dictionary<Type, Dictionary<String, String>> DictDisplayNameToEnumValue = new();

    public static String GetDisplayName(this Enum? enumValue, String? valueIfNull = null)
    {
        if(enumValue is not Enum value) return String.Empty;
        Type type = value.GetType();
        if(!DictEnumValueToDisplayNames.ContainsKey(type))
            DictEnumValueToDisplayNames.Add(type, new());
        String enumMemberName = value.ToString();
        if(!DictEnumValueToDisplayNames[type].ContainsKey(enumMemberName))
        {
            String enumMemberDisplayName = type.GetMember(enumMemberName)?.FirstOrDefault()?.GetCustomAttribute<FriendlyNameAttribute>()?.NameByDefault ?? valueIfNull ?? $"❗{type.Name}.{enumMemberName}";
            DictEnumValueToDisplayNames[type].Add(enumMemberName, enumMemberDisplayName);
            //DictDisplayNameToEnumValue[type].Add(enumMemberDisplayName, enumMemberName);

            return enumMemberDisplayName;
        }
        return DictEnumValueToDisplayNames[type][enumMemberName];
    }

    //public static String GetEnumValueByDisplayName<T>(this String enumDisplayName) where T : Enum
    //{
    //    return DictDisplayNameToEnumValue[typeof(T)][enumDisplayName];
    //}
}