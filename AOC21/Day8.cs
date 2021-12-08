using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC21 {
    internal class Digit {
        public string patternString = "";
        private int Value = -1;

        public Digit(string patternString) {
            SetPatternString(patternString);
        }

        public void SetValue(int v) {
            if (Value != -1) {
                Console.WriteLine($"Value is: {Value} Changing to {v}");
            }

            Value = v;
        }

        public int GetValue() => Value;

        private void SetPatternString(string s) {
            patternString = s;
            Value = s.Length switch {
                2 => 1,
                3 => 7,
                4 => 4,
                7 => 8,
                _ => Value
            };
        }

        public string OrderedString() {
            return string.Concat(patternString.OrderBy(c => c));
        }
    }

    internal class SignalEntry {
        private List<Digit> pattern = new();
        private readonly List<string> output;
        private List<Digit> PatternDigits;
        private readonly char cSegment;
        private readonly string fSegment;
        public int SignalSum;

        public SignalEntry(string s) {
            var p = s.Split("|")[0].Trim().Split(" ");
            output = new List<string>(s.Split("|")[1].Trim().Split(" "));

            foreach (var patternString in p) {
                pattern.Add(new Digit(patternString));
            }

            foreach (var possibleSixes in pattern.Where(d => d.GetValue() == -1 && d.patternString.Length == 6)) {
                foreach (var c in pattern.First(v => v.GetValue() == 1).patternString
                    .Where(c => !possibleSixes.patternString.Contains(c))) {
                    possibleSixes.SetValue(6);
                    cSegment = c;
                }
            }

            fSegment = pattern.First(d => d.GetValue() == 1).patternString.Trim(cSegment);

            foreach (var possibleZeros in pattern.Where(d => d.GetValue() == -1 && d.patternString.Length == 6)) {
                foreach (var c in pattern.First(v => v.GetValue() == 4).patternString
                    .Where(c => !possibleZeros.patternString.Contains(c))) {
                    possibleZeros.SetValue(0);
                }
            }

            foreach (var nines in pattern.Where(d => d.GetValue() == -1 && d.patternString.Length == 6)) {
                nines.SetValue(9);
            }

            foreach (var fives in pattern.Where(d => d.GetValue() == -1 && d.patternString.Length == 5)) {
                if (!fives.patternString.Contains(cSegment)) {
                    fives.SetValue(5);
                }
            }

            foreach (var twos in pattern.Where(d => d.GetValue() == -1 && d.patternString.Length == 5)) {
                if (!twos.patternString.Contains(fSegment)) {
                    twos.SetValue(2);
                }
            }

            foreach (var three in pattern.Where(d => d.GetValue() == -1)) {
                three.SetValue(3);
            }

            SignalSum = GetOutput();
        }

        private int GetOutput() {
            var sum = 0;
            var sumstring = "";

            foreach (var p in output.Select(number => String.Concat(number.OrderBy(c => c)))
                .SelectMany(s => pattern.Where(p => p.OrderedString() == s))) {
                Console.Write(p.GetValue());
                sumstring += p.GetValue().ToString();
            }

            Console.WriteLine();

            return int.Parse(sumstring);
        }
    }

    internal class Day8 {
        private List<SignalEntry> Signals = new();
        private readonly List<string> _data;
        private int sum;

        public Day8(string[] data) {
            _data = data.ToList();
        }

        public void SetSignalPatterns() {
            foreach (var pattern in _data.Select(d => new SignalEntry(d))) {
                sum += pattern.SignalSum;
                Signals.Add(pattern);
            }

            Console.WriteLine(sum);
            Console.WriteLine("end");
        }
    }
}