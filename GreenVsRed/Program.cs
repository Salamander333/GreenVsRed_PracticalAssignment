using System;
using System.Globalization;
using System.Linq;

namespace GreenVsRed
{
    class Program
    {
        static void Main(string[] args)
        {
            var gridSize = GetGridSize();
            int[][] grid = GenerateGrid(gridSize);

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    Console.Write(grid[j]);
                }
                Console.WriteLine();
            }
        }

        private static int[][] GenerateGrid(int[] gridSize)
        {
            var width = gridSize[0];
            var height = gridSize[1];

            var result = new int[height][];


            for (int row = 0; row < height; row++)
            {
                var sequence = new int[width];

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
                                sequence[digit] = int.Parse(digits[digit].ToString());
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
                Console.Write("Grid width size: ");
                var input = Console.ReadLine();
                int value;
                var isNumber = int.TryParse(input, out value);
                if (isNumber && value > 0 && value < 1000)
                {
                    width = value;
                    break;
                }
                else
                {
                    Console.WriteLine("Input must be a number or more than 0 and less than 1000");
                    continue;
                }
            }

            while (true)
            {
                Console.Write("Grid height size: ");
                var input = Console.ReadLine();
                int value;
                var isNumber = int.TryParse(input, out value);
                if (isNumber && value > 0 && value < 1000)
                {
                    height = value;
                    break;
                }
                else
                {
                    Console.WriteLine("Input must be a number or more than 0 and less than 1000");
                    continue;
                }
            }

            result[0] = width;
            result[1] = height;

            return result;
        }
    }
}
