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
}
