namespace HW01_05;

public static class StudentDeleteMenu
{
    public const string Delete = "Видалити";
    public const string Cancel = "Відміна";

    public static string Show()
    {
        return Menu.Show("Меню [green]пошуку студентів[/]", new[]
            {
                Delete,
                Cancel
            });
    }

    public static bool WorkIsFinished(string userChoice)
        => userChoice == Delete || userChoice == Cancel;
}
