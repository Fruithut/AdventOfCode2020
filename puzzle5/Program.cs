using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

var seats = File.ReadAllLines("./input.txt");

SolveUsingDivisionApproach(seats);
SolveUsingBinaryApproach(seats);

static void SolveUsingDivisionApproach(IEnumerable<string> seats)
{
    var seatIds = seats
        .Select(x => GetRowFromBoardingSpecification(x) * 8 + GetColFromBoardingSpecification(x))
        .OrderBy(x => x)
        .ToList();

    var largestId = seatIds.Last();
    var missingId = 0;

    for (var i = 1; i < seatIds.Count; i++)
    {
        var difference = seatIds[i] - seatIds[i - 1];
        if (difference != 1)
        {
            missingId = seatIds[i] - 1;
            break;
        }
    }

    Console.WriteLine($"Result part one: {largestId}");
    Console.WriteLine($"Result part two: {missingId}");
}

static void SolveUsingBinaryApproach(IEnumerable<string> seats)
{
    var seatIdsAlt = seats.Select(x =>
    {
        var binary = GetBinaryRepresentation(x);
        var row = Convert.ToInt32(binary.Substring(0, 7), 2);
        var col = Convert.ToInt32(binary.Substring(7, 3), 2);
        return row * 8 + col;
    }).OrderBy(x => x).ToList();

    var smallestId = seatIdsAlt.First();
    var largestId = seatIdsAlt.Last();

    // Assumes 1 missing number
    var missingId = Enumerable.Range(smallestId, largestId).Except(seatIdsAlt).First();

    Console.WriteLine($"Result part one alternative: {largestId}");
    Console.WriteLine($"Result part two alternative: {missingId}");
}

static string GetBinaryRepresentation(string boardSpec)
{
    return new StringBuilder(boardSpec)
        .Replace("F", "0")
        .Replace("B", "1")
        .Replace("L", "0")
        .Replace("R", "1")
        .ToString();
}

static int GetColFromBoardingSpecification(string boardSpec)
{
    int lower = 0, upper = 7;

    for (var i = 7; i < boardSpec.Length - 1; i++)
    {
        var currentCharacter = boardSpec[i];
        switch (currentCharacter)
        {
            case 'L':
                upper = (lower + upper) / 2;
                break;
            case 'R':
                lower = (lower + upper - 1) / 2 + 1;
                break;
        }
    }

    return boardSpec[9] == 'L' ? lower : upper;
}

static int GetRowFromBoardingSpecification(string boardSpec)
{
    int lower = 0, upper = 127;

    for (var i = 0; i < boardSpec.Length - 4; i++)
    {
        var currentCharacter = boardSpec[i];

        switch (currentCharacter)
        {
            case 'F':
                upper = (lower + upper) / 2;
                break;
            case 'B':
                lower = (lower + upper - 1) / 2 + 1;
                break;
        }
    }

    return boardSpec[6] == 'F' ? lower : upper;
}