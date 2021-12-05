using System;
using System.Linq;

namespace AOC21 {
    internal class SolveDay3Problem2 {
        private readonly string[] data;
        
        public SolveDay3Problem2(string[] content) {
            data = content;
        }

        public void FindFilter() {
            var d =FilterOxy(data);
            var e = FilterCo2(data);
            Console.WriteLine(d*e);
            


        }

        private int FilterCo2(string[] strings) {
            var list = strings.ToList();
            var length = list[0].Length;
            
            for (int i = 0; i < length; i++) {
                var dataCount = (float) list.Count;
                var d = list.FindAll(s => s[i] == '1');

                if (d.Count >= dataCount - d.Count ) {
                    list.RemoveAll(s => s[i] == '1');
                }
                else {
                    list.RemoveAll(s => s[i] == '0');
                }
                
                if (list.Count == 1) {
                    break;
                } 
            }

            return Convert.ToInt32(list[0], 2);
        }

        private int FilterOxy(string[] strings) {
            var list = strings.ToList();
            var length = list[0].Length;
            
            for (int i = 0; i < length; i++) {
                var dataCount = (float) list.Count;
                var d = list.FindAll(s => s[i] == '1');

                if (d.Count >= dataCount - d.Count ) {
                    list.RemoveAll(s => s[i] == '0');
                }
                else {
                    list.RemoveAll(s => s[i] == '1');
                }
                
                if (list.Count == 1) {
                    break;
                } 
            }

            return Convert.ToInt32(list[0], 2);
        }
    }
}