using System;
using System.Collections.Generic;

namespace AOC21 {
    internal class SolveDay3Problem1 {
        private readonly string[] diagnosticData;
        private List<int> gammaRate = new();
        private List<int> epsilonRate = new();
        public SolveDay3Problem1(string[] content) {
            diagnosticData = content; 
        }

        public void Solve() {
            var length = diagnosticData[0].Length;
            var common = 0;

            for (var i = 0; i < length; i++) {
                foreach (var r in diagnosticData) {
                    if (r[i] == '1') {
                        common++;
                    }
                    else {
                        common--;
                    }
                } 

                if (common > 0 ) {
                    gammaRate.Add(1);
                    epsilonRate.Add(0);
                } else if (common < 0) {
                    gammaRate.Add(0);
                    epsilonRate.Add(1);
                }
                else {
                    throw new Exception("Equal");
                }
                common = 0;

            }

            var gammaAsDecimal = Convert.ToInt32(string.Join("", gammaRate), 2);
            var epsilonAsDecimal = Convert.ToInt32(string.Join("", epsilonRate), 2);
            
            Console.WriteLine(epsilonAsDecimal * gammaAsDecimal);
            Console.WriteLine();
        }
    }
}