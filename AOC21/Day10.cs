using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC21 {
    internal class Day10 {
        private List<string> clearedLines = new();
        private List<ulong> sumList = new();

        public Day10(string[] data) {
            var sum = data.Sum(line => FindFirst(line));

            Console.WriteLine($"Solution Part1: {sum}");

            foreach (var line in clearedLines) {
                FixIncompleteLines(line);
            }

            sumList.Sort();
            Console.WriteLine($"Solution Part2: {sumList[sumList.Count / 2]}");
        }

        private void FixIncompleteLines(string line) {
            var chunkParts = new Stack<char>();

            foreach (var c in line) {
                switch (c) {
                    case '(' or '[' or '{' or '<':
                        chunkParts.Push(c);

                        break;
                    case ')' or ']' or '}' or '>': {
                        chunkParts.Pop();

                        break;
                    }
                }
            }

            ulong sum = 0;

            foreach (var c in chunkParts) {
                sum *= 5;
                sum += GetValueOfSymbol(GetOpposite(c));
            }

            sumList.Add(sum);
        }

        private ulong GetValueOfSymbol(char c) {
            return c switch {
                ')' => 1,
                ']' => 2,
                '}' => 3,
                '>' => 4,
                _ => 0
            };
        }

        private int FindFirst(string line) {
            var chunkParts = new Stack<char>();

            foreach (var c in line) {
                switch (c) {
                    case '(' or '[' or '{' or '<':
                        chunkParts.Push(c);

                        break;
                    case ')' or ']' or '}' or '>': {
                        var op = GetOpposite(c);

                        if (op != chunkParts.Pop()) {
                            switch (c) {
                                case ')':
                                    return 3;
                                case ']':
                                    return 57;
                                case '}':
                                    return 1197;
                                case '>':
                                    return 25137;
                            }
                        }

                        break;
                    }
                }
            }

            clearedLines.Add(line);

            return 0;
        }

        private static char GetOpposite(char c) {
            return c switch {
                ')' => '(',
                ']' => '[',
                '}' => '{',
                '>' => '<',
                '(' => ')',
                '[' => ']',
                '{' => '}',
                '<' => '>',
                _ => '\0'
            };
        }
    }
}