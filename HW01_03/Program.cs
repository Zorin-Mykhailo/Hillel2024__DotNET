using Spectre.Console;
using System.Text;

namespace HW01_03;

internal class Program
{
    static void Main(string[] args)
    {
        ShowProgramName();
        Int128 number = AskDesiredNumber();
        int shift = AskShift(number);
        Int128 shiftedNumber = ShiftNumber(number, shift);
        AnsiConsole.Markup($"[yellow] {number} {(shift < 0 ? "<<" : ">>")} {shift} = {shiftedNumber}[/]");
    }

    private static Int128 ShiftNumber(Int128 number, int shift)
    {

        return number;
    }

    private static Int128 AskDesiredNumber()
    {
        Int128 number = 0;
        do
        {
            number = AnsiConsole.Ask<Int128>("[green]Число[/] яке ви бажаєте [green]зсунути[/]: ");
            if(number < 0)
                AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Число повинне бути >= 0[/]");
        } while(number < 0);
        return number;
    }

    private static int AskShift(Int128 numb)
    {
        int number = 0;
        do
        {
            number = AnsiConsole.Ask<int>("На яку кількість [green]розрядів зсунути[/]: ");
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
        table.AddColumn(new TableColumn(new Markup("[blue] ДЗ 01.03 • Зміщення числа [/]")));
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("\n");
    }
}


public static class Int32__Ext
{
    /// <summary> Получить подчисло из начального числа </summary>
    /// <param name="digits"> Начальное число </param>
    /// <param name="skipRight"> Сколько цифр пропустить справа. Если 0 - не пропускать ничего </param>
    /// <param name="takeRight"> Сколько цифр вернуть справа. Eсли null - вернет все оставшееся число </param>
    /// <returns> Подчисло из начального числа </returns>
    /// <remarks>
    /// Например если нужно получить подчисло 654 из начального числа 9876543210, нужно вызвать
    /// 9876543210.GetSubNumber(4, 3)
    /// </remarks>
    public static Int32 SubNumb(this Int32 digits, Int32 skipRight = 0, Int32? takeRight = null)
    {
        Int32 temp = digits / (Int32)Math.Pow(10, Math.Abs(skipRight));
        return takeRight == null ? temp : temp % (Int32)Math.Pow(10, Math.Abs((Int32)takeRight));
    }



    /// <summary> Дополнить указанным количеством нулей </summary>
    /// <param name="value"> Значение </param>
    /// <param name="amount"> Количесво нулей </param>
    /// <returns> Число дополненное нулями </returns>
    public static Int32 AppendZero(this Int32 value, Int32 amount)
    {
        Int32 zeros = amount < 0 ? 0 : amount;
        return value * (Int32)Math.Pow(10, zeros);
    }
}
