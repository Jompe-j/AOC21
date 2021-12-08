using System.IO;
using System.Text;

namespace AOC21 {
    internal static class Program {
        private static void Main(string[] args) {
            var data = File.ReadAllLines(@"C:\temp\8.txt");
            var d8 = new Day8(data);
            d8.SetSignalPatterns();
        }
    }
}