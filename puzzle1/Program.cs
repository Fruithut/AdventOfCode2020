using System;
using System.IO;
using System.Linq;

const int total = 2020;

var allLines = File.ReadAllLines("./input.txt").Select(int.Parse).ToList();
var numberDictionary = allLines.ToDictionary(value => value, value => true);

var resultingMultiplication = 0;
foreach (var inputValue in allLines)
{
    var differenceMissing = total - inputValue;
    
    if (numberDictionary.ContainsKey(differenceMissing))
    {
        resultingMultiplication = inputValue * differenceMissing;
        break;
    }
}

Console.WriteLine($"Result part one: {resultingMultiplication}");