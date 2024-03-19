// Опис завдання та примітки по його виконанню в файлі readme.md

using System.Text;
using Spectre.Console;


namespace HW01_01;

internal class Program
{
    static void Main(string[] args)
    {        
        ShowProgramName();
        try
        {
            int digit = AskDesiredDigitFactorial();
            Int128 digitFactorial = CalculateFactorialOf(digit);
            AnsiConsole.Markup($"[yellow]{digit}! = {digitFactorial}[/]");
        }
        catch(Exception ex)
        {
            AnsiConsole.WriteException(ex);
        }        
    }

    private static Int128 CalculateFactorialOf(int digit)
    {
        if(digit < 0) throw new ArgumentException("Не може бути від'ємним числом.");
        if(digit == 0) return 0;        

        Int128 fact = 1;
        for(int i = digit; i > 0; i--)  checked { fact *= i; }
        return fact;
    }

    private static int AskDesiredDigitFactorial()
    {
        int digit = 0;
        do
        {
            digit = AnsiConsole.Ask<int>("[green]Факторіал[/] якого [green]числа[/] ви бажаєте отримати:");
            if(digit < 0)
                AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Число повинне бути > 0[/]");
        } while(digit < 0);
        return digit;
    }

    private static void ShowProgramName()
    {
        Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;
        //
        Table table = new ();
        table.Alignment(Justify.Center);
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn(new Markup("[blue] ДЗ 01.01 • Факторіал числа [/]")));
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("\n");
    }
}
