using GreenVsRed.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GreenVsRed
{
    class Program
    {
        static void Main(string[] args)
        {
            var gridSize = GetGridSize();
            GridCell[][] grid = GenerateGrid(gridSize);

            Console.Write("Write the cordinates if the cell you want to track \n" +
                          "and the number of gemnerations in the format [x, y, generations]:");
            var cellToCount = Console.ReadLine().Split(", ");
            var cellCoordinates = new int[] { int.Parse(cellToCount[0]), int.Parse(cellToCount[1]) };
            var generationsCount = int.Parse(cellToCount[2]);

            PrintGrid(grid);

            for (int i = 0; i < generationsCount; i++)
            {
                for (int row = 0; row < grid.Length; row++)
                {
                    for (int col = 0; col < grid[row].Length ; col++)
                    {
                        var neighboringCells = new List<GridCell>();
                        if (col > 0) neighboringCells.Add(grid[row][col - 1]);
                        if (col > 0 && row > 0) neighboringCells.Add(grid[row - 1][col - 1]);
                        if (row > 0) neighboringCells.Add(grid[row - 1][col]);
                        if (row > 0 && col < grid[row].Length - 1) neighboringCells.Add(grid[row - 1][col + 1]);
                        if (col < grid[row].Length - 1) neighboringCells.Add(grid[row][col + 1]);
                        if (row < grid.Length - 1 && col < grid[row].Length - 1) neighboringCells.Add(grid[row + 1][col + 1]);
                        if (row < grid.Length - 1) neighboringCells.Add(grid[row + 1][col]);
                        if (row < grid.Length - 1 && col > 0) neighboringCells.Add(grid[row + 1][col - 1]);

                        if (grid[row][col].Value == 0)
                        {
                            var greenCellsSurroundingCount = 0;
                            foreach (var cell in neighboringCells)
                            {
                                if (cell.Value == 1) greenCellsSurroundingCount++;
                            }

                            if (greenCellsSurroundingCount == 3 || greenCellsSurroundingCount == 6) grid[row][col].SwitchesNextGeneration = true;
                        }
                        else if (grid[row][col].Value == 1)
                        {
                            var greenCellsSurroundingCount = 0;
                            foreach (var cell in neighboringCells)
                            {
                                if (cell.Value == 1) greenCellsSurroundingCount++;
                            }

                            if (greenCellsSurroundingCount != 2 &&
                                greenCellsSurroundingCount != 3 &&
                                greenCellsSurroundingCount != 6) grid[row][col].SwitchesNextGeneration = true;
                        }
                    }
                }

                for (int row = 0; row < grid.Length; row++)
                {
                    for (int col = 0; col < grid[row].Length; col++)
                    {
                        var cell = grid[row][col];
                        if (cell.SwitchesNextGeneration) cell.SwitchState();
                        if (cell.Value == 1) cell.IncrementBeenGreenCount();
                    }
                }

                PrintGrid(grid);
            }
        }

        private static void PrintGrid(GridCell[][] grid)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    Console.Write(grid[i][j].Value);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
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
