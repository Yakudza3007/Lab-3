using System;

namespace ConsoleApp1
{
    internal class Matrix
    {
        private int[,] _array;

        public Matrix(int n, int m)
        {
            Checked c = new Checked();
            _array = new int[n, m];
            Console.WriteLine($"\nЗаполнение массива {n}x{m} " +
                              $"по диагоналям:");
    
            for (int diagonalOffset = m - 1; diagonalOffset >= 
                 -(n - 1); diagonalOffset--)
            {
                int startRow = Math.Max(0, -diagonalOffset);
                int endRow = Math.Min(n - 1, m - 1 - 
                    diagonalOffset);

                for (int row = startRow; row <= endRow; row++)
                {
                    int column = row + diagonalOffset;
                    _array[row, column] = c.ReadInt(
                        $"Введите " +
                        $"элемент [{row},{column}]: ");
                }
            }
        }

        public Matrix(int n)
        {
            _array = new int[n, n];
            Random random = new Random();
            Console.WriteLine($"\nЗаполнение массива {n}x{n} " +
                              $"случайными числами:");

            for (int column = 0; column < n; column++)
            {
                int[] temp = new int[n];
        
                for (int i = 0; i < n; i++)
                {
                    temp[i] = random.Next(-100, 100);
                }
        
                Array.Sort(temp);
                Array.Reverse(temp);

                for (int row = 0; row < n; row++)
                {
                    _array[row, column] = temp[row];
                }
            }
        }

        public Matrix(int n, bool isSpiral)
        {
            _array = new int[n, n];
            Console.WriteLine($"\nЗаполнение массива {n}x{n} " +
                              $"спиралью:");

            int currentRow = (n - 1) / 2;
            int currentColumn = (n - 1) / 2;
            int currentValue = 1;
            int stepLength = 1;

            int[] rowOffsets = { 0, 1, 0, -1 };
            int[] columnOffsets = { -1, 0, 1, 0 };
            int direction = 0;

            _array[currentRow, currentColumn] = currentValue++;

            while (currentValue <= n * n)
            {
                for (int turnCounter = 0; turnCounter < 2; 
                     turnCounter++)
                {
                    for (int stepCounter = 0; stepCounter < 
                         stepLength; stepCounter++)
                    {
                        currentRow += rowOffsets[direction];
                        currentColumn += columnOffsets[direction];

                        if (currentRow >= 0 && currentRow < n && 
                            currentColumn >= 0 && currentColumn < 
                            n && _array[
                                currentRow, currentColumn] == 0)
                        {
                            _array[currentRow, currentColumn] =
                                currentValue++;
                        }
                    }
                    direction = (direction + 1) % 4;
                }
                stepLength++;
            }
        }
        
        public Matrix(int rowCount, int columnCount, bool 
            isStandardInput)
        {
            _array = new int[rowCount, columnCount];
            
            if (isStandardInput)
            {
                Checked c = new Checked();
                Console.WriteLine($"\nЗаполнение массива {
                    rowCount}x{columnCount} в обычном порядке:");

                for (int row = 0; row < rowCount; row++)
                {
                    for (int column = 0; column < columnCount; 
                         column++)
                    {
                        _array[row, column] = c.ReadInt(
                            $"Введите элемент " +
                            $"[{row},{column}]: ");
                    }
                }
            }
        }

        public int[,] Data
        {
            get { return _array; }
        }
        
        public Matrix Transpose()
        {
            int rowCount = _array.GetLength(0);
            int columnCount = _array.GetLength(1);
            Matrix result = new Matrix(columnCount, rowCount, 
                false);

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; 
                     column++)
                {
                    result.Data[column, row] = _array[row, 
                        column];
                }
            }

            return result;
        }

        public static Matrix operator +(Matrix left, Matrix 
            right)
        {
            if (left._array.GetLength(0) != 
                right._array.GetLength(0) || 
                left._array.GetLength(1) != 
                right._array.GetLength(1))
            {
                throw new ArgumentException("Размеры матриц " +
                    "не совпадают.");
            }

            int rowCount = left._array.GetLength(0);
            int columnCount = left._array.GetLength(1);
            Matrix result = new Matrix(rowCount, columnCount, 
                false);

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; 
                     column++)
                {
                    result.Data[row, column] = left._array[
                        row, column] + right._array[row, column];
                }
            }

            return result;
        }

        public static Matrix operator -(Matrix left, Matrix 
            right)
        {
            if (left._array.GetLength(0) != 
                right._array.GetLength(0) || 
                left._array.GetLength(1) != 
                right._array.GetLength(1))
            {
                throw new ArgumentException("Размеры матриц " +
                    "не совпадают.");
            }

            int rowCount = left._array.GetLength(0);
            int columnCount = left._array.GetLength(1);
            Matrix result = new Matrix(rowCount, columnCount, 
                false);

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; 
                     column++)
                {
                    result.Data[row, column] = left._array[
                        row, column] - right._array[row, column];
                }
            }

            return result;
        }

        public static Matrix operator *(int scalar, Matrix 
            matrix)
        {
            int rowCount = matrix._array.GetLength(0);
            int columnCount = matrix._array.GetLength(
                1);
            Matrix result = new Matrix(rowCount, columnCount, 
                false);

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; 
                     column++)
                {
                    result.Data[row, column] = scalar * 
                        matrix._array[row, column];
                }
            }

            return result;
        }

        public void PrintFibonacciFormat()
        {
            int rowCount = _array.GetLength(0);
            int columnCount = _array.GetLength(1);
            int totalElements = rowCount * columnCount;
            int currentIndex = 0;
            int previousFib = 0;
            int currentFib = 1;

            while (currentIndex < totalElements)
            {
                int elementsToPrint = currentFib;

                for (int i = 0; i < elementsToPrint; i++)
                {
                    if (currentIndex >= totalElements)
                    {
                        break;
                    }

                    int row = currentIndex / columnCount;
                    int column = currentIndex % columnCount;

                    Console.Write(_array[row, column].ToString(
                        ).PadLeft(5) + " ");
                    currentIndex++;
                }

                Console.WriteLine();

                int nextFib = previousFib + currentFib;
                previousFib = currentFib;
                currentFib = nextFib;
            }
        }

        public override string ToString()
        {
            string result = "";
            int rowCount = _array.GetLength(0);
            int columnCount = _array.GetLength(1);

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; 
                     column++)
                {
                    result += _array[row, column].ToString(
                        ).PadLeft(5) + " ";
                }
                result += "\n";
            }
            return result;
        }
    }
}
