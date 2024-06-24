using System.Globalization;
using System.Windows.Data;

namespace HW22_Drugs.Converters;

public class TimeSpanToString_ValConverter : IValueConverter
{
    public CultureInfo Culture { get; set; }

    public string Format { get; set; }

    public TimeSpanToString_ValConverter()
    {
        //Format = "dd'д' HH'г' mm'хв'";
        Format = "dd'дн 'hh'год 'mm'хв '";
        Culture = new CultureInfo("uk-UA");
    }

    public object Convert(object value, Type targetType, Object parameter, CultureInfo culture)
    => value switch
    {
        null => string.Empty,
        TimeSpan timeSpan => timeSpan.ToString(Format, Culture),
        { } => throw new ArgumentException($"Тип даних параметру {nameof(value)} не підтримується"),
    };

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is not string stringValue) return null;

        List<string> dateTimeFormats = new()
        {
            "mm",
            "h'-'m",
            "hh'-'mm",
            "d'-'h'-'m",
            "dd'-'hh'-'mm",
        };

        return !TimeSpan.TryParseExact(stringValue.Trim(), dateTimeFormats.ToArray(), null, TimeSpanStyles.None, out TimeSpan convertingResult)
            ? null
            : convertingResult;
    }
}
