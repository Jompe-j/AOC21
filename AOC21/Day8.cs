using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC21 {
    internal class Digit {
        public string PatternString = "";
        private int _value = -1;

        public Digit(string patternString) {
            SetPatternString(patternString);
        }

        public void SetValue(int v) {
            if (_value != -1) {
                Console.WriteLine($"Value is: {_value} Changing to {v}");
            }

            _value = v;
        }

        public int GetValue() => _value;

        private void SetPatternString(string s) {
            PatternString = s;
            _value = s.Length switch {
                2 => 1,
                3 => 7,
                4 => 4,
                7 => 8,
                _ => _value
            };
        }

        public string OrderedString() {
            return string.Concat(PatternString.OrderBy(c => c));
        }
    }

    internal class SignalEntry {
        private readonly List<Digit> _pattern = new();
        private readonly List<string> _output;
        private readonly char _cSegment;
        public readonly int SignalSum;

        public SignalEntry(string s) {
            var p = s.Split("|")[0].Trim().Split(" ");
            _output = new List<string>(s.Split("|")[1].Trim().Split(" "));

            foreach (var patternString in p) {
                _pattern.Add(new Digit(patternString));
            }

            foreach (var possibleSixes in _pattern.Where(d => d.GetValue() == -1 && d.PatternString.Length == 6)) {
                foreach (var c in _pattern.First(v => v.GetValue() == 1).PatternString
                    .Where(c => !possibleSixes.PatternString.Contains(c))) {
                    possibleSixes.SetValue(6);
                    _cSegment = c;
                }
            }

            foreach (var possibleZeros in _pattern.Where(d => d.GetValue() == -1 && d.PatternString.Length == 6)) {
                foreach (var unused in _pattern.First(v => v.GetValue() == 4).PatternString.Where(c => !possibleZeros.PatternString.Contains(c))) {
                    possibleZeros.SetValue(0);
                }
            }

            foreach (var nines in _pattern.Where(d => d.GetValue() == -1 && d.PatternString.Length == 6)) {
                nines.SetValue(9);
            }

            foreach (var fives in _pattern.Where(d => d.GetValue() == -1 && d.PatternString.Length == 5)) {
                if (!fives.PatternString.Contains(_cSegment)) {
                    fives.SetValue(5);
                }
            }

            foreach (var twos in _pattern.Where(d => d.GetValue() == -1 && d.PatternString.Length == 5)) {
                if (!twos.PatternString.Contains(_pattern.First(d => d.GetValue() == 1).PatternString.Trim(_cSegment))) {
                    twos.SetValue(2);
                }
            }

            foreach (var three in _pattern.Where(d => d.GetValue() == -1)) {
                three.SetValue(3);
            }

            SignalSum = GetOutput();
        }

        private int GetOutput() {
            var sumstring = "";

            foreach (var p in _output.Select(number => string.Concat(number.OrderBy(c => c)))
                .SelectMany(s => _pattern.Where(p => p.OrderedString() == s))) {
                Console.Write(p.GetValue());
                sumstring += p.GetValue().ToString();
            }

            Console.WriteLine();

            return int.Parse(sumstring);
        }
    }

    internal class Day8 {
        private readonly List<SignalEntry> _signals = new();
        private readonly List<string> _data;
        private int _sum;

        public Day8(IEnumerable<string> data) {
            _data = data.ToList();
        }

        public void SetSignalPatterns() {
            foreach (var pattern in _data.Select(d => new SignalEntry(d))) {
                _sum += pattern.SignalSum;
                _signals.Add(pattern);
            }

            Console.WriteLine(_sum);
            Console.WriteLine("end");
        }
    }
}