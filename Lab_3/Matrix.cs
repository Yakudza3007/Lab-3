using System;

namespace ConsoleApp1
{
    internal class Matrix
    {
        private int[,] _array;

        public Matrix(int n, int m)
        {
            Checked c = new Checked();
            int diagonalOffset;
            int startRow;
            int endRow;
            int row;
            int column;

            _array = new int[n, m];
            Console.WriteLine($"\nЗаполнение массива {n}x{m} по диагоналям:");
    
            for (diagonalOffset = m - 1; diagonalOffset >= -(n - 1); diagonalOffset--)
            {
                startRow = Math.Max(0, -diagonalOffset);
                endRow = Math.Min(n - 1, m - 1 - diagonalOffset);

                for (row = startRow; row <= endRow; row++)
                {
                    column = row + diagonalOffset;
                    _array[row, column] = c.ReadInt($"Введите элемент [{row},{column}]: ");
                }
            }
        }

        public Matrix(int n)
        {
            int column;
            int[] temp;
            int i;
            int row;
            Random random = new Random();

            _array = new int[n, n];
            Console.WriteLine($"\nЗаполнение массива {n}x{n} случайными числами:");

            for (column = 0; column < n; column++)
            {
                temp = new int[n];
        
                for (i = 0; i < n; i++)
                {
                    temp[i] = random.Next(-100, 100);
                }
        
                Array.Sort(temp);
                Array.Reverse(temp);

                for (row = 0; row < n; row++)
                {
                    _array[row, column] = temp[row];
                }
            }
        }

        public Matrix(int n, bool isSpiral)
        {
            int currentRow;
            int currentColumn;
            int currentValue;
            int stepLength;
            int[] rowOffsets;
            int[] columnOffsets;
            int direction;
            int turnCounter;
            int stepCounter;

            _array = new int[n, n];
            Console.WriteLine($"\nЗаполнение массива {n}x{n} спиралью:");

            currentRow = (n - 1) / 2;
            currentColumn = (n - 1) / 2;
            currentValue = 1;
            stepLength = 1;

            rowOffsets = new int[] { 0, 1, 0, -1 };
            columnOffsets = new int[] { -1, 0, 1, 0 };
            direction = 0;

            _array[currentRow, currentColumn] = currentValue++;

            while (currentValue <= n * n)
            {
                for (turnCounter = 0; turnCounter < 2; turnCounter++)
                {
                    for (stepCounter = 0; stepCounter < stepLength; stepCounter++)
                    {
                        currentRow += rowOffsets[direction];
                        currentColumn += columnOffsets[direction];

                        if (currentRow >= 0 && currentRow < n && currentColumn >= 0 && currentColumn < n && _array[currentRow, currentColumn] == 0)
                        {
                            _array[currentRow, currentColumn] = currentValue++;
                        }
                    }
                    direction = (direction + 1) % 4;
                }
                stepLength++;
            }
        }
        
        public Matrix(int rowCount, int columnCount, bool isStandardInput)
        {
            Checked c = new Checked();
            int row;
            int column;

            _array = new int[rowCount, columnCount];
            
            if (isStandardInput)
            {
                Console.WriteLine($"\nЗаполнение массива {rowCount}x{columnCount} в обычном порядке:");

                for (row = 0; row < rowCount; row++)
                {
                    for (column = 0; column < columnCount; column++)
                    {
                        _array[row, column] = c.ReadInt($"Введите элемент [{row},{column}]: ");
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
            int rowCount;
            int columnCount;
            Matrix result;
            int row;
            int column;

            rowCount = _array.GetLength(0);
            columnCount = _array.GetLength(1);
            result = new Matrix(columnCount, rowCount, false);

            for (row = 0; row < rowCount; row++)
            {
                for (column = 0; column < columnCount; column++)
                {
                    result.Data[column, row] = _array[row, column];
                }
            }

            return result;
        }

        public static Matrix operator +(Matrix left, Matrix right)
        {
            int rowCount;
            int columnCount;
            Matrix result;
            int row;
            int column;

            if (left._array.GetLength(0) != right._array.GetLength(0) || left._array.GetLength(1) != right._array.GetLength(1))
            {
                throw new ArgumentException("Размеры матриц не совпадают.");
            }

            rowCount = left._array.GetLength(0);
            columnCount = left._array.GetLength(1);
            result = new Matrix(rowCount, columnCount, false);

            for (row = 0; row < rowCount; row++)
            {
                for (column = 0; column < columnCount; column++)
                {
                    result.Data[row, column] = left._array[row, column] + right._array[row, column];
                }
            }

            return result;
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            int rowCount;
            int columnCount;
            Matrix result;
            int row;
            int column;

            if (left._array.GetLength(0) != right._array.GetLength(0) || left._array.GetLength(1) != right._array.GetLength(1))
            {
                throw new ArgumentException("Размеры матриц не совпадают.");
            }

            rowCount = left._array.GetLength(0);
            columnCount = left._array.GetLength(1);
            result = new Matrix(rowCount, columnCount, false);

            for (row = 0; row < rowCount; row++)
            {
                for (column = 0; column < columnCount; column++)
                {
                    result.Data[row, column] = left._array[row, column] - right._array[row, column];
                }
            }

            return result;
        }

        public static Matrix operator *(int scalar, Matrix matrix)
        {
            int rowCount;
            int columnCount;
            Matrix result;
            int row;
            int column;

            rowCount = matrix._array.GetLength(0);
            columnCount = matrix._array.GetLength(1);
            result = new Matrix(rowCount, columnCount, false);

            for (row = 0; row < rowCount; row++)
            {
                for (column = 0; column < columnCount; column++)
                {
                    result.Data[row, column] = scalar * matrix._array[row, column];
                }
            }

            return result;
        }

        public void PrintFibonacciFormat()
        {
            int rowCount;
            int columnCount;
            int totalElements;
            int currentIndex;
            int previousFib;
            int currentFib;
            int elementsToPrint;
            int i;
            int row;
            int column;
            int nextFib;

            rowCount = _array.GetLength(0);
            columnCount = _array.GetLength(1);
            totalElements = rowCount * columnCount;
            currentIndex = 0;
            previousFib = 0;
            currentFib = 1;

            while (currentIndex < totalElements)
            {
                elementsToPrint = currentFib;

                for (i = 0; i < elementsToPrint; i++)
                {
                    if (currentIndex >= totalElements)
                    {
                        break;
                    }

                    row = currentIndex / columnCount;
                    column = currentIndex % columnCount;

                    Console.Write(_array[row, column].ToString().PadLeft(5) + " ");
                    currentIndex++;
                }

                Console.WriteLine();

                nextFib = previousFib + currentFib;
                previousFib = currentFib;
                currentFib = nextFib;
            }
        }

        public override string ToString()
        {
            string result;
            int rowCount;
            int columnCount;
            int row;
            int column;

            result = "";
            rowCount = _array.GetLength(0);
            columnCount = _array.GetLength(1);

            for (row = 0; row < rowCount; row++)
            {
                for (column = 0; column < columnCount; column++)
                {
                    result += _array[row, column].ToString().PadLeft(5) + " ";
                }
                result += "\n";
            }
            return result;
        }
    }
}