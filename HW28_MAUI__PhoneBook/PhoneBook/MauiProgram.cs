using Microsoft.Extensions.Logging;
using PhoneBook.Data;
using PhoneBook.Views;

namespace PhoneBook;
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
