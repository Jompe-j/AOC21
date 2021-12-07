using System;
using System.Collections.Generic;
using System.IO;

namespace AOC21 {
    internal static class Program {
        private static void Main(string[] args) {
            var content = File.ReadAllLines(@"C:\temp\7.txt");
            var d7 = new Day7(content);
        }
    }

    internal class Day7 {
        private readonly int[] data;
        private readonly Dictionary<int, int> _dataDictionary = new();
        private int largest;
        private int smallest = int.MaxValue;
        private List<List<int>> allCosts = new();

        public Day7(string[] content) {
            data = Array.ConvertAll(content[0].Split(","), int.Parse);

            foreach (var i in data) {
                if (!_dataDictionary.TryAdd(i, 1)) {
                    _dataDictionary[i]++;
                }

                if (i > largest) {
                    largest = i;
                }

                if (i < smallest) {
                    smallest = i;
                }
            }

            SetVisitCost();

            CalculateMinimumCost();
        }

        private void CalculateMinimumCost() {
            var lowestCost = int.MaxValue;
            var total = 0;

            for (var i = 0; i < allCosts[0].Count; i++) {
                for (var j = 0; j < allCosts.Count; j++) {
                    var tmp = allCosts[j][i];
                    total += tmp;
                }

                if (total < lowestCost) {
                    lowestCost = total;
                }

                total = 0;
            }

            Console.WriteLine($"Lowest cost is {lowestCost}");
        }

        private void SetVisitCost() {
            foreach (var keyValPair in _dataDictionary) {
                var visitCost = new List<int>();

                var occurances = keyValPair.Value;
                var downDistance = keyValPair.Key;

                for (var i = 0; i <= largest; i++) {
                    if (downDistance >= 0) {
                        var tmp = 0;

                        for (var j = downDistance; j > 0; j--) {
                            tmp += j;
                        }

                        visitCost.Add(tmp * occurances);
                        downDistance--;
                    }
                    else {
                        var price = 0;

                        for (var j = 1; j <= i - keyValPair.Key; j++) {
                            price += j;
                        }

                        visitCost.Add(price * occurances);
                    }
                }

                allCosts.Add(visitCost);
            }
        }
    }
}