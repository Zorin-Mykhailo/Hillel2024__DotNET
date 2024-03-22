// Опис завдання та примітки по його виконанню в файлі readme.md

using Spectre.Console;

namespace HW01_05;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            MainClass mainClass = new ();
            mainClass.ShowProgramName();
            mainClass.TreatingUserCommands();
        }
        catch(Exception ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }
}