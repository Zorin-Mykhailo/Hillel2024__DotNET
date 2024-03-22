// Опис завдання та примітки по його виконанню в файлі readme.md

using Spectre.Console;
using System.Text;

namespace HW01_04;

internal class Program
{
    static void Main(string[] args)
    {
        ShowProgramName();
        ShowArray(new ArrayInfo(5, 5));
    }


    private static void ShowArray(ArrayInfo arrInfo)
    {
        Table table = new ();
        table.Border(TableBorder.Rounded);

        int cols = arrInfo.Array.GetLength(0);
        int rows = arrInfo.Array.GetLength(1);

        table.AddColumn("y/x");
        for(int c = 0; c < cols; c++)
            table.AddColumn($"[green]{c,4}[/]");
        for(int r = 0; r < rows; r++)
            table.AddRow($"[green]{r}[/]");

        Random rnd = new ();

        for(int c = 1; c < cols + 1; c++)
        {
            for(int r = 0; r < rows; r++)
            {
                int value = arrInfo.Array[r, c - 1];
                string color =
                    value == arrInfo.Min.Value ? "blue" :
                    value == arrInfo.Max.Value ? "red" :
                    "white";

                table.UpdateCell(r, c, $"[{color}]{value,4}[/]");
            }
        }
        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine($"[blue]Min:[/] {arrInfo.Min.Value}");
        AnsiConsole.MarkupLine($"[red]Max:[/] {arrInfo.Max.Value}");
        AnsiConsole.MarkupLine($"[yellow]Sum:[/] {arrInfo.CalculateMinMaxSum()}");
    }

    private static void ShowProgramName()
    {
        Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;
        //
        Table table = new ();
        table.Alignment(Justify.Center);
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn(new Markup("[blue] ДЗ 01.04 • Сума значень між min та max елементами 5x5 масива [/]")));
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("\n");
    }
}

public class ArrayInfo
{
    public int[,] Array { get; private set; }

    public ArrayCell Min { get; private set; }

    public ArrayCell Max { get; private set; }

    public ArrayInfo(int rows, int columns)
    {
        Array = new int[rows, columns];
        Random rnd = new ();

        Min = null!;
        Max = null!;

        for(int c = 0; c < columns; c++)
        {
            for(int r = 0; r < rows; r++)
            {
                ArrayCell cell = new(rnd.Next(-100, 101), r, c);
                Array[r, c] = cell.Value;

                Min = Min == null || Min > cell ? cell : Min;
                Max = Max == null || Max < cell ? cell : Max;
            }
        }
    }

    public int CalculateMinMaxSum()
    {
        int r1 = Min.Row < Max.Row ? Min.Row : Max.Row;
        int c1 = Min.Column < Max.Column ? Min.Column : Max.Column;
        int r2 = Max.Row > Min.Row ? Max.Row : Min.Row;
        int c2 = Max.Column > Min.Column ? Max.Column : Min.Column;
        int sum = 0;

        for(int c = c1; c <= c2; c++)
            for(int r = r1; r <= r2; r++)
                sum += Array[r, c];
        
        return sum;
    }
}

public class ArrayCell
{
    public int Value { get; private set; }

    public int Row { get; private set; }

    public int Column { get; private set; }

    public ArrayCell(int value, int row, int column)
    {
        Value = value;
        Row = row;
        Column = column;
    }

    public static bool operator > (ArrayCell a, ArrayCell b)
    {
        return a.Value > b.Value;
    }

    public static bool operator <(ArrayCell a, ArrayCell b)
    {
        return a.Value < b.Value;
    }
}
