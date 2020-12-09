using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var numbers = File.ReadLines("./input.txt").Select(long.Parse).ToArray();
var invalidNumber = FindFirstInvalidNumber(numbers, 25);
var sequentialNumbers = FindSequentialNumbersForNumber(numbers, invalidNumber).ToList();

Console.WriteLine($"Result part one: {invalidNumber}");
Console.WriteLine($"Result part two: {sequentialNumbers.First() +  sequentialNumbers.Last()}");

static long FindFirstInvalidNumber(IReadOnlyList<long> numbers, int preambleLength)
{
    for (var i = preambleLength; i < numbers.Count; i++)
    {
        var currentNumber = numbers[i];
        var preamble = numbers.Skip(i - preambleLength).Take(preambleLength).ToHashSet();
        var possible = preamble.Any(preambleNumber => preamble.Contains(currentNumber - preambleNumber));
        if (!possible) return currentNumber;
    }

    return -1;
}
    
static IEnumerable<long> FindSequentialNumbersForNumber(IReadOnlyCollection<long> numbers, long targetNumber)
{
    var stepSize = 2;
    
    while (stepSize < numbers.Count)
    {
        for (var i = stepSize; i < numbers.Count; i++)
        {
            var stepSum = numbers.Skip(i - stepSize).Take(stepSize).Sum();
            if (stepSum == targetNumber)
            {
                return numbers.Skip(i - stepSize).Take(stepSize);
            }
        }
        stepSize++;
    }
    
    return new List<long>();
}