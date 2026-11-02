using System;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Checked c = new Checked();

            while (true)
            {
                Console.WriteLine("\n0 - Выход из программы");
                Console.WriteLine("1 - Заполнение двумерных " +
                                  "массивов");
                Console.WriteLine("2 - Работа с двумерными " +
                                  "массивами");
                Console.WriteLine("3 - Работа с двумерными " +
                                  "массивами как с матрицами");
                Console.WriteLine("4 - Бинарные файлы");
                Console.WriteLine("5 - Бинарные файлы и " +
                                  "структуры");
                Console.WriteLine("6 - Текстовые файлы");
                Console.WriteLine("7 - Решение задачи с " +
                                  "помощью структуры " +
                                  "\"текстовый файл\"");
                Console.WriteLine("8 - Текстовый файл");

                int choice = c.ReadIntInRange("Выберите " +
                    "задание: ", 0, 8);

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Работа завершена.");
                        return;
                    case 1:
                        RunTask1(c);
                        break;
                    case 2:
                        RunTask2(c);
                        break;
                    case 3:
                        RunTask3(c);
                        break;
                    case 4:
                        RunTask4(c);
                        break;
                    case 5:
                        RunTask5(c);
                        break;
                    case 6:
                        RunTask6(c);
                        break;
                    case 7:
                        RunTask7(c);
                        break;
                    case 8:
                        RunTask8(c);
                        break;
                }
            }
        }

        private static void RunTask1(Checked c)
        {
            Console.WriteLine("\n=== Задание 1 ===");
            int rowCount = c.ReadIntInRange("Введите " +
                "размер n: ", 1, 100);
            int columnCount = c.ReadIntInRange("Введите " +
                "размер m: ", 1, 100);

            Matrix matrix1 = new Matrix(rowCount, 
                columnCount);
            Console.WriteLine("\nМатрица 1:");
            Console.WriteLine(matrix1.ToString());

            Matrix matrix2 = new Matrix(rowCount);
            Console.WriteLine("\nМатрица 2:");
            Console.WriteLine(matrix2.ToString());

            Matrix matrix3 = new Matrix(rowCount, true);
            Console.WriteLine("\nМатрица 3:");
            Console.WriteLine(matrix3.ToString());
        }

        private static void RunTask2(Checked c)
        {
            Console.WriteLine("\n=== Задание 2 ===");
            int rowCount = c.ReadIntInRange("Введите " +
                "количество строк: ", 1, 20);
            int columnCount = c.ReadIntInRange("Введите " +
                "количество столбцов: ", 1, 20);

            Matrix matrix = new Matrix(rowCount, columnCount, 
                true);
    
            Console.WriteLine("\nИсходная матрица:");
            Console.WriteLine(matrix.ToString());

            Console.WriteLine("\nВывод по числам Фибоначчи:");
            matrix.PrintFibonacciFormat();
        }

        private static void RunTask3(Checked c)
        {
            Console.WriteLine("\n=== Задание 3 ===");
            int m = c.ReadIntInRange("Введите " +
                                     "количество строк (m): ", 
                1, 10);
            int n = c.ReadIntInRange("Введите " +
                                     "количество столбцов (n): ", 
                1, 10);

            Console.WriteLine($"\nВвод матрицы A ({m}x{n}):");
            Matrix a = new Matrix(m, n, 
                true);

            Console.WriteLine($"\nВвод матрицы B ({m}x{n}):");
            Matrix b = new Matrix(m, n, 
                true);

            Console.WriteLine($"\nВвод матрицы C ({n}x{m}):");
            Matrix matrixC = new Matrix(n, m, 
                true);

            Matrix result = 2 * a + (b - matrixC.Transpose());

            Console.WriteLine("\nМатрица A:");
            Console.WriteLine(a.ToString());

            Console.WriteLine("\nМатрица B:");
            Console.WriteLine(b.ToString());

            Console.WriteLine("\nМатрица C^T " +
                              "(транспонированная):");
            Console.WriteLine(matrixC.Transpose().ToString());

            Console.WriteLine("\nРезультат выражения 2*A + " +
                              "(B - C^T):");
            Console.WriteLine(result.ToString());
        }
        
        private static void RunTask4(Checked c)
        {
            Console.WriteLine("\n=== Задание 4 ===");
            FileTasks.Task4();
        }
        
        private static void RunTask5(Checked c)
        {
            Console.WriteLine("\n=== Задание 5 ===");
            FileTasks.Task5(c);
        }
        
        private static void RunTask6(Checked c)
        {
            Console.WriteLine("\n=== Задание 6 ===");
            FileTasks.Task6(c);
        }
        
        private static void RunTask7(Checked c)
        {
            Console.WriteLine("\n=== Задание 7 ===");
            FileTasks.Task7(c);
        }
        
        private static void RunTask8(Checked c)
        {
            Console.WriteLine("\n=== Задание 8 ===");
            FileTasks.Task8(c);
        }
    }
}
