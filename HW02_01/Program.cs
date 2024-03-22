// Опис завдання та примітки по його виконанню в файлі readme.md

using Spectre.Console;
using System.Text;

namespace HW02_01;

internal class Program
{
    const int MaxCustomers = 10;
    const int NumChairs = 3;

    static void Main(string[] args)
    {
        Aux.ShowProgramName();

        Random rnd = new ();        
        Semaphore waitingRoom = new (NumChairs, NumChairs);
        Semaphore barberChair = new (1, 1);
        Semaphore barberSleepChair = new (0, 1);
        Semaphore seatBelt = new (0, 1);
        bool done = false;
        void Barber()
        {
            while(!done)
            {
                Print("[black on red]Перукар[/] спить ...");
                barberSleepChair.WaitOne();
                if(!done)
                {
                    Print("[black on red]Перукар[/] стриже ...");
                    Thread.Sleep(rnd.Next(1, 3) * 1000);
                    Print("[black on red]Перукар[/] постриг.");
                    seatBelt.Release();
                }
                else Print("[black on red]Перукар[/] спить ...");
            }
            return;
        }
        void Customer(object number)
        {
            int numb = (int)number;
            
            Print($"[{numb.Color()}]Клієнт {numb}[/] йде в перукарню ...");
            Thread.Sleep(rnd.Next(1, 5) * 1000);
            Print($"[{numb.Color()}]Клієнт {numb}[/] прийшов");
            
            waitingRoom.WaitOne();
            Print($"[{numb.Color()}]Клієнт {numb}[/] заходить в кімнату очікування ...");
            barberChair.WaitOne();
            waitingRoom.Release();

            Print($"[{numb.Color()}]Клієнт {numb}[/] будить перукаря ...");            
            barberSleepChair.Release();

            seatBelt.WaitOne();
            barberChair.Release();
            Print($"[{numb.Color()}]Клієнт {numb}[/] залишає перукарню.");
        }
        Thread barberThread = new (Barber);
        barberThread.Start();
        Thread[] customersThreads = new Thread[MaxCustomers];
        for(int i = 0; i < MaxCustomers; i++)
        {
            customersThreads[i] = new Thread(new ParameterizedThreadStart(Customer));
            customersThreads[i].Start(i);
        }
        for(int i = 0; i < MaxCustomers; i++)
        {
            customersThreads[i].Join();
        }
        done = true;
        barberSleepChair.Release();
        
        barberThread.Join();        
        Print("[underline]Роботу завершено[/]");
    }

    private static void Print(string message)
    {
        AnsiConsole.MarkupLine(message);
    }
}

public static class Aux
{
    private static Dictionary<int, string> _colors;

    static Aux()
    {
        _colors = new()
        {
            { 0, "maroon" },
            { 1, "darkorange3" },
            { 2, "olive" },
            { 3, "yellow" },
            { 4, "lime" },
            { 5, "springgreen4" },
            { 6, "aqua" },
            { 7, "teal" },
            { 8, "dodgerblue2" },
            { 9, "purple" },
            { 10, "fuchsia" }
        };
    }

    public static string Color(this int index)
        => _colors[index];

    public static void ShowProgramName()
    {
        Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;
        //
        Table table = new ();
        table.Alignment(Justify.Center);
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn(new Markup("[blue] ДЗ 02.01 • Сплячий перукар [/]")));
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("\n");
    }
}