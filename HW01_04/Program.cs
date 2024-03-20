using Spectre.Console;
using System.Text;

namespace HW01_04;

internal class Program
{
    static void Main(string[] args)
    {
        ShowProgramName();
        ShowArray();
    }


    private static void ShowArray()
    {
        // Create a table
        Table table = new ();
        table.Border(TableBorder.Rounded);

        // Add some columns
        table.AddColumn("");
        for(int c = 0; c < 5; c++)
            table.AddColumn($"[green]{c}[/]");
        for(int r = 0; r < 5; r++)
            table.AddRow($"[green]{r}[/]");

        Random rnd = new Random();

        for(int c = 0; c < 5; c++)
        {
            for(int r = 0; r < 5; r++)
            {
                table.UpdateCell(r, c, rnd.Next(-100, 100));
            }
        }
        AnsiConsole.Write(table);
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
