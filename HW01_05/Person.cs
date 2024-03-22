using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW01_05;
public class Person
{
    protected string _name;
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    protected string _surname;
    public string Surname
    {
        get => _surname;
        set => _surname = value;
    }

    protected int _age;
    public int Age
    {
        get => _age;
        set => _age = value;
    }

    protected string _phone;
    public string Phone
    {
        get => _phone;
        set => _phone = value;
    }

    public Person(string surname, string name, int age, string phone)
    {
        Surname = surname;
        Name = name;
        Age = age;
        Phone = phone;
    }

    public virtual void Print(string? title = null)
    {
        string header = string.IsNullOrWhiteSpace(title) ? "[blue]Персона:[/]" : title;
        AnsiConsole.MarkupLine(header);
        Table table = new ();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[blue]Прізвище[/]");
        table.AddColumn("[blue]Ім'я[/]");
        table.AddColumn("[blue]Вік[/]");
        table.AddColumn("[blue]Телефон[/]");
        table.AddRow(Surname, Name, $"{Age,4}", Phone);
        AnsiConsole.Write(table);
    }
}
