using System.IO;

namespace AOC21 {
    internal static class Program {
        private static void Main(string[] args) {
            var content = File.ReadAllLines(@"C:\temp\3.txt");
            //new SolveDay2().Solve2(content);
            //Console.WriteLine(new SolveDay2().Solve1(content));
            var d3 = new SolveDay3Problem1(content);
            d3.Solve();
        }
    }
}