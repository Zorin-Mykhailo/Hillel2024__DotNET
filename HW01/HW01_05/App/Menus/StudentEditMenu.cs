namespace HW01_05;

public static class StudentEditMenu
{
    public const string EditSurname = "Редагувати [yellow]прізвище[/]";
    public const string EditName = "Редагувати [yellow]ім'я[/]";
    public const string EditAge = "Редагувати [yellow]вік[/]";
    public const string EditPhone = "Редагувати [yellow]телефон[/]";
    public const string EditGroupNumber = "Редагувати [yellow]№ групи[/]";
    public const string EditAvgGrade = "Редагувати [yellow]середній бал[/]";
    public const string Cancel = "↩️ Відмінити";
    public const string Save = "💾 Зберегти зміни";

    public static string Show()
    {
        return Menu.Show("Меню [green]редагування[/] обраного [green]студента[/]", new[]
            {
                EditSurname,
                EditName,
                EditAge,
                EditPhone,
                EditGroupNumber,
                EditAvgGrade,
                Cancel,
                Save
            });
    }

    public static bool WorkIsFinished(string userChoice)
        => userChoice == Cancel || userChoice == Save;
}
