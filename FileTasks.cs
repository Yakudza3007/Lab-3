using System;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public struct Toy
    {
        private string _name;
        private double _price;
        private int _minAge;
        private int _maxAge;
        
        public Toy(string name, double price, int minAge, int maxAge)
        {
            _name = name;
            _price = price;
            _minAge = minAge;
            _maxAge = maxAge;
        }
        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public int MinAge
        {
            get { return _minAge; }
            set { _minAge = value; }
        }

        public int MaxAge
        {
            get { return _maxAge; }
            set { _maxAge = value; }
        }
        
        public override string ToString()
        {
            return $"{_name}, {_price} руб., возраст: от {_minAge} до {_maxAge} лет";
        }
    }

    internal class FileTasks
    {
        private static void FillBinaryFile(string fileName, int elementCount)
        {
            Random random = new Random();
            int i;
            
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
            {
                for (i = 0; i < elementCount; i++)
                {
                    binaryWriter.Write(random.Next(-100, 100));
                }
            }
        }

        public static void Task4()
        {
            string fileName;
            int firstElement;
            int maxElement;
            int currentElement;

            fileName = "task4.bin";
            FillBinaryFile(fileName, 10);

            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            using (BinaryReader binaryReader = new BinaryReader(fileStream))
            {
                if (fileStream.Length == 0)
                {
                    Console.WriteLine("Файл пуст.");
                    return;
                }

                firstElement = binaryReader.ReadInt32();
                maxElement = firstElement;

                Console.Write("Элементы файла: ");
                Console.Write($"{firstElement} ");

                while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    currentElement = binaryReader.ReadInt32();
                    Console.Write($"{currentElement} ");

                    if (currentElement > maxElement)
                    {
                        maxElement = currentElement;
                    }
                }

                Console.WriteLine();
                Console.WriteLine($"Первый элемент: {firstElement}");
                Console.WriteLine($"Максимальный элемент: {maxElement}");
                Console.WriteLine($"Разность первого и максимального: {firstElement - maxElement}");
            }
        }

        private static void FillToyFile(string fileName, Checked c)
        {
            int toyCount;
            Toy[] toys;
            int i;
            string name;
            double price;
            int minAge;
            int maxAge;
            XmlSerializer serializer;

            toyCount = c.ReadIntInRange("Сколько игрушек вы хотите добавить? ", 1, 20);
            toys = new Toy[toyCount];

            for (i = 0; i < toyCount; i++)
            {
                Console.WriteLine($"\nВвод игрушки #{i + 1}:");
        
                name = c.ReadString("Название: ");
                
                while (true)
                {
                    price = c.ReadDouble("Цена: ");
                    if (price >= 0)
                    {
                        break;
                    }
                    Console.WriteLine("Ошибка: цена не может быть отрицательной.");
                }

                minAge = c.ReadIntInRange("Минимальный возраст: ", 0, 18);
                maxAge = c.ReadIntInRange("Максимальный возраст: ", minAge, 18);

                toys[i] = new Toy(name, price, minAge, maxAge);
            }

            serializer = new XmlSerializer(typeof(Toy[]));

            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(fileStream, toys);
            }
        }

        public static void Task5(Checked c)
        {
            string fileName;
            XmlSerializer serializer;
            Toy[] toys;
            double totalPrice;
            int foundCount;
            int i;
            bool isDoll;
            bool isSuitable;

            fileName = "task5.xml";
            FillToyFile(fileName, c);

            serializer = new XmlSerializer(typeof(Toy[]));

            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                toys = (Toy[])serializer.Deserialize(fileStream);
            }

            totalPrice = 0;
            foundCount = 0;

            Console.WriteLine("\nКуклы для детей шести лет:");

            for (i = 0; i < toys.Length; i++)
            {
                isDoll = toys[i].Name.IndexOf("кукл", StringComparison.OrdinalIgnoreCase) >= 0;
                isSuitable = toys[i].MinAge <= 6 && toys[i].MaxAge >= 6;

                if (isDoll && isSuitable)
                {
                    Console.WriteLine($"{toys[i].Name}: {toys[i].Price:F2} руб.");
                    totalPrice += toys[i].Price;
                    foundCount++;
                }
            }

            if (foundCount == 0)
            {
                Console.WriteLine("Подходящие куклы не найдены.");
            }
            else
            {
                Console.WriteLine($"Количество кукол: {foundCount}");
                Console.WriteLine($"Общая стоимость: {totalPrice:F2} руб.");
            }
        }
        
        private static void FillTextFile(string fileName, int elementCount)
        {
            Random random;
            int i;

            random = new Random();
            
            using (StreamWriter streamWriter = new StreamWriter(fileName, false))
            {
                for (i = 0; i < elementCount; i++)
                {
                    streamWriter.WriteLine(random.Next(-100, 100));
                }
            }
        }

        private static void PrintFile(string fileName)
        {
            string line;

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        public static void Task6(Checked c)
        {
            string sourceFileName;
            string resultFileName;
            int elementCount;
            string line;
            int number;
            int decreasedNumber;

            sourceFileName = "task6_source.txt";
            resultFileName = "task6_result.txt";

            elementCount = c.ReadIntInRange("Введите количество элементов для генерации: ", 1, 100);

            FillTextFile(sourceFileName, elementCount);

            using (StreamReader streamReader = new StreamReader(sourceFileName))
            using (StreamWriter streamWriter = new StreamWriter(resultFileName, false))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    number = int.Parse(line);
                    decreasedNumber = number - 1;
                    streamWriter.WriteLine(decreasedNumber);
                }
            }

            Console.WriteLine("\nИсходный файл:");
            PrintFile(sourceFileName);

            Console.WriteLine("\nНовый файл (каждый элемент уменьшен на 1):");
            PrintFile(resultFileName);
        }
        
        private static void FillTextFileTask7(string fileName, int lineCount)
        {
            Random random;
            int i;
            int numbersPerLine;
            int j;

            random = new Random();
            
            using (StreamWriter streamWriter = new StreamWriter(fileName, false))
            {
                for (i = 0; i < lineCount; i++)
                {
                    numbersPerLine = random.Next(2, 6);
                    
                    for (j = 0; j < numbersPerLine; j++)
                    {
                        streamWriter.Write(random.Next(-100, 100) + " ");
                    }
                    
                    streamWriter.WriteLine();
                }
            }
        }

        public static void Task7(Checked c)
        {
            string fileName;
            int lineCount;
            int firstElement;
            int maxElement;
            bool isFirstFound;
            string line;
            string[] parts;
            int i;
            int currentElement;
            int difference;

            fileName = "task7_source.txt";
            lineCount = c.ReadIntInRange("Введите количество строк для генерации: ", 1, 20);

            FillTextFileTask7(fileName, lineCount);

            Console.WriteLine("\nИсходный файл (числа по несколько в строке):");
            PrintFile(fileName);

            firstElement = 0;
            maxElement = 0;
            isFirstFound = false;

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    for (i = 0; i < parts.Length; i++)
                    {
                        currentElement = int.Parse(parts[i]);

                        if (!isFirstFound)
                        {
                            firstElement = currentElement;
                            maxElement = currentElement;
                            isFirstFound = true;
                        }
                        else if (currentElement > maxElement)
                        {
                            maxElement = currentElement;
                        }
                    }
                }
            }

            difference = firstElement - maxElement;

            Console.WriteLine($"\nПервый элемент: {firstElement}");
            Console.WriteLine($"Максимальный элемент: {maxElement}");
            Console.WriteLine($"Разность первого и максимального: {difference}");
        }
        
        private static void FillTextFileTask8(string fileName, int lineCount, Checked c)
        {
            int i;
            string line;

            using (StreamWriter streamWriter = new StreamWriter(fileName, false))
            {
                for (i = 0; i < lineCount; i++)
                {
                    line = c.ReadString($"Введите строку {i + 1}: ");
                    streamWriter.WriteLine(line);
                }
            }
        }

        public static void Task8(Checked c)
        {
            string sourceFileName;
            string resultFileName;
            int lineCount;
            string line;

            sourceFileName = "task8_source.txt";
            resultFileName = "task8_result.txt";

            lineCount = c.ReadIntInRange("Введите количество строк для ввода: ", 1, 20);

            FillTextFileTask8(sourceFileName, lineCount, c);

            using (StreamReader streamReader = new StreamReader(sourceFileName))
            using (StreamWriter streamWriter = new StreamWriter(resultFileName, false))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.Length > 0 && char.ToLowerInvariant(line[0]) == 'б')
                    {
                        streamWriter.WriteLine(line);
                    }
                }
            }

            Console.WriteLine("\nИсходный файл:");
            PrintFile(sourceFileName);

            Console.WriteLine("\nНовый файл (строки, начинающиеся на 'б'):");
            PrintFile(resultFileName);
        }
    }
}