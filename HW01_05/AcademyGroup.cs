using Newtonsoft.Json;
using Spectre.Console;
using System.IO.Enumeration;
using System.Numerics;
using System.Xml.Linq;

namespace HW01_05;
public class AcademyGroup
{
    private List<Student> _students;
    
    public int Count { get => _students.Count; }

    public AcademyGroup()
    {
        _students = new ();
    }

    public void Add(Student student)
    {
        _students.Add(student);
    }

    public bool Remove(string surname)
    {
        Student? student = _students.FirstOrDefault(s => s.Surname == surname);
        if(student == null) return false;
        _students.Remove(student);
        return true;
    }

    public Student? Edit(string surname)
    {
        return _students.FirstOrDefault(s => s.Surname == surname);
    }

    public void Print()
    {
        AnsiConsole.MarkupLine($"[blue]Академічна група ({Count} чол.):[/]");
        Table table = new ();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[blue]Прізвище[/]");
        table.AddColumn("[blue]Ім'я[/]");
        table.AddColumn("[blue]Вік[/]");
        table.AddColumn("[blue]Телефон[/]");
        table.AddColumn("[blue]Група[/]");
        table.AddColumn("[blue]Середній бал[/]");

        _students.ForEach(s => table.AddRow(s.Surname, s.Name, $"{s.Age,4}", s.Phone, $"{s.NumberOfGroup}", $"{s.Average,6:0.00}"));
        
        AnsiConsole.Write(table);
    }

    private const string DataFileRelativePath = @"..\..\..\AppData\Data.json";

    public void Save()
    {
        string dataFilePath = Path.GetFullPath(DataFileRelativePath, Environment.CurrentDirectory);
        if(!Directory.Exists(Path.GetDirectoryName(dataFilePath))) 
            Directory.CreateDirectory(Path.GetDirectoryName(dataFilePath)!);

        File.WriteAllText(dataFilePath, JsonConvert.SerializeObject(_students, Formatting.Indented));

        //using StreamWriter file = File.CreateText(dataFilePath);
        //JsonSerializer serializer = new ();

        //serializer.Serialize(file, _students);
        AnsiConsole.MarkupLine($"Дані збережено до файлу [yellow]{dataFilePath}[/]");
    }

    public void Load()
    {
        string dataFilePath = Path.GetFullPath(DataFileRelativePath, Environment.CurrentDirectory);
        if(!File.Exists(dataFilePath))
        {
            AnsiConsole.MarkupLine($"Дані не завантажено по причині відсутності файлу [yellow]{dataFilePath}[/]");
            return;
        }

        string jsonString = File.ReadAllText(dataFilePath);
        _students = JsonConvert.DeserializeObject<List<Student>>(jsonString) ?? new();

        AnsiConsole.MarkupLine($"З файлу [yellow]{dataFilePath}[/] записів завантажено: [green]{Count} шт.[/]");
    }

    public List<Student> Search_Name(string name)
    {
        throw new NotImplementedException("Не реалізовано");
    }

    public List<Student> Search_Surname(string name)
    {
        throw new NotImplementedException("Не реалізовано");
    }

    public List<Student> Search_Age(int from, int? till = null)
    {
        throw new NotImplementedException("Не реалізовано");
    }

    public List<Student> Search_Phone(string name)
    {
        throw new NotImplementedException("Не реалізовано");
    }

    public List<Student> Search_Group(int from, int? till = null)
    {
        throw new NotImplementedException("Не реалізовано");
    }

    public List<Student> Search_AvgGrade(double from, double? till = null)
    {
        throw new NotImplementedException("Не реалізовано");
    }
}
