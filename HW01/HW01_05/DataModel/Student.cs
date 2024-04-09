using Spectre.Console;

namespace HW01_05;
public class Student : Person
{
    protected double _average;

    public double Average
    {
        get => _average;
        set => _average = value;
    }

    protected int _numberOfGroup;

    public int NumberOfGroup
    {
        get => _numberOfGroup;
        set => _numberOfGroup = value;
    }

    public Student(string surname, string name, int age, string phone, int numberOfGroup, double average) 
        : base(surname, name, age, phone)
    {
        NumberOfGroup = numberOfGroup;
        Average = average;
    }

    public Student Clone()
        => new(Surname, Name, Age, Phone, NumberOfGroup, Average);

    public void CopyFrom(Student anotherStudent)
    {
        Surname = anotherStudent.Surname;
        Name = anotherStudent.Name;
        Age = anotherStudent.Age;
        Phone = anotherStudent.Phone;
        NumberOfGroup = anotherStudent.NumberOfGroup;
        Average = anotherStudent.Average;
    }

    public override void Print(string? title = null)
    {
        string header = string.IsNullOrWhiteSpace(title) ? "[blue]Студент:[/]" : title;
        AnsiConsole.MarkupLine(header);
        Table table = new ();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[blue]Прізвище[/]");
        table.AddColumn("[blue]Ім'я[/]");
        table.AddColumn("[blue]Вік[/]");
        table.AddColumn("[blue]Телефон[/]");
        table.AddColumn("[blue]Група[/]");
        table.AddColumn("[blue]Середній бал[/]");
        table.AddRow(Surname, Name, $"{Age,4}", Phone, $"{NumberOfGroup}", $"{Average,6:0.00}");
        AnsiConsole.Write(table);
    }
}
