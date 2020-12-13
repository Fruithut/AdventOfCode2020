using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace puzzle13
{
    class Program
    {
        static void Main(string[] args)
        {
            var draft = File.ReadLines("./input.txt").ToList();
            var earliestDeparture = int.Parse(draft[0]);
            var busIds = draft[1].Split(',');

            var resultPartOne = busIds
                .Where(id => id != "x")
                .Select(int.Parse)
                .Select(id => (busId: id, difference: (earliestDeparture / id + 1) * id - earliestDeparture))
                .OrderBy(tuple => tuple.difference)
                .Select(tuple => tuple.busId * tuple.difference)
                .First();

            Console.WriteLine($"Result part one: {resultPartOne}");

            /*
             For part 2 i threw the input into a chinese remainder solver 
             Theory: https://en.wikipedia.org/wiki/Chinese_remainder_theorem
             Setup for example data would be something like this:
             (t + 0) mod 7 = 0
             (t + 1) mod 13 = 0
             (t + 4) mod 59 = 0
             (t + 6) mod 31 = 0
             (t + 7) mod 19 = 0
            */
        }

        
        // First working attempt part 1
        private static int CalculatePartOne(int earliestDeparture, IEnumerable<string> busses)
        {
            var timeMap = new Dictionary<int, List<int>>();

            foreach (var bus in busses)
            {
                if (bus == "x") continue;
                var id = int.Parse(bus);
                
                timeMap.Add(id, new List<int>());
                for (var i = 0; i < earliestDeparture + id; i += id)
                {
                    if (i >= earliestDeparture) timeMap[id].Add(i);
                }
            }

            var min = int.MaxValue;
            var selectedBus = 0;
            foreach (var bus in timeMap.Keys)
            {
                var timeStamp = timeMap[bus].Min();
                if (timeStamp < min)
                {
                    selectedBus = bus;
                    min = timeStamp;
                }
            }

            return (min - earliestDeparture) * selectedBus;
        }
    }
}
