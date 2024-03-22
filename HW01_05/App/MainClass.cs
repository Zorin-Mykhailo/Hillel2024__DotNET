using Spectre.Console;
using System;
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
        List<Student> students = _academyGroup.GetAllStudents();
        string userChoice = null!;
        do
        {
            Console.Clear();
            ShowProgramName();
            AcademyGroup.Print(students, "[black on yellow]Знайдені записи[/]");
            userChoice = StudentSearchMenu.Show();
            switch(userChoice)
            {
                case StudentSearchMenu.SearchByName:
                    string name = Ask.SearchFilter.Name();
                    students = _academyGroup.SearchByName(students, name);
                    break;
                case StudentSearchMenu.SearchBySurname:
                    string surname = Ask.SearchFilter.Surname();
                    students = _academyGroup.SearchBySurname(students, surname);
                    break;
                case StudentSearchMenu.SearchByAge:
                    int minAge = Ask.SearchFilter.MinAge();
                    int maxAge = Ask.SearchFilter.MaxAge(minAge);
                    students = _academyGroup.SearchByAge(students, minAge, maxAge);
                    break;
                case StudentSearchMenu.SearchByPhone:
                    string phone = Ask.SearchFilter.Phone();
                    students = _academyGroup.SearchByPhone(students, phone);
                    break;
                case StudentSearchMenu.SearchByGroupNumber:
                    int minGroupNumb = Ask.SearchFilter.MinGroupNumber();
                    int maxGroupNumb = Ask.SearchFilter.MaxGroupNumber(minGroupNumb);
                    students = _academyGroup.SearchByGroup(students, minGroupNumb, maxGroupNumb);
                    break;
                case StudentSearchMenu.SearchByAvgGrade:
                    double minAvgGrade = Ask.SearchFilter.MinAvgGrade();
                    double maxAvgGrade = Ask.SearchFilter.MaxAvgGrade(minAvgGrade);
                    students = _academyGroup.SearchByAvgGrade(students, minAvgGrade, maxAvgGrade);
                    break;
                case StudentSearchMenu.Reset:
                    students = _academyGroup.GetAllStudents();
                    break;
                default: break;
            }
        } while(!StudentSearchMenu.WorkIsFinished(userChoice));
        Console.Clear();
        ShowProgramName();
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

    private Student? SelectSingleStudent()
    {
        if(_academyGroup.Count == 0) return null;
        Student? selectedRecord = null;
        do
        {
            string surname = Ask.Student.Surname();
            List<Student> foundedStudents = _academyGroup.SearchBySurname(surname);
            if(foundedStudents.Count == 0)
                AnsiConsole.MarkupLine("[yellow]Не знайдено жодного запису[/]");
            else if(foundedStudents.Count == 1)
                selectedRecord = _academyGroup.SearchBySurname(surname).First();
            else
            {
                AnsiConsole.MarkupLine("[yellow]Знайдено більше одного запису. Оберіть порядковий номер запису, з яким необхідно продовжити роботу[/]");
                AcademyGroup.Print(foundedStudents, "[yellow]Знайдені записи[/]");
                int recordIndex = Ask.Record.OrderNumber(foundedStudents.Count - 1);
                selectedRecord = foundedStudents[recordIndex];
            }
        } while(selectedRecord == null);
        return selectedRecord;
    }

    private void DataRecortEdit()
    {
        Student? selectedRecord = SelectSingleStudent();
        if(selectedRecord == null)
        {
            Console.Clear();
            ShowProgramName();
            AnsiConsole.MarkupLine("[red]Записи для редагування відсутні[/]");
            _academyGroup.Print();
            return;
        }

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
        Student? selectedRecord = SelectSingleStudent();
        if(selectedRecord == null)
        {
            Console.Clear();
            ShowProgramName();
            AnsiConsole.MarkupLine("[red]Записи для видалення відсутні[/]");
            _academyGroup.Print();
            return;
        }

        selectedRecord.Print("[black on red] Запис обраний для видалення [/]");
        string userChoice = StudentDeleteMenu.Show();

        Console.Clear();
        ShowProgramName();
        if(userChoice == StudentDeleteMenu.Delete)
        {
            bool isDeleted = _academyGroup.Remove(selectedRecord);
            if(_academyGroup.Remove(selectedRecord))
                AnsiConsole.MarkupLine("[yellow]Запис видалено[/]");
            else
                AnsiConsole.MarkupLine("[red]Не вдалось видалити запис[/]");
        }
        else
            AnsiConsole.MarkupLine("[yellow]Видалення відмінено[/]");

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