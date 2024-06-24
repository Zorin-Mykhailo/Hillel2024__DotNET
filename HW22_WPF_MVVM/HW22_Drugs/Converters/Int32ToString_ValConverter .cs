using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Data;

namespace HW22_Drugs.Converters;

public class Int32ToString_ValConverter : IValueConverter
{
    public string Format { get; set; }

    private static readonly NumberFormatInfo NumberFormatInfo;

    static Int32ToString_ValConverter()
    {
        NumberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        NumberFormatInfo.NumberGroupSeparator = " ";
    }

    public Int32ToString_ValConverter()
    {
        Format = ",#,0";
    }


    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value switch
        {
            null => string.Empty,
            int intVal => intVal.ToString(Format, NumberFormatInfo),
            { } => throw new ArgumentException($"Тип даних параметра {nameof(value)} не підтримується"),
        };

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is not string stringValue) return null;

        return !int.TryParse(stringValue.Trim(), out int convertingResult)
            ? null
            : convertingResult;
    }
}