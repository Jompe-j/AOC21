using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC21 {
    internal class Board {
        private bool won; 
        public int index;
        public List<List<(string, bool)>> Rows { get; set; } = new();
        public int WinningNumber { get; set; }

        public Board(List<string> boardLines, int i) {
            index = i;
            foreach (var line in boardLines) {
                
                var numbers = line.Split(" ");
                var l = new List<ValueTuple<string, bool>>();

                foreach (var number in numbers) {
                    if (number != "") {
                        l.Add((number, false));
                    }
                }

                Rows.Add(l);
            }
        }

        public bool CheckForBingo() {
            if (won) {
                return false;
            }
            var rowBingo = new List<bool> { true, true, true, true, true };
            var columnBingo = new List<bool> { true, true, true, true, true };

            for (int i = 0; i < Rows.Count; i++) {
                for (int j = 0; j < Rows[i].Count; j++) {
                    if (Rows[i][j].Item2 == false) {
                        rowBingo[i] = false;
                    }

                    // Todo How to handle rowbingo? Outside of this loop?
                    if (Rows[i][j].Item2 == false) {
                        columnBingo[j] = false;
                    }
                }
            }

            if (rowBingo.Any(row => row) || columnBingo.Any(column => column)) {
                won = true;
                return true;
            }

            return false;

        }

        public void Mark(string number) {
            if (won) {
                return;
            }
            foreach (var r in Rows) {
                for (var j = 0; j < r.Count; j++) {
                    if (r[j].Item1 == number) {
                        r[j] = (number, true);
                    }
                }
            }

        }

        public int CalculateUnmarked() {
            var sum = 0; 
            foreach (var row in Rows) {
                foreach (var valueTuple in row) {
                    if (!valueTuple.Item2) {
                        sum += int.Parse(valueTuple.Item1);
                    } 
                } 
            }
            Console.WriteLine($"Sum is {sum}");

            return sum;
        }
    }

    internal class Game {
        private List<Board> boards = new();
        private List<string> drawNumbers;
        private List<Board> winningBoards = new();
        private string lastNumber;

        public Game(string[] content) {
            drawNumbers = content[0].Split(",").ToList();

            SetBoards(content);
            // PrintAllBoard();
            Play();

            foreach (var b in winningBoards) {
                Console.WriteLine(
                    $"sum is {b.CalculateUnmarked()} and final score is {b.CalculateUnmarked() * b.WinningNumber}");
            }
        }

        private void Play() {
            foreach (var number in drawNumbers) {
                lastNumber = number;

                if (MarkOnBoards(number)) {
                    return;
                }
            }
        }

        private bool MarkOnBoards(string number) {
            foreach (var board in boards) {
                board.Mark(number);
            }

            foreach (var board in boards) {
                if (board.CheckForBingo()) {
                    board.WinningNumber = int.Parse(number);
                    winningBoards.Add(board);
                }
            }

            // if (winningBoards.Count > 1) {
            //     return true;
            // }

            return false;
        }

        private void PrintAllBoard() {
            foreach (var board in boards) {
                foreach (var row in board.Rows) {
                    foreach (var numbers in row) {
                        Console.Write(numbers.Item1);
                    }
                }
            }
        }

        private void SetBoards(string[] content) {
            var index = 0;

            for (int i = 1; i < content.Length; i++) {
                if (content[i].Length == 0) {
                    i++;
                    var boardLines = new List<string>();

                    for (var j = i; j < i + 5; j++) {
                        boardLines.Add(content[j]);
                    }

                    index++;
                    boards.Add(new Board(boardLines, index - 1));
                }
            }
        }
    }

    internal class SolveDay4Problem1 {
        public SolveDay4Problem1(string[] content) {
            var game = new Game(content);
        }
    }
}