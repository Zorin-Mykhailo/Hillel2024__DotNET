using PhoneBook.Models;
using System.Globalization;

namespace PhoneBook.Converters;

public class ItemToTitleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is not Item item) return "<Некоректний тип даних>";
        return item.ID == 0 ? "<Новий контакт>" : $"{item.LastName} {item.FirstName}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException("Зворотнє перетвонення не підтримується");
    }
}
