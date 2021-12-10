using System;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.IO.Enumeration;

namespace AOC21 {
    internal static class Program {
        private static void Main() {
            var data = File.ReadAllLines(@"C:\temp\10.txt");
            var d = new Day10(data);
            Console.WriteLine("End");
        }
    }
}