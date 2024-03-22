// Опис завдання та примітки по його виконанню в файлі readme.md

using Spectre.Console;
using System.Text;

namespace HW01_05;

internal class Program
{
    static void Main(string[] args)
    {
        MainClass mainClass = new ();

        mainClass.ShowProgramName();
        mainClass.ShowMenu();
    }
}
