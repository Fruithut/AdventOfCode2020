using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace puzzle12
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = File.ReadLines("./input.txt").ToList();

            var transformedVectorPartOne = CalculateFinalVectorPartOne(Vector2.Zero, new Vector2(1, 0), commands);
            var resultPartOne = CalculateManhattanDistance(Vector2.Zero, transformedVectorPartOne);
            Console.WriteLine($"Result part one: {resultPartOne}");
            
            var transformedVectorPartTwo = CalculateFinalVectorPartTwo(Vector2.Zero, new Vector2(10, -1), commands);
            var resultPartTwo = CalculateManhattanDistance(Vector2.Zero, transformedVectorPartTwo);
            Console.WriteLine($"Result part one: {resultPartTwo}");
        }

        private static Vector2 CalculateFinalVectorPartOne(Vector2 currentVector, Vector2 currentDirection, IEnumerable<string> commands)
        {
            foreach (var command in commands)
            {
                var direction = command[0];
                var amount = long.Parse(command.Substring(1));
                
                switch (direction)
                {
                    case 'N': currentVector += new Vector2(0, amount * -1); break;
                    case 'S': currentVector += new Vector2(0, amount); break;
                    case 'W': currentVector += new Vector2(amount * -1, 0); break;
                    case 'E': currentVector += new Vector2(amount, 0); break;
                    case 'L': currentDirection = GetRotatedVectorByDegrees(currentDirection, -amount); break;
                    case 'R': currentDirection = GetRotatedVectorByDegrees(currentDirection, amount); break;
                    case 'F': currentVector += currentDirection * amount; break;
                }
            }

            return currentVector;
        }
        
        private static Vector2 CalculateFinalVectorPartTwo(Vector2 ship, Vector2 waypoint, IEnumerable<string> commands)
        {
            foreach (var command in commands)
            {
                var direction = command[0];
                var amount = long.Parse(command.Substring(1));
  
                switch (direction)
                {
                    case 'N': waypoint += new Vector2(0, amount * -1); break;
                    case 'S': waypoint += new Vector2(0, amount); break;
                    case 'W': waypoint += new Vector2(amount * -1, 0); break;
                    case 'E': waypoint += new Vector2(amount, 0); break;
                    case 'L': waypoint = GetRotatedVectorByDegrees(waypoint, -amount); break;
                    case 'R': waypoint = GetRotatedVectorByDegrees(waypoint, amount); break;
                    case 'F': ship += waypoint * amount; break;
                }
            }

            return ship;
        }

        private static float CalculateManhattanDistance(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        private static Vector2 GetRotatedVectorByDegrees(Vector2 vector, double degrees)
        {
            var radians = Math.PI / 180 * degrees;

            var ca = Math.Cos(radians);
            var sa = Math.Sin(radians);
            return new Vector2(
                (float) (ca * vector.X - sa * vector.Y),
                (float) (sa * vector.X + ca * vector.Y)
            );
        }
    }
}
