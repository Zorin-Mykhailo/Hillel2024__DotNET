using Newtonsoft.Json;
using Spectre.Console;
using System.IO.Enumeration;
using System.Numerics;
using System.Xml.Linq;

namespace HW01_05;
public class AcademyGroup
{
    private List<Student> Students { get; set; }
    
    public int Count { get => Students.Count; }

    public AcademyGroup()
    {
        Students = new ();
    }

    public void Add(Student student)
    {
        Students.Add(student);
    }

    public bool Remove(string surname)
    {
        Student? student = Students.FirstOrDefault(s => s.Surname == surname);
        if(student == null) return false;
        Students.Remove(student);
        return true;
    }

    public Student? Edit(string surname)
    {
        return Students.FirstOrDefault(s => s.Surname == surname);
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

        Students.ForEach(s => table.AddRow(s.Surname, s.Name, $"{s.Age,4}", s.Phone, $"{s.NumberOfGroup}", $"{s.Average,6:0.00}"));
        
        AnsiConsole.Write(table);
    }

    private const string DataFileRelativePath = @"..\..\..\AppData\Data.json";

    public void Save()
    {
        string dataFilePath = Path.GetFullPath(DataFileRelativePath, Environment.CurrentDirectory);
        if(!Directory.Exists(Path.GetDirectoryName(dataFilePath))) 
            Directory.CreateDirectory(Path.GetDirectoryName(dataFilePath)!);

        File.WriteAllText(dataFilePath, JsonConvert.SerializeObject(Students, Formatting.Indented));

        //using StreamWriter file = File.CreateText(dataFilePath);
        //JsonSerializer serializer = new ();

        //serializer.Serialize(file, Students);
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
        Students = JsonConvert.DeserializeObject<List<Student>>(jsonString) ?? new();

        AnsiConsole.MarkupLine($"З файлу [yellow]{dataFilePath}[/] записів завантажено: [green]{Count} шт.[/]");
    }

    public List<Student> SearchByName(string name)
        => SearchByName(Students, name);

    public List<Student> SearchByName(List<Student> source, string name)
    {
        List<Student> searchResult = new ();
        if(source == null || source.Count == 0) return searchResult;
        searchResult = source.Where(s => s.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToList();
        return searchResult;
    }

    public List<Student> SearchBySurname(string surname)
        => SearchBySurname(Students, surname);

    public List<Student> SearchBySurname(List<Student> source, string surname)
    {
        List<Student> searchResult = new ();
        if(source == null || source.Count == 0) return searchResult;
        searchResult = source.Where(s => s.Surname.Contains(surname, StringComparison.InvariantCultureIgnoreCase)).ToList();
        return searchResult;
    }

    public List<Student> SearchByAge(int from, int? till = null)
        => SearchByAge(Students, from, till);

    public List<Student> SearchByAge(List<Student> source, int from, int? till = null)
    {
        int to = till ?? from;
        if(from > to) (from, to) = (to, from);
        List<Student> searchResult = new ();
        if(source == null || source.Count == 0) return searchResult;
        searchResult = source.Where(s => s.Age >= from || s.Age <= to).ToList();
        return searchResult;
    }

    public List<Student> SearchByPhone(string phone)
        => SearchByPhone(Students, phone);

    public List<Student> SearchByPhone(List<Student> source, string phone)
    {
        List<Student> searchResult = new ();
        if(source == null || source.Count == 0) return searchResult;
        searchResult = source.Where(s => NormalizePhone(s.Phone) == NormalizePhone(phone)).ToList();
        return searchResult;
        string NormalizePhone(string phone) => new (phone.Where(c => char.IsDigit(c)).ToArray());
    }

    public List<Student> SearchByGroup(int from, int? till = null)
        => SearchByGroup(Students, from, till);

    public List<Student> SearchByGroup(List<Student> source, int from, int? till = null)
    {
        int to = till ?? from;
        if(from > to) (from, to) = (to, from);
        List<Student> searchResult = new ();
        if(source == null || source.Count == 0) return searchResult;
        searchResult = source.Where(s => s.NumberOfGroup >= from || s.NumberOfGroup <= to).ToList();
        return searchResult;
    }

    public List<Student> SearchByAvgGrade(double from, double? till = null)
        => SearchByAvgGrade(Students, from, till);

    public List<Student> SearchByAvgGrade(List<Student> source, double from, double? till = null)
    {
        double to = till ?? from;
        if(from > to) (from, to) = (to, from);
        List<Student> searchResult = new ();
        if(source == null || source.Count == 0) return searchResult;
        searchResult = source.Where(s => s.Average >= from || s.Average <= to).ToList();
        return searchResult;
    }
}
