using System.IO;

namespace AOC21 {
    internal static class Program {
        private static void Main(string[] args) {
            var content = File.ReadAllLines(@"C:\temp\7.txt");
            var d7 = new Day7(content);
        }
    }
}