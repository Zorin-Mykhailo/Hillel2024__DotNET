namespace ConsoleHelpers;

public static class ConsoleExt
{
    public static void Print(this string? message, ConsoleColor? foreground = null, ConsoleColor? background = null)
    {
        ConsolePrint(message, (msg) => Console.Write(msg), foreground, background);
    }

    public static void PrintLn(this string? message, ConsoleColor? foreground = null, ConsoleColor? background = null)
    {
        ConsolePrint(message, (msg) => Console.WriteLine(msg), foreground, background);
    }

    private static void ConsolePrint(string? message, Action<string?> printFunc, ConsoleColor? foreground = null, ConsoleColor? background = null)
    {
        ConsoleColor prevForeground = Console.ForegroundColor;
        Console.ForegroundColor = foreground ?? prevForeground;
        ConsoleColor prevBackground = Console.BackgroundColor;
        Console.BackgroundColor = background ?? prevBackground;
        printFunc(message);
        Console.ForegroundColor = prevForeground;
        Console.BackgroundColor = prevBackground;
    }

    private static void ShowInputError(this string? errorMessage)
    {
        " ПОМИЛКА ВВОДУ ".Print(ConsoleColor.Black, ConsoleColor.Red);
        $" {errorMessage}".PrintLn(ConsoleColor.Red);
    }

    private static void ShowInputErrors(this List<string> errorMessages)
    {
        " ПОМИЛКИ ВВОДУ: ".PrintLn(ConsoleColor.Black, ConsoleColor.Red);
        errorMessages.ForEach(errorMessage => $" ❌ {errorMessage}".PrintLn(ConsoleColor.Red));
    }

    public static int? TakeInt32(this string inputPrompt, params Func<int?, string>[] validators)
    {
        int? value = null;
        List<string> validationErrors;
        do
        {
            validationErrors = new();
            inputPrompt.Print(ConsoleColor.Cyan);
            "> ".Print(ConsoleColor.White);
            string? userInput = Console.ReadLine() ?? string.Empty;
            int parsedValue;
            
            if(int.TryParse(userInput, out parsedValue))
                value = parsedValue;
            else if(!string.IsNullOrEmpty(userInput))
                validationErrors.Add("Значення не є цілим числом");                
            
            if(validationErrors.Count == 0) 
                validationErrors = GetValidationErrors(value, validators);
            
            if(validationErrors.Count > 0)
                validationErrors.ShowInputErrors();
        }
        while(validationErrors.Count > 0);
        return value;
    }

    private static List<string> GetValidationErrors<T>(T value, params Func<T, string>[] validators)
    {
        List<string> validationErrors = new ();
        if(validators == null || validators.Length == 0) return validationErrors;

        foreach(Func<T, string> validator in validators)
        {
            string validationError = validator(value);
            if(!string.IsNullOrWhiteSpace(validationError))
                validationErrors.Add(validationError);
        }
        return validationErrors;
    }
}

public static class CheckInt32
{
    public static string NotNull(this int? value)
        => value == null ? "Значення не повинно бути пустим" : string.Empty;

    public static string Greather(this int? value, int minValue)
        => minValue < value ? string.Empty : $"Значення повинно бути більше ніш {minValue}";

    public static string GreatherOrEq(this int? value, int minValue)
        => minValue <= value ? string.Empty : $"Значення повинно бути більшим або рівним {minValue}";

    public static string Less(this int? value, int maxValue)
        => maxValue > value ? string.Empty : $"Значення повинно бути менше ніш {maxValue}";

    public static string LessOrEq(this int? value, int maxValue)
        => maxValue >= value ? string.Empty : $"Значення повинно бути меншим або рівним {maxValue}";
}