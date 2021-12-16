using System;
using System.Collections.Generic;
using System.IO;

namespace AOC21 {
    internal static class Program {
        private static void Main() {
            var dataLst = new List<string[]>();
           dataLst.Add(File.ReadAllLines(@"C:\temp\tmp.txt")); 
            dataLst.Add(File.ReadAllLines(@"C:\temp\tmp1.txt"));
            dataLst.Add(File.ReadAllLines(@"C:\temp\tmp2.txt"));
            dataLst.Add(File.ReadAllLines(@"C:\temp\12.txt"));
            foreach (var data in dataLst) {
                var d = new Day12(data);
            }

            Console.WriteLine();
            Console.WriteLine("End");
        }
    }
}