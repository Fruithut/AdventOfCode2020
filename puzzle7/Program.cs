using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace puzzle7
{
    class Program
    {
        static void Main(string[] args)
        {
            var allLines = File.ReadAllLines("./input.txt");

            var containerRegex = new Regex(@"((?<bagName>\w+ \w+) bags contain)");
            var contentsRegex = new Regex(@"((?<bagCount>\d) (?<bagName>\w+ \w+))");

            var bagParentToChildren = new Dictionary<string, IEnumerable<BagSpecification>>();

            foreach (var rule in allLines)
            {
                var containerBag = containerRegex.Match(rule).Groups["bagName"].Value;
                var contentBags = contentsRegex
                    .Matches(rule)
                    .Select(x => new BagSpecification(x.Groups["bagName"].Value, int.Parse(x.Groups["bagCount"].Value)));
                
                bagParentToChildren.Add(containerBag, contentBags);
            }

            var canReachGold = CountBagsLeadingToShinyGoldBags(bagParentToChildren);
            var totalBags = CountTotalBagsInsideBag("shiny gold", bagParentToChildren) - 1;

            Console.WriteLine($"Result part one: {canReachGold}");
            Console.WriteLine($"Result part two: {totalBags}");
        }

        private static int CountTotalBagsInsideBag(string bagName, IReadOnlyDictionary<string, IEnumerable<BagSpecification>> bagToContentMap)
        {
            return 1 + bagToContentMap[bagName].Sum(bagSpec => bagSpec.Count * CountTotalBagsInsideBag(bagSpec.Name, bagToContentMap));;
        }

        private static int CountBagsLeadingToShinyGoldBags(IReadOnlyDictionary<string, IEnumerable<BagSpecification>> bagToContentMap)
        {
            var bagsThatCanContainGold = new HashSet<string>();
            
            foreach (var parentBag in bagToContentMap.Keys)
            {
                var bagExploreStack = new Stack<BagSpecification>();
                
                foreach (var bagDescription in bagToContentMap[parentBag])
                {
                    bagExploreStack.Push(bagDescription);
                }

                while (bagExploreStack.Any())
                {
                    var (bagName, _) = bagExploreStack.Pop();

                    if (bagName.Equals("shiny gold"))
                    {
                        bagsThatCanContainGold.Add(parentBag);
                    }
                    else
                    {
                        foreach (var bagDescription in bagToContentMap[bagName])
                        {
                            bagExploreStack.Push(bagDescription);
                        }
                    }
                }
            }

            return bagsThatCanContainGold.Count;
        }

        public record BagSpecification(string Name, int Count);
    }
}
