using GreenVsRed.Models;
using System;

namespace GreenVsRed.Factories
{
    internal static class GridFactory
    {
        public static GridCell[][] InitializeGrid()
        {
            var gridSize = GetGridSize();
            var grid = GenerateGrid(gridSize);
            return grid;
        }

        private static GridCell[][] GenerateGrid(int[] gridSize)
        {
            var width = gridSize[0];
            var height = gridSize[1];

            var result = new GridCell[height][];


            for (int row = 0; row < height; row++)
            {
                var sequence = new GridCell[width];

                while (true)
                {
                    Console.Write($"Grid row {row + 1}: ");
                    var input = Console.ReadLine();

                    var digits = input.ToCharArray();
                    if (digits.Length != width)
                    {
                        Console.WriteLine($"Input must be the correct size of the grid width which is {width}");
                        continue;
                    }

                    bool invalidInput = false;
                    for (int digit = 0; digit < digits.Length; digit++)
                    {
                        try
                        {
                            var value = int.Parse(digits[digit].ToString());
                            if (value == 0 || value == 1)
                            {
                                var cellValue = int.Parse(digits[digit].ToString());
                                sequence[digit] = new GridCell(cellValue);
                                continue;
                            }

                            Console.WriteLine($"Input must be a value of 1 or 0");
                            invalidInput = true;
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"Input must be a number");
                            invalidInput = true;
                            break;
                        }
                    }

                    if (invalidInput) continue;

                    break;
                }

                result[row] = sequence;
            }

            return result;
        }

        private static int[] GetGridSize()
        {
            var result = new int[2];
            int width = -1;
            int height = -1;

            while (true)
            {
                Console.Write("Type grid size [x, y]: ");
                var input = Console.ReadLine();
                var values = input.Split(", ");
                if (values.Length < 2 || values.Length > 2)
                {
                    Console.WriteLine("Invalid input format");
                    continue;
                }

                int value;
                var isNumber = int.TryParse(values[0], out value);
                if (isNumber && value > 0 && value < 1000)
                {
                    width = value;
                }
                else
                {
                    Console.WriteLine("X must be a number or more than 0 and less than 1000");
                    continue;
                }

                isNumber = int.TryParse(values[1], out value);
                if (isNumber && value > 0 && value < 1000)
                {
                    height = value;
                }
                else
                {
                    Console.WriteLine("Y must be a number or more than 0 and less than 1000");
                    continue;
                }

                break;
            }

            result[0] = width;
            result[1] = height;

            return result;
        }
    }
}
