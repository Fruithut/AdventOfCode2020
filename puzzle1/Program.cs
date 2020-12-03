using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

const int total = 2020;
var numbers = File.ReadAllLines("./input.txt").Select(int.Parse).ToList();

var numberDictionary = numbers.ToHashSet();
var resultPartOne = 
    (from inputValue in numbers
        let differenceMissing = total - inputValue 
        where numberDictionary.Contains(differenceMissing) 
        select inputValue * differenceMissing)
    .FirstOrDefault();

var resultPartTwo =
    (from a in numbers
        from b in numbers
        from c in numbers
        where a + b + c == total
        select a * b * c)
    .FirstOrDefault();

Console.WriteLine($"Result part one: {resultPartOne}");
Console.WriteLine($"Result part two: {resultPartTwo}");

// Part one alternative
var numberSet = new HashSet<int>();
var resultPartOneAlt = 0;

foreach (var inputValue in numbers)
{
    var diff = total - inputValue;
    if (numberSet.Contains(diff))
    {
        resultPartOneAlt = inputValue * diff;
        break;
    }
    numberSet.Add(inputValue);
}

// Part two alternative
var numberSumMap = new Dictionary<int, int[]>();
var resultPartTwoAlt = 0;

for (var i = 0; i < numbers.Count; i++)
{
    var numA = numbers[i];

    for (var j = i + 1; j < numbers.Count; j++)
    {
        var numB = numbers[j];
        var sum = numA + numB;
        numberSumMap[sum] = new[] {numA, numB};
    }
}

foreach (var num in numbers)
{
    var diff = total - num;
    if (numberSumMap.ContainsKey(diff))
    {
        resultPartTwoAlt = num * numberSumMap[diff][0] * numberSumMap[diff][1];
        break;
    }
}

Console.WriteLine($"Result part one alternative: {resultPartOneAlt}");
Console.WriteLine($"Result part two alternative: {resultPartTwoAlt}");