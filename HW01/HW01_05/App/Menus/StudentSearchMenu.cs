namespace HW01_05;

public static class StudentSearchMenu
{
    public const string Reset = "Новий пошук";
    public const string SearchByOrder = "Пошук за [yellow]порядковим номером[/]";
    public const string SearchByName = "Пошук за полем [yellow]ім'я[/]";
    public const string SearchBySurname = "Пошук за полем [yellow]прізвище[/]";
    public const string SearchByAge = "Пошук за полем [yellow]вік[/]";
    public const string SearchByPhone = "Пошук за полем [yellow]телефон[/]";
    public const string SearchByGroupNumber = "Пошук за полем [yellow]№ групи[/]";
    public const string SearchByAvgGrade = "Пошук за полем [yellow]середній бал[/]";
    //public const string Actions = "Дії";
    public const string OK = "OK";

    public static string Show()
    {
        return Menu.Show("Меню [green]пошуку студентів[/]", new[]
            {
                SearchBySurname,
                SearchByName,
                SearchByAge,
                SearchByPhone,
                SearchByGroupNumber,
                SearchByAvgGrade,
                Reset,
                OK
            });
    }

    public static bool WorkIsFinished(string userChoice)
        => userChoice == OK;
}