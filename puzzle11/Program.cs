using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace puzzle11
{
    class Program
    {
        private static readonly List<Point> Directions = new()
        {
            new Point {X = 1, Y = 1},
            new Point {X = 1, Y = 0},
            new Point {X = 0, Y = 1},
            new Point {X = 1, Y = -1},
            new Point {X = -1, Y = 0},
            new Point {X = -1, Y = 1},
            new Point {X = -1, Y = -1},
            new Point {X = 0, Y = -1},
        };
        
        static void Main(string[] args)
        {
            var rows = File.ReadLines("./input.txt").ToList();

            var matrix = GetMatrixFromStringRows(rows);

            var resultPartOne = GetFinalOccupiedSeatCount(matrix, 4, 1);
            Console.WriteLine($"Result part one: {resultPartOne}");
            
            var resultPartTwo = GetFinalOccupiedSeatCount(matrix, 5, int.MaxValue);
            Console.WriteLine($"Result part two: {resultPartTwo}");
        }
        
        private static int GetFinalOccupiedSeatCount(char[,] currentState, int occupiedThreshold, int viewDistance)
        {
            var height = currentState.GetLength(0);;
            var width = currentState.GetLength(1);
            
            var modifiedState = (char[,]) currentState.Clone();
            
            var noChanges = true;
            while (noChanges)
            {
                var hasChanged = false;

                for (var row = 0; row < height; row++)
                {
                    for (var col = 0; col < width; col++)
                    {
                        var currentSeat = currentState[row, col];

                        if (currentSeat != '.')
                        {
                            var adjacentSeatPositions = GetAdjacentPoints(currentState, new Point(col, row), viewDistance);
                        
                            var newSeat = modifiedState[row, col] = currentSeat switch
                            {
                                'L' when SeatBecomesOccupied(adjacentSeatPositions) => '#',
                                '#' when SeatBecomesAvailable(adjacentSeatPositions, occupiedThreshold) => 'L',
                                _ => modifiedState[row, col]
                            };

                            if (currentSeat != newSeat) hasChanged = true;
                        }
                    }
                }

                noChanges = hasChanged;

                if (noChanges) currentState = (char[,]) modifiedState.Clone();
            }

            var occupiedCount = 0;
            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    if (modifiedState[row, col] == '#') occupiedCount++;
                }
            }

            return occupiedCount;
        }

        private static IEnumerable<char> GetAdjacentPoints(char[,] matrix, Point point, int distanceLimit)
        {
            var points = new LinkedList<char>();
            var height = matrix.GetLength(0);
            var width = matrix.GetLength(1);
            
            foreach (var direction in Directions)
            {
                var limitCounter = 0;
                
                var nextPosition = point;
                nextPosition.Offset(direction);
                
                while (IsValidPoint(width, height, nextPosition))
                {
                    if (limitCounter == distanceLimit) break;

                    var seat = matrix[nextPosition.Y, nextPosition.X];
                    if (seat == '#' || seat == 'L')
                    {
                        points.AddLast(seat);
                        break;
                    }
                    
                    nextPosition.Offset(direction);
                    limitCounter++;
                }
            }

            return points;
        }
        
        private static bool SeatBecomesAvailable(IEnumerable<char> adjacentSeats, int occupiedThreshold)
        {
            return adjacentSeats.Count(c => c == '#') >= occupiedThreshold;
        }
        
        private static bool SeatBecomesOccupied(IEnumerable<char> adjacentSeats)
        {
            return adjacentSeats.All(c => c == 'L' || c == '.');
        }

        private static bool IsValidPoint(int width, int height, Point point)
        {
            return point.X >= 0 && point.X < width && point.Y >= 0 && point.Y < height;
        }

        private static char[,] GetMatrixFromStringRows(IReadOnlyList<string> rows)
        {
            var matrix = new char[rows.Count, rows[0].Length];
            
            for (var row = 0; row < rows.Count; row++)
            {
                var seatRow = rows[row];
                for (var col = 0; col < seatRow.Length; col++)
                {
                    matrix[row, col] = seatRow[col];
                }
            }

            return matrix;
        }
    }
}
