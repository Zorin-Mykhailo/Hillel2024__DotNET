using System.Globalization;
using System.Windows.Data;

namespace HW22_Drugs.Converters;

public class DateToString_ValConverter : IValueConverter
{
    public CultureInfo Culture { get; set; }

    public String Format { get; set; }

    public DateToString_ValConverter()
    {
        Format = "yyyy.MM.dd(ddd)";
        Culture = new CultureInfo("uk-UA");
    }

    public object Convert(object value, Type targetType, Object parameter, CultureInfo culture)
    => value switch
    {
        null => string.Empty,
        DateTime dateTimeVal => dateTimeVal.ToString(Format, Culture),
        { } => throw new ArgumentException($"Тип даних параметру не підтримується {nameof(value)}"),
    };

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is not string stringValue) return null;

        List<string> dateTimeFormats = new()
        {
            "dd.MM.yyyy",
            "dd-MM-yyyy",
            "d.M.yyyy",
            "d-M-yyyy",
            "yyyy.MM.dd",
            "yyyy-MM-dd",
            "yyyy.M.d",
            "yyyy-M-d",
        };

        return !DateTime.TryParseExact(stringValue.Trim(), dateTimeFormats.ToArray(), null, DateTimeStyles.None, out DateTime convertingResult)
            ? null
            : convertingResult;
    }
}