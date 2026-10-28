using System;

namespace ConsoleApp1;

internal class Checked
{
    public int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int result)) return 
                result;
            Console.WriteLine("Ошибка: введите целое число.");
        }
    }

    public int ReadIntInRange(string prompt, int minValue, int 
        maxValue)
    {
        while (true)
        {
            int result = ReadInt(prompt); 
            if (result >= minValue && result <= maxValue)
            {
                return result;
            }
            Console.WriteLine($"Ошибка: введите число от {
                minValue} до {maxValue}.");
        }
    }

    public double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (double.TryParse(input, out double result)) return 
                result;
            Console.WriteLine("Ошибка: введите число (можно " +
                              "с запятой или точкой).");
        }
    }

    public string ReadString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) return input;
            Console.WriteLine("Ошибка: строка не может быть " +
                              "пустой.");
        }
    }
}
