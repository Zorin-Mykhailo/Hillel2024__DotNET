using Spectre.Console;

namespace HW01_05;

public static class Menu
{
    public static string Show(string title, params string[] menuItems)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title(title)
            .PageSize(10)
            .MoreChoicesText("[grey](Натисткайте стрілки вверх та вниз для навігації по меню)[/]")
            .AddChoices(menuItems));
    }
}
