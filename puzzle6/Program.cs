using System;
using System.IO;
using System.Linq;

var groupsAnswers = File.ReadAllText("./input.txt").Split("\n\n");

var yesCount = 0;
var sharedYesCount = 0;

foreach (var group in groupsAnswers)
{
    var peopleInGroup = group.Split("\n", StringSplitOptions.RemoveEmptyEntries).Length;
    var charactersInAnswers = group.Replace("\n", string.Empty).ToCharArray();
    
    yesCount += charactersInAnswers.Distinct().Count();
    sharedYesCount += charactersInAnswers.GroupBy(character => character).Count(charGroup => charGroup.Count() == peopleInGroup);
}

Console.WriteLine($"Result part one: {yesCount}");
Console.WriteLine($"Result part two: {sharedYesCount}");

// Part one alternative
var yesCountAlt = groupsAnswers.Select(x => x.Replace("\n", string.Empty).ToCharArray().Distinct().Count()).Sum();