using Spectre.Console;

namespace HW01_05;

public static class Ask
{
    public static class Student
    {
        public static string Name()
        {
            string? name = null;
            do
            {
                name = AnsiConsole.Ask<string>("[green]Ім'я[/] студента:");
                if(!IsValid(name.Trim()))
                    AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Значення не повинно бути пустим та повино мати більше однієї літери[/]");
            } while(!IsValid(name.Trim()));
            return name!;
            bool IsValid(string? name) => !string.IsNullOrWhiteSpace(name) && name.Length > 1;
        }

        public static string Surname()
        {
            string? name = null;
            do
            {
                name = AnsiConsole.Ask<string>("[green]Прізвище[/] студента:");
                if(!IsValid(name.Trim()))
                    AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Значення не повинно бути пустим та повино мати більше однієї літери[/]");
            } while(!IsValid(name.Trim()));
            return name!;
            bool IsValid(string? name) => !string.IsNullOrWhiteSpace(name) && name.Length > 1;
        }

        public static int Age()
        {
            int age = 0;
            do
            {
                age = AnsiConsole.Ask<int>("[green]Вік[/] студента:");
                if(!IsValid(age))
                    AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Вік студента повинен бути >= 15 та <= 80 років[/]");
            } while(!IsValid(age));
            return age;
            bool IsValid(int age) => age > 15 && age < 80;
        }

        public static string Phone()
        {
            string? phone = null;
            do
            {
                phone = AnsiConsole.Ask<string>("[green]Телефон[/] студента:");
                if(!IsValid(phone.Trim()))
                    AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Значення повинно містити символи лише з наботу \"9876543210 -()\" та повинно містити рівно 10 цифр[/]");
            } while(!IsValid(phone.Trim()));
            return phone!;

            bool IsValid(string? phone)
            {
                HashSet<char> allowedSymbols = new ("9876543210 -()");
                bool isPreValid = !string.IsNullOrWhiteSpace(phone)
                && phone.All(symb => allowedSymbols.Contains(symb));
                if(!isPreValid)
                    return false;
                int digitsCount = 0;
                foreach(char symb in phone)
                    if(char.IsDigit(symb)) ++digitsCount;
                return digitsCount == 10;
            }
        }

        public static int GroupNumber()
        {
            int groupNumber = 0;
            do
            {
                groupNumber = AnsiConsole.Ask<int>("[green]№ групи[/] студента:");
                if(!IsValid(groupNumber))
                    AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Значення повинно бути > 0[/]");
            } while(!IsValid(groupNumber));
            return groupNumber;
            bool IsValid(int groupNumber) => groupNumber > 0;
        }

        public static double AvgGrade()
        {
            double avgGrade = 0;
            do
            {
                avgGrade = AnsiConsole.Ask<double>("[green]Середній бал[/] студента:");
                if(!IsValid(avgGrade))
                    AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Значення повинно бути >= 0 та <= 100[/]");
            } while(!IsValid(avgGrade));
            return avgGrade;
            bool IsValid(double avgGrade) => avgGrade >= 0.00 && avgGrade <= 100.00;
        }
    }

    public static class Record
    {
        public static int OrderNumber(int maxOrderNumber)
        {
            int orderNumber = 0;
            do
            {
                orderNumber = AnsiConsole.Ask<int>("[green]Порядковий номер[/] запису:");
                if(!IsValid(orderNumber, maxOrderNumber))
                    AnsiConsole.MarkupLine($"[black on red] Input error: [/] [red]Порядковий номер запису повинен бути >= 0 та <= {maxOrderNumber}[/]");
            } while(!IsValid(orderNumber, maxOrderNumber));
            return orderNumber;
            bool IsValid(int orderNumber, int maxOrderNumber) => orderNumber >= 0 && orderNumber <= maxOrderNumber;
        }
    }

    public static class SearchFilter
    {
        public static string Name()
        {
            string? name = null;
            do
            {
                name = AnsiConsole.Ask<string>("[green]Ім'я[/] студента:");
                if(!IsValid(name.Trim()))
                    AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Значення не повинно бути пустим та повино мати більше однієї літери[/]");
            } while(!IsValid(name.Trim()));
            return name!;
            bool IsValid(string? name) => !string.IsNullOrWhiteSpace(name) && name.Length > 1;
        }

        public static string Surname()
        {
            string? name = null;
            do
            {
                name = AnsiConsole.Ask<string>("[green]Прізвище[/] студента:");
                if(!IsValid(name.Trim()))
                    AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Значення не повинно бути пустим та повино мати більше однієї літери[/]");
            } while(!IsValid(name.Trim()));
            return name!;
            bool IsValid(string? name) => !string.IsNullOrWhiteSpace(name) && name.Length > 1;
        }

        public static int MinAge()
        {
            int minAge = 0;
            do
            {
                minAge = AnsiConsole.Ask<int>("[green]Мінімальний вік[/] студента:");
                if(!IsValid(minAge))
                    AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Мінімальний вік студента повинен бути >= 15[/]");
            } while(!IsValid(minAge));
            return minAge;
            bool IsValid(int age) => age >= 15;
        }

        public static int MaxAge(int minAge)
        {
            int maxAge = minAge;
            do
            {
                maxAge = AnsiConsole.Ask<int>("[green]Максимальний вік[/] студента:", minAge);
                if(!IsValid(maxAge, minAge))
                    AnsiConsole.MarkupLine($"[black on red] Input error: [/] [red]Максимальний вік студента повинен бути >= {minAge} та <= 80 років[/]");
            } while(!IsValid(maxAge, minAge));
            return maxAge;
            bool IsValid(int age, int minAge) => age >= minAge && age <= 80;
        }

        public static string Phone()
        {
            string? phone = null;
            do
            {
                phone = AnsiConsole.Ask<string>("[green]Телефон[/] студента:");
                if(!IsValid(phone.Trim()))
                    AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Значення повинно містити символи лише з наботу \"9876543210 -()\" та повинно містити рівно 10 цифр[/]");
            } while(!IsValid(phone.Trim()));
            return phone!;

            bool IsValid(string? phone)
            {
                HashSet<char> allowedSymbols = new ("9876543210 -()");
                bool isPreValid = !string.IsNullOrWhiteSpace(phone)
                && phone.All(symb => allowedSymbols.Contains(symb));
                if(!isPreValid)
                    return false;
                int digitsCount = 0;
                foreach(char symb in phone)
                    if(char.IsDigit(symb)) ++digitsCount;
                return digitsCount == 10;
            }
        }

        public static int MinGroupNumber()
        {
            int minAge = 0;
            do
            {
                minAge = AnsiConsole.Ask<int>("[green]Мінімальний № групи[/] студента:");
                if(!IsValid(minAge))
                    AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Мінімальний № групи студента повинен бути >= 1[/]");
            } while(!IsValid(minAge));
            return minAge;
            bool IsValid(int age) => age >= 1;
        }

        public static int MaxGroupNumber(int minGroupNumber)
        {
            int maxAge = minGroupNumber;
            do
            {
                maxAge = AnsiConsole.Ask<int>("[green]Максимальний № групи[/] студента:", minGroupNumber);
                if(!IsValid(maxAge, minGroupNumber))
                    AnsiConsole.MarkupLine($"[black on red] Input error: [/] [red]Максимальний вік студента повинен бути >= {minGroupNumber}[/]");
            } while(!IsValid(maxAge, minGroupNumber));
            return maxAge;
            bool IsValid(int maxGroupNumber, int minGroupNumber) => maxGroupNumber >= minGroupNumber;
        }

        public static double MinAvgGrade()
        {
            double minAvgGrade = 0;
            do
            {
                minAvgGrade = AnsiConsole.Ask<int>("[green]Мінімальний середній бал[/] студента:");
                if(!IsValid(minAvgGrade))
                    AnsiConsole.MarkupLine("[black on red] Input error: [/] [red]Мінімальний середній бал студента повинен бути >= 0.00[/]");
            } while(!IsValid(minAvgGrade));
            return minAvgGrade;
            bool IsValid(double minAvgGrade) => minAvgGrade >= 0.00;
        }

        public static double MaxAvgGrade(double minAvgGrade)
        {
            double maxAvgGrade = minAvgGrade;
            do
            {
                maxAvgGrade = AnsiConsole.Ask<double>("[green]Максимальний середній бал[/] студента:", minAvgGrade);
                if(!IsValid(maxAvgGrade, minAvgGrade))
                    AnsiConsole.MarkupLine($"[black on red] Input error: [/] [red]Максимальний середній бал студента повинен бути >= {minAvgGrade}[/]");
            } while(!IsValid(maxAvgGrade, minAvgGrade));
            return maxAvgGrade;
            bool IsValid(double maxAvgGrade, double minAvgGrade) => maxAvgGrade >= minAvgGrade;
        }
    }
}
