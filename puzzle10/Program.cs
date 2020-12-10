using System;
using System.IO;
using System.Linq;

namespace puzzle10
{
    class Program
    {
        static void Main(string[] args)
        {
            var adapters = File.ReadLines("./input.txt").Select(int.Parse).ToList();
            adapters.Insert(0, 0);
            adapters.Add(adapters.Last() + 3);
            adapters.Sort();

            int currentJolt = 0, oneOffCount = 0, threeOffCount = 0;
            foreach (var joltRating in adapters.ToHashSet())
            {
                var diff = joltRating - currentJolt;

                if (diff == 1) oneOffCount++;
                else if (diff == 3) threeOffCount++;

                currentJolt = joltRating;
            }

            Console.WriteLine($"Result part one: {oneOffCount * threeOffCount}");
        }
    }
}
