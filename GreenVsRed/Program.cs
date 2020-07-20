using GreenVsRed.Factories;
using GreenVsRed.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenVsRed
{
    class Program
    {
        static void Main(string[] args)
        {
            GridCell[][] grid = GridFactory.InitializeGrid();
            
            var cellToCount = ValidateCellToTrackInput(grid[0].Length, grid.Length);
            var cellCoordinates = new int[] { int.Parse(cellToCount[1]), int.Parse(cellToCount[0]) };
            var generationsCount = int.Parse(cellToCount[2]);

            var cellToTrack = GetCellToTrack(grid, cellCoordinates);

            //PrintGrid(grid);

            for (int i = 0; i < generationsCount; i++)
            {
                CalculateGeneration(grid);
            }

            Console.WriteLine();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"Number of generations cell with coordinates " +
                $"{cellCoordinates[1]}, {cellCoordinates[0]} stayed green: {cellToTrack.BeenGreenCount}");
        }

        private static string[] ValidateCellToTrackInput(int width, int height)
        {
            string[] result;
            while (true)
            {
                Console.Write("Write the cordinates if the cell you want to track \n" +
                          "and the number of gemnerations in the format [x, y, generations]:");

                result = Console.ReadLine().Split(", ");
                if (result.Length < 3 || result.Length > 3)
                {
                    Console.WriteLine("Invalid input format");
                    continue;
                }

                int num;
                if (!result.All(x => int.TryParse(x, out num)))
                {
                    Console.WriteLine("All values must be numbers");
                    continue;
                }

                if (int.Parse(result[0]) > width - 1 || int.Parse(result[1]) > height - 1)
                {
                    Console.WriteLine("Invalid cell coordinates");
                    continue;
                }

                break;
            }

            return result;
        }

        private static GridCell GetCellToTrack(GridCell[][] grid, int[] coordinates)
        {
            return grid[coordinates[0]][coordinates[1]];
        }

        private static void CalculateGeneration(GridCell[][] grid)
        {
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
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

            //PrintGrid(grid);
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
    }
}
