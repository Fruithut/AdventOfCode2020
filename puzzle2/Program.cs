using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("./input.txt");
var regexSpec = new Regex(@"(?<numA>\d+)-(?<numB>\d+) (?<character>\w): (?<password>\w+)");

var validCountPolicyPasswords = 0;
var validPositionPolicyPasswords = 0;

foreach (var inputValue in allLines)
{
    var matchGroups = regexSpec.Match(inputValue).Groups;

    var numA = int.Parse(matchGroups["numA"].Value);
    var numB = int.Parse(matchGroups["numB"].Value);
    var character = char.Parse(matchGroups["character"].Value);
    var password = matchGroups["password"].Value;

    if (IsPasswordCountPolicyValid(numA, numB, character, password)) validCountPolicyPasswords++;
    if (IsPasswordPositionPolicyValid(numA, numB, character, password)) validPositionPolicyPasswords++;
}

Console.WriteLine($"Result part one: {validCountPolicyPasswords}");
Console.WriteLine($"Result part two: {validPositionPolicyPasswords}");

static bool IsPasswordCountPolicyValid(int min, int max, char character, string password)
{
    var count = password.Count(c => c.Equals(character));
    return count >= min && count <= max;
}

static bool IsPasswordPositionPolicyValid(int posA, int posB, char character, string password)
{
    var characterAtPosA = password[posA - 1];
    var characterAtPosB = password[posB - 1];
    return characterAtPosA.Equals(character) ^ characterAtPosB.Equals(character);
}