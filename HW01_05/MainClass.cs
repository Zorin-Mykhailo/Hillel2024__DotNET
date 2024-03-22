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

    

    public void TreatingUserCommands()
    {        
        bool programContinue = true;
        do
        {
            string userChoice = MainMenu.Show();
            Console.Clear();
            ShowProgramName();

            switch(userChoice)
            {
                case MainMenu.DataRecortsFind:
                    DataRecortsFind();
                    break;
                case MainMenu.DataRecortAdd:
                    DataRecortAdd();
                    break;
                case MainMenu.DataRecortEdit:
                    DataRecortEdit();
                    break;
                case MainMenu.DataRecortRemove:
                    DataRecortRemove();
                    break;
                case MainMenu.DataSave:
                    DataSave();
                    break;
                case MainMenu.DataLoad:
                    DataLoad();
                    break;
                case MainMenu.ProgramExit:
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
        string surname = Ask.Student.Surname();
        string name = Ask.Student.Name();
        int age = Ask.Student.Age();
        string phone = Ask.Student.Phone();
        int groupNumber = Ask.Student.GroupNumber();
        double avgGrade = Ask.Student.AvgGrade();

        Student student = new (surname, name, age, phone, groupNumber, avgGrade);

        _academyGroup.Add(student);
        _academyGroup.Print();
    }

    private void DataRecortEdit()
    {
        Student? selectedRecord = null;
        do
        {
            string surname = Ask.Student.Surname();
            List<Student> foundedStudents = _academyGroup.SearchBySurname(surname);
            if(foundedStudents.Count == 0)
                AnsiConsole.MarkupLine("[yellow]Не знайдено жодного запису для редагування[/]");
            else if(foundedStudents.Count == 1)
                selectedRecord = _academyGroup.SearchBySurname(surname).First();
            else
                AnsiConsole.MarkupLine("[yellow]Не знайдено більше одного запису[/]");
        } while(selectedRecord == null);

        Student editableRecord = selectedRecord.Clone();

        string userChoice = null!;
        do
        {
            Console.Clear();
            ShowProgramName();
            selectedRecord.Print("[black on yellow] Редагування запису: [/]");
            editableRecord.Print("[yellow]Нові дані:[/]");

            userChoice = StudentEditMenu.Show();
            switch(userChoice)
            {
                case StudentEditMenu.EditSurname:
                    editableRecord.Surname = Ask.Student.Surname();
                    break;
                case StudentEditMenu.EditName:
                    editableRecord.Name = Ask.Student.Name();
                    break;
                case StudentEditMenu.EditAge:
                    editableRecord.Age = Ask.Student.Age();
                    break;
                case StudentEditMenu.EditPhone:
                    editableRecord.Phone = Ask.Student.Phone();
                    break;
                case StudentEditMenu.EditGroupNumber:
                    editableRecord.NumberOfGroup = Ask.Student.GroupNumber();
                    break;
                case StudentEditMenu.EditAvgGrade:
                    editableRecord.Average = Ask.Student.AvgGrade();
                    break;
                default:
                    break;
            }
        }while(!StudentEditMenu.WorkIsFinished(userChoice));

        Console.Clear();
        ShowProgramName();
        if(userChoice == StudentEditMenu.Save)
        {
            selectedRecord.CopyFrom(editableRecord);
            AnsiConsole.MarkupLine("[yellow]Дані збережено[/]");
        }
        else if(userChoice == StudentEditMenu.Cancel)
            AnsiConsole.MarkupLine("[yellow]Редагування відмінено[/]");
        else
            AnsiConsole.MarkupLine("[red]Сталась помилка[/]");
        
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
}