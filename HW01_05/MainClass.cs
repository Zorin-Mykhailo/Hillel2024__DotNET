using Spectre.Console;
using System.Text;

namespace HW01_05;
public  class MainClass
{
    private AcademyGroup _academyGroup {  get; set; }

    public MainClass()
    {
        _academyGroup = new AcademyGroup();
    }

    

    public void ShowMenu()
    {        
        bool programContinue = true;
        do
        {
            string selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Виберіть один із пунктів [green]МЕНЮ[/]?")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
            .AddChoices(new[]
            {
                Menu.DataRecortsFind,
                Menu.DataRecortAdd,
                Menu.DataRecortEdit,
                Menu.DataRecortRemove,
                Menu.DataSave,
                Menu.DataLoad,
                Menu.ProgramExit
            }));

            Console.Clear();
            ShowProgramName();

            switch(selection)
            {
                case Menu.DataRecortsFind:
                    DataRecortsFind();
                    break;
                case Menu.DataRecortAdd:
                    DataRecortAdd();
                    break;
                case Menu.DataRecortEdit:
                    DataRecortEdit();
                    break;
                case Menu.DataRecortRemove:
                    DataRecortRemove();
                    break;
                case Menu.DataSave:
                    DataSave();
                    break;
                case Menu.DataLoad:
                    DataLoad();
                    break;
                case Menu.ProgramExit:
                    ProgramExit();
                    programContinue = false;
                    break;
            }
        } while(programContinue);        
    }

    private void DataRecortsFind()
    {
        AnsiConsole.MarkupLine("[red]Пошук записів не реалізовано[/]");
        _academyGroup.Print();
    }

    private void DataRecortAdd()
    {
        string studentSurname = AskStudentSurname();
        string studentName = AskStudentName();
        int studentAge = AskStudentAge();
        String studentPhone = AskStudentPhone();
        AnsiConsole.MarkupLine("[red]Додання записів не реалізовано повністю[/]");
        _academyGroup.Print();
    }

    private void DataRecortEdit()
    {
        AnsiConsole.MarkupLine("[red]Редагування запису не реалізовано[/]");
        _academyGroup.Print();
    }

    private void DataRecortRemove()
    {
        AnsiConsole.MarkupLine("[red]Видалення даних не реалізовано[/]");
        _academyGroup.Print();
    }

    private void DataSave()
    {
        _academyGroup.Save();
        _academyGroup.Print();
    }

    private void DataLoad()
    {
        _academyGroup.Load();
        _academyGroup.Print();
    }

    private void ProgramExit()
    {
        AnsiConsole.MarkupLine($"[red]Програму завершено[/]");
    }

    public void ShowProgramName()
    {
        Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;
        //
        Table table = new ();
        table.Alignment(Justify.Center);
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn(new Markup("[blue] ДЗ 01.05 • Academy_Group [/]")));
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("");
    }

    private string AskStudentName()
    {
        string? name = null;
        do
        {
            name = AnsiConsole.Ask<string>("[green]Ім'я[/] студента:");
            if(!IsValid(name.Trim()))
                AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Значення не повинно бути пустим та повино мати більше однієї літери[/]");
        } while(!IsValid(name.Trim()));
        return name!;
        bool IsValid(string? name) => !string.IsNullOrWhiteSpace(name) && name.Length > 1;
    }

    private string AskStudentSurname()
    {
        string? name = null;
        do
        {
            name = AnsiConsole.Ask<string>("[green]Прізвище[/] студента:");
            if(!IsValid(name.Trim()))
                AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Значення не повинно бути пустим та повино мати більше однієї літери[/]");
        } while(!IsValid(name.Trim()));
        return name!;
        bool IsValid(string? name) => !string.IsNullOrWhiteSpace(name) && name.Length > 1;
    }

    private static int AskStudentAge()
    {
        int age = 0;
        do
        {
            age = AnsiConsole.Ask<int>("[green]Вік[/] студента:");
            if(!IsValid(age))
                AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Вік студента повинен бути >= 15 та <= 80 років[/]");
        } while(age < 0);
        return age;
        bool IsValid(int age) => age > 15 && age < 80;
    }

    private string AskStudentPhone()
    {
        string? phone = null;
        do
        {
            phone = AnsiConsole.Ask<string>("[green]Телефон[/] студента:");
            if(!IsValid(phone.Trim()))
                AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Значення повинно містити символи лише з наботу \"9876543210 -()\" та повинно містити рівно 10 цифр[/]");
        } while(!IsValid(phone.Trim()));
        return phone!;
        
        bool IsValid(string? phone)
        {
            HashSet<char> allowedSymbols = new ("9876543210 -()");
            bool isPreValid = !string.IsNullOrWhiteSpace(phone) 
                && phone.All(symb => allowedSymbols.Contains(symb));
            if(!isPreValid)
                return false;
            int digitsCount = 0;
            foreach(char symb in phone)
                if(char.IsDigit(symb)) ++digitsCount;
            return digitsCount == 10;
        }
    }
}

public static class DataInput
{
    
}

public static class Menu
{
    public const string DataRecortsFind = "🔍 Знайти записи *";
    public const string DataRecortAdd = "➕ Додати запис *";
    public const string DataRecortEdit = "✏️ Редагувати запис *";
    public const string DataRecortRemove = "❌ Видадлити запис *";
    public const string DataSave = "💾 Зберегти дані";
    public const string DataLoad = "📥 Завантажити дані";
    public const string ProgramExit = "⛔ Вихід";
}
