using System;
using System.IO;

namespace AOC21 {
    internal static class Program {
        private static void Main(string[] args) {
            
            var content = File.ReadAllLines(@"C:\temp\1_1.txt");
            Console.WriteLine(new SolveDay1().Solve1(content));
            
        }
    }
}