using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

var inputRows = File.ReadAllLines("./input.txt");
var rowLength = inputRows[0].Length;

var treeCountList = new List<long>();
var traversalStyle = new List<Point>
{
    new(1, 1),
    new(3, 1),
    new(5, 1),
    new(7, 1),
    new(1, 2),
};

foreach (var traversal in traversalStyle)
{
    var traversalTreeCount = 0;
    var column = traversal.X;

    for (var row = traversal.Y; row < inputRows.Length; row += traversal.Y)
    {
        if (inputRows[row][column] == '#')
        {
            traversalTreeCount++;
        }

        column = (column + traversal.X) % rowLength;
    }

    treeCountList.Add(traversalTreeCount);
}

Console.WriteLine($"Result part one: {treeCountList[1]}");
Console.WriteLine($"Result part two: {treeCountList.Aggregate((sum, num) => sum * num)}");