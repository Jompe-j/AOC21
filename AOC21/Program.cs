using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Threading.Channels;

namespace AOC21 {
    internal static class Program {
        private static void Main() {
            var data = File.ReadAllLines(@"C:\temp\tmp.txt");
            var d = new Day12(data);

            Console.WriteLine();
            Console.WriteLine("End");
        }
    }

    internal class Day12 {
        private readonly List<Cave> cavesInfo = new();
        private List<List<string>> Paths = new();

        public Day12(string[] data) {
            foreach (var cavePair in data) {
                var caveString1 = cavePair.Split("-")[0];
                var caveString2 = cavePair.Split("-")[1];

                AddCave(caveString1, caveString2);
                AddCave(caveString2, caveString1);
            }

            var start = cavesInfo.Find(c => c.CaveString == "start");
            var path = new List<string>();

            if (start == null) return;

            FindPath(start, path);
            Console.WriteLine(Paths.Count);

            Console.WriteLine("End of day 12");
        }

        private void FindPath(Cave currentCave, List<string> path) {
            if (currentCave.CaveString == "end") {
                path.Add(currentCave.CaveString);
                Paths.Add(path);

                return;
            }

            var oneSmallCaveTwice = IsSmallCaveSetTwice(path);

            foreach (var relation in currentCave.relations) {
                if (path.Exists(x => x.Equals(currentCave.CaveString) && currentCave.smallCave)) {
                    continue;
                }

                if (relation.CaveString == "start") {
                    continue;
                }

                var lst = new List<string>(path) { currentCave.CaveString };
                FindPath(relation, lst);
            }
        }

        private KeyValuePair<string, bool> IsSmallCaveSetTwice(List<string> path) {
            for (var i = 0; i < path.Count; i++) {
                var count = 1;

                if (char.IsLower(path[i][0])) {
                    var cave = path[i];

                    for (var j = i + 1; j < path.Count - 1; j++) {
                        if (cave == path[j]) {
                            count++;

                            if (count >= 2) {
                                var vp = new KeyValuePair<string, bool>(cave, true);

                                return vp;
                            }
                        }
                    }
                }
            }

            return new KeyValuePair<string, bool>("", false);
        }

        private void AddCave(string cave, string relation) {
            if (!cavesInfo.Exists(c => c.CaveString == cave)) {
                cavesInfo.Add(new Cave(cave));
            }

            var cv = cavesInfo.Find(c => c.CaveString == cave);

            if (!cavesInfo.Exists(c => c.CaveString == relation)) {
                cavesInfo.Add(new Cave(relation));
            }

            cv?.Relations(cavesInfo.Find(c => c.CaveString == relation));
        }
    }

    internal class Cave {
        public bool smallCave;
        public string CaveString;
        public readonly List<Cave> relations = new();

        public Cave(string cave) {
            CaveString = cave;

            if (char.IsLower(cave[0])) {
                smallCave = true;
            }
        }

        public void Relations(Cave cave) {
            relations.Add(cave);
        }
    }
}