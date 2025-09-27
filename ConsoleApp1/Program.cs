using System;
using System.Data;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите арифметическое выражение (или 'exit' для выхода):");
        string input;
        do
        {
            input = Console.ReadLine();

            if (input == "exit")
                break;

            string expr = Regex.Replace(input, @"Pow\(([^,]+),([^)]+)\)", match =>
            {
                double a = double.Parse(match.Groups[1].Value);
                double b = double.Parse(match.Groups[2].Value);
                return Math.Pow(a, b).ToString();
            });

            expr = Regex.Replace(expr, @"Max\(([^,]+),([^)]+)\)", match =>
            {
                double a = double.Parse(match.Groups[1].Value);
                double b = double.Parse(match.Groups[2].Value);
                return Math.Max(a, b).ToString();
            });

            expr = Regex.Replace(expr, @"Min\(([^,]+),([^)]+)\)", match =>
            {
                double a = double.Parse(match.Groups[1].Value);
                double b = double.Parse(match.Groups[2].Value);
                return Math.Min(a, b).ToString();
            });

            var result = new DataTable().Compute(expr, null);

            Console.WriteLine("Результат: " + result);
            Console.WriteLine("Введите арифметическое выражение (или 'exit' для выхода):");

        } while (true);
    }
}
