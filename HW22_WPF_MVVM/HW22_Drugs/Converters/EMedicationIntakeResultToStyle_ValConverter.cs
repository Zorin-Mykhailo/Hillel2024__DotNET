using HW22_Drugs.Model;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HW22_Drugs.Converters;

public class EMedicationIntakeResultToStyle_ValConverter : IValueConverter
{
    public Style StyleIfTaken { get; set; } = default!;

    public Style StyleIfSkiped { get; set; } = default!;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value switch
        {
            null => string.Empty,
            EMedicationIntakeResult val => val == EMedicationIntakeResult.Skiped ? StyleIfSkiped : StyleIfTaken,
            { } => throw new ArgumentException($"Тип даних параметра {nameof(value)} не підтримується"),
        };

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}