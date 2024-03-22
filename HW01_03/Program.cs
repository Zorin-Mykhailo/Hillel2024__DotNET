// Опис завдання та примітки по його виконанню в файлі readme.md

using Spectre.Console;
using System.Text;

namespace HW01_03;

internal class Program
{
    static void Main(string[] args)
    {
        ShowProgramName();

        int number = AskDesiredNumber();
        int shift = AskShift(number);
        int shiftedNumber = number.Shift(shift);
        AnsiConsole.MarkupLine($"[yellow] {number} {(shift < 0 ? "<<" : ">>")} {Math.Abs(shift)} = {shiftedNumber}[/]");
    }

    private static int AskDesiredNumber()
    {
        int number = 0;
        do
        {
            number = AnsiConsole.Ask<int>("[green]Число[/] яке ви бажаєте [green]зсунути[/]: ");
            if(number < 0)
                AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Число повинне бути >= 0[/]");
        } while(number < 0);
        return number;
    }

    private static int AskShift(int numb)
    {
        return AnsiConsole.Ask<int>("На яку кількість [green]розрядів зсунути[/] ([black on white] + [/] - вправо, [black on white] - [/] - вліво): ");
    }


    private static void ShowProgramName()
    {
        Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;
        //
        Table table = new ();
        table.Alignment(Justify.Center);
        table.Border(TableBorder.Rounded);
        table.AddColumn(new TableColumn(new Markup("[blue] ДЗ 01.03 • Циклічний зсув числа [/]")));
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("\n");
    }
}


public static class Int128__Ext
{
    public static int Shift(this int number, int shift)
    {
        int digitsCount = number.DigitsCount();
        int normalizedShift = shift % digitsCount;

        int leftPart;
        int rightPart;
        
        if(normalizedShift >= 0) // Зсунути вправо
        {
            leftPart = number.SubNumb(0, normalizedShift);
            rightPart = number.SubNumb(normalizedShift);
        }
        else // Зсунути вліво
        {
            int leftShift = digitsCount - Math.Abs(normalizedShift);
            leftPart = number.SubNumb(0, leftShift);
            rightPart = number.SubNumb(leftShift);
        }
        return leftPart.AppendZero(digitsCount) + rightPart;
    }

    public static int DigitsCount(this int number)
    {
        number = Math.Abs(number);
        return (int)Math.Floor(Math.Log10(number) + 1);
    }

    /// <summary> Отримати підчисло із початкового </summary>
    /// <param name="number"> Початкове число </param>
    /// <param name="skipRight"> Скільки цифр пропустити (зправа на ліво). Якщо 0 - не пропускати нічого </param>
    /// <param name="takeRight"> Скфльки цифр повернути (після пропущених, зправа на ліво). Якщо null - повернути решту цифр початкового числа </param>
    /// <returns> Підчисло із початкового числа </returns>
    /// <remarks> Наприклад, якщо необхідно отримати підчисло 654 із початкового 987_654_3210 необхідно викликати
    /// 987_654_3210.GetSubNumber(4, 3) </remarks>
    public static int SubNumb(this int number, int skipRight = 0, int? takeRight = null)
    {
        int temp = number / (int)Math.Pow(10, Math.Abs(skipRight));
        return takeRight == null ? temp : temp % (int)Math.Pow(10, Math.Abs((int)takeRight));
    }



    /// <summary> Доповнити початкове число вказаною кількістю нулів до бажаної кількості цифр </summary>
    /// <param name="number"> Початкове число </param>
    /// <param name="desiredDigitsCount"> Бажана кількість цифр у числі </param>
    /// <returns> Число доповнене нулями </returns>
    /// <remarks> Наприклад, якщо необхідно число 654 доповнити нулями до шести цифр (654_000) необхідно викликати
    /// 654.AppendZero(6) </remarks>
    public static int AppendZero(this int number, int desiredDigitsCount)
    {
        int digitsCount = number.DigitsCount();
        if(digitsCount >= desiredDigitsCount)
            return number;

        int amountOfZeros = desiredDigitsCount - digitsCount;
        return number * (int)Math.Pow(10, amountOfZeros);
    }
}
