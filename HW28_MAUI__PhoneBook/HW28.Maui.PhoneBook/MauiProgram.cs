using HW28.Maui.PhoneBook.Data;
using HW28.Maui.PhoneBook.Views;
using Microsoft.Extensions.Logging;

namespace HW28.Maui.PhoneBook;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        builder.Services.AddSingleton<ItemsSetPage>();
        builder.Services.AddTransient<ItemPage>();
        
        builder.Services.AddSingleton<AppDatabase>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
