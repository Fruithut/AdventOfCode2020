using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace puzzle10
{
    class Program
    {
        static void Main(string[] args)
        {
            var adapters = File.ReadLines("./input.txt").Select(int.Parse).ToList();
            adapters.Sort();
            adapters.Add(adapters.Last() + 3);

            var resultPartOne = FindProductOfOneAndThreeDifference(adapters);
            var resultPartTwo = FindAllCombinations(adapters);

            Console.WriteLine($"Result part one: {resultPartOne}");
            Console.WriteLine($"Result part two: {resultPartTwo}");
        }
        
        private static int FindProductOfOneAndThreeDifference(List<int> sortedAdapters)
        {
            int currentJolt = 0, oneOffCount = 0, threeOffCount = 0;
            foreach (var joltRating in sortedAdapters)
            {
                var difference = joltRating - currentJolt;
                if (difference == 1) oneOffCount++;
                else if (difference == 3) threeOffCount++;

                currentJolt = joltRating;
            }

            return oneOffCount * threeOffCount;
        }

        /// <summary>
        /// Found part 2 difficult, see discussion and inspiration below here:
        /// <see href="https://www.reddit.com/r/adventofcode/comments/ka8z8x/2020_day_10_solutions/gf9pg9n/"/>
        /// </summary>
        /// <param name="sortedAdapters">Numbers in ascending order</param>
        /// <returns>All the ways one can access the last adapter</returns>
        private static long FindAllCombinations(List<int> sortedAdapters)
        {
            var ways = new Dictionary<long, long> {{0, 1}};
            foreach (var currentAdapter in sortedAdapters)
            {
                ways.TryGetValue(currentAdapter - 1, out var accessViaNegative1);
                ways.TryGetValue(currentAdapter - 2, out var accessViaNegative2);
                ways.TryGetValue(currentAdapter - 3, out var accessViaNegative3);
                ways[currentAdapter] = accessViaNegative1 + accessViaNegative2 + accessViaNegative3;
            }

            return ways[sortedAdapters.Last()];
        }
    }
}
