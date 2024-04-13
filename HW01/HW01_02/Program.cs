// Опис завдання та примітки по його виконанню в файлі readme.md

using System.Numerics;
using System.Text;
using Spectre.Console;

namespace HW01_02;

internal class Program
{
    static void Main(string[] args)
    {
        ShowProgramName();
        Int128 number = AskDesiredNumber();
        Int128 flippedNumber = FlipNumber(number);
        AnsiConsole.Markup($"[yellow]Число відзеркалене до {number}: {flippedNumber}[/]");
    }

    private static Int128 FlipNumber(Int128 number)
    {
        Int128 flippedNumber = 0;
        do
        {
            Int128 lastDigit = number % 10;
            number /= 10;
            flippedNumber = flippedNumber * 10 + lastDigit;
        } while(number > 0);
        return flippedNumber;
    }

    private static Int128 AskDesiredNumber()
    {
        Int128 number = 0;
        do
        {
            number = AnsiConsole.Ask<Int128>("[green]Число[/] яке ви бажаєте [green]відзеркалити[/]: ");
            if(number < 0)
                AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Число повинне бути >= 0[/]");
        } while(number < 0);
        return number;
    }

    private static void ShowProgramName()
    {
        Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;
        //
        Table table = new ();
        table.Alignment(Justify.Center);
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn(new Markup("[blue] ДЗ 01.02 • Відзеркалення числа [/]")));
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("\n");
    }
}
