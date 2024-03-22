namespace HW01_05;

public static class MainMenu
{
    public const string DataRecortsFind = "🔍 Знайти записи";
    public const string DataRecortAdd = "➕ Додати запис";
    public const string DataRecortEdit = "✏️ Редагувати запис";
    public const string DataRecortRemove = "❌ Видадлити запис";
    public const string DataSave = "💾 Зберегти дані";
    public const string DataLoad = "📥 Завантажити дані";
    public const string ProgramExit = "⛔ Вихід";

    public static string Show()
    {
        return Menu.Show("Виберіть один із пунктів [green]МЕНЮ[/]?", new[]
            {
                DataRecortsFind,
                DataRecortAdd,
                DataRecortEdit,
                DataRecortRemove,
                DataSave,
                DataLoad,
                ProgramExit
            });
    }
}
