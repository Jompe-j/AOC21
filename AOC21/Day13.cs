using System;
using System.Collections.Generic;
using System.Drawing;

namespace AOC21 {
    internal class Day13 {
        private HashSet<Point> _coordinates = new();
        private readonly List<List<string>> _instructions = new();
        private int _maxX;
        private int _maxY;

        public Day13(string[] data) {
            SetCoordinates(data);
            SetInstructions(data);
            Fold();

            var testcoordinate = new HashSet<Point> { new(6, 10), new(6, 11) };
            Print();
            Console.WriteLine("Done");
        }

        private void Print() {
            var column = new List<List<string>>();
            var printX = 0;
            var printY = 0;
            
            
            foreach (var coordinate in _coordinates) {
                if (coordinate.X > printX) {
                    printX = coordinate.X;
                }
                if (coordinate.Y > printY) {
                    printY = coordinate.Y;
                }
            }
            
            for (var i = 0; i <= printY; i++) {

                for (var j = 0; j <= printX; j++) {
                    var printed = false;
                    foreach (var coordinate in _coordinates) {
                        if (coordinate.X == j && coordinate.Y == i) {
                            Console.Write("#");
                            printed = true;
                        }
                    }

                    if (!printed) {
                        Console.Write("."); 
                    }
                    
                }
                Console.WriteLine();
            }
           
            
            
        }

        private void Fold() {
            foreach (var instruction in _instructions) {
                if (instruction[0] == "y") {
                    YFold(instruction[1]);
                }
                else {
                    XFold(instruction[1]);
                }
            }
            
        }

        private void XFold(string s) {
            var foldRow = int.Parse(s);
            var tmpSet = new HashSet<Point>();

            foreach (var coordinate in _coordinates) {
                var tmpCord = new Point(coordinate.X, coordinate.Y);

                if (tmpCord.X > foldRow) {
                    tmpCord.X -= (tmpCord.X - foldRow) * 2;
                }

                tmpSet.Add(tmpCord);
            }

            _coordinates = new HashSet<Point>(tmpSet);
        }

        private void YFold(string s) {
            var foldRow = int.Parse(s);
            var tmpSet = new HashSet<Point>();

            foreach (var coordinate in _coordinates) {
                var tmpCord = new Point(coordinate.X, coordinate.Y);

                if (tmpCord.Y > foldRow) {
                    tmpCord.Y -= (tmpCord.Y - foldRow) * 2;
                }

                tmpSet.Add(tmpCord);
            }

            _coordinates = new HashSet<Point>(tmpSet);
        }

        private void SetCoordinates(string[] data) {
            foreach (var row in data) {
                if (row.Length == 0) {
                    return;
                }

                var xy = row.Split(",");
                var x = int.Parse(xy[0]);
                var y = int.Parse(xy[1]);

                // Set MaxX
                if (x > _maxX) {
                    _maxX = x;
                }

                // Set MaxY
                if (y > _maxY) {
                    _maxY = y;
                }

                _coordinates.Add(new Point(x, y));
            }
        }

        private void SetInstructions(string[] data) {
            for (var i = _coordinates.Count + 1; i < data.Length; i++) {
                var xyAmount = data[i].Split("=");
                var directionAndSize = new List<string> { xyAmount[0][xyAmount[0].Length - 1].ToString(), xyAmount[1] };
                _instructions.Add(directionAndSize);
            }
        }
    }
}