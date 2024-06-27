using PhoneBook.Models;
using PhoneBook.ViewModels;
using System.Globalization;

namespace PhoneBook.Converters;

public class ItemViewModelToTitleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is not ItemViewModel item) return "<Некоректний тип даних>";
        return item.ID == 0 ? "<Новий контакт>" : $"{item.LastName} {item.FirstName}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException("Зворотнє перетвонення не підтримується");
    }
}
