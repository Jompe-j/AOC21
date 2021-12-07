using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC21 {
    internal class Day6 {
        private readonly List<int> data;

        public Day6(string[] content) {
            // BasicSolve(content);
            var fishCounter = new List<ulong>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var tmpData = Array.ConvertAll(content[0].Split(","), int.Parse).ToList();

            foreach (var d in tmpData) {
                fishCounter[d]++;
            }

            for (int i = 0; i < fishCounter.Count; i++) {
                Console.Write($"{i},");
            }

            Console.WriteLine();

            foreach (var f in fishCounter) {
                Console.Write($"{f},");
            }

            Console.WriteLine();

            for (int i = 0; i < 256; i++) {
                var tmpList = new List<ulong>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                for (var j = fishCounter.Count-1; j >= 0; j--) {
                    if (j != 0) {
                        tmpList[j - 1] = fishCounter[j];
                    }
                    else {
                        tmpList[fishCounter.Count - 1] = fishCounter[j];
                        tmpList[6] += fishCounter[j];
                    } 
                    
                }

                fishCounter = tmpList;
            }
            ulong tmp = 0;
            foreach (var count in fishCounter) {
                tmp += count;
                Console.WriteLine(count + " "); 
            }
            Console.WriteLine(tmp);
            
            //
            // Console.WriteLine();
            // foreach (var i in fishCounter) {
            //     Console.Write(i +" ");
            // }
        }

        private static void BasicSolve(string[] content) {
            var tmpData = Array.ConvertAll(content[0].Split(","), int.Parse).ToList();

            for (int i = 0; i < 80; i++) {
                var newFish = new List<int>();

                for (var index = 0; index < tmpData.Count; index++) {
                    if (tmpData[index] == 0) {
                        tmpData[index] = 6;
                        newFish.Add(8);
                    }
                    else {
                        tmpData[index]--;
                    }
                }

                foreach (var nf in newFish) {
                    tmpData.Add(nf);
                }

                Console.Write($"Day {i + 1}: ");

                // foreach (var pf in tmpData) {
                //     Console.Write($"{pf},");
                // }
                Console.Write(" Count:" + tmpData.Count);
                Console.WriteLine();
            }

            Console.Write(" Count:" + tmpData.Count);
        }
    }
}