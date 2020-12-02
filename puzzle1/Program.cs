using System;
using System.IO;
using System.Linq;

const int total = 2020;

var inputValues = File.ReadAllLines("./input.txt").Select(int.Parse).ToList();

var numberDictionary = inputValues.ToDictionary(value => value, value => true);

var resultPartOne = 
    (from inputValue in inputValues
        let differenceMissing = total - inputValue 
        where numberDictionary.ContainsKey(differenceMissing) 
        select inputValue * differenceMissing)
    .FirstOrDefault();

var resultPartTwo =
    (from a in inputValues
        from b in inputValues
        from c in inputValues
        where a + b + c == total
        select a * b * c)
    .FirstOrDefault();

Console.WriteLine($"Result part one: {resultPartOne}");
Console.WriteLine($"Result part two: {resultPartTwo}");