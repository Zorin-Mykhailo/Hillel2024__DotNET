using System.Globalization;
using System.Windows.Data;

namespace HW22_Drugs.Converters;

public class DateTime2ToString_ValConverter : IValueConverter
{
    public CultureInfo Culture { get; set; }

    public String Format { get; set; }

    public DateTime2ToString_ValConverter()
    {
        Format = "yyyy.MM.dd(ddd)   \nHH:mm:ss";
        Culture = new CultureInfo("uk-UA");
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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
            "dd.MM.yyyy HH:mm",
            "dd.MM.yyyy HH:mm:ss",
            "d.M.yyyy H:m",
            "d.M.yyyy H:m:s",
            "yyyy.MM.dd HH:mm",
            "yyyy.MM.dd HH:mm:ss",
            "yyyy.M.d H:m",
            "yyyy.M.d H:m:s",
            "dd-MM-yyyy HH-mm",
            "dd-MM-yyyy HH-mm-ss",
            "d-M-yyyy H-m",
            "d-M-yyyy H-m-s",
            "yyyy-MM-dd HH-mm",
            "yyyy-MM-dd HH-mm-ss",
            "yyyy-M-d H-m",
            "yyyy-M-d H-m-s",
        };

        return !DateTime.TryParseExact(stringValue.Trim(), dateTimeFormats.ToArray(), null, DateTimeStyles.None, out DateTime convertingResult)
            ? null
            : convertingResult;
    }
}