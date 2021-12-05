using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

            // var d5 = new Day5(content);
            // d5.SetLines();
            // d5.SetGrid();
            // d5.FindMultiMarked();
namespace AOC21 {
    internal class Line {
        public Point Start { get; set; }
        public Point Stop { get; set; }
        public bool IsHoriZontalOrVertical;
        public List<Point> Points = new();

        public void SetIsHorizontalOrVertical() {
            if (Start.X == Stop.X || Start.Y == Stop.Y) {
                IsHoriZontalOrVertical = true;
            }
        }

        public void SetLinePoints() {
            int startLine;
            int stopLine;
            
            if (Start.X == Stop.X) {
                startLine = Start.Y <= Stop.Y ? Start.Y : Stop.Y;
                stopLine = Start.Y > Stop.Y ? Start.Y : Stop.Y;

                do {
                    Points.Add(new Point(Start.X, startLine));
                    startLine++;
                } while (startLine <= stopLine); 
                
            }
            else if( Start.Y == Stop.Y) {
                startLine = Start.X <= Stop.X ? Start.X : Stop.X;
                stopLine = Start.X > Stop.X ? Start.X : Stop.X;

                do {
                    Points.Add(new Point(startLine, Start.Y));
                    startLine++;
                } while (startLine <= stopLine); 
                
            }

            // Console.WriteLine($"Start: {Start.X} {Start.Y} stop: {Stop.X} {Stop.Y}");
            //
            // foreach (var point in Points) {
            //     Console.Write($"Point: {point.X}:{point.Y} ");
            // }
            // Console.WriteLine();
        }

        public void SetDiagonalLinePoints() {
            var xChange = 0;
            var yChange = 0;
            var diff = Start.X - Stop.X;

            if (diff < 0) {
                diff = diff * -1;
            } 

            if (Start.X - Stop.X <= 0) {
                xChange = 1;
            }
            else {
                xChange = -1;
            }

            if (Start.Y - Stop.Y <= 0) {
                yChange = 1;
            }
            else {
                yChange = -1;
            }
            Points.Add(new Point(Start.X, Start.Y));
            do {
                var p = new Point(Points.Last().X + xChange, Points.Last().Y + yChange);
                Points.Add(p);
                diff--;
            } while (diff!=0);

            foreach (var point in Points) {
                Console.WriteLine($"x: {point.X} y:{point.Y}");
            }
        }
    }

    internal class Day5 {
        private List<string> data;
        private List<Line> Lines = new();
        private List<List<int>> Grid;

        public Day5(string[] content) {
            data = content.ToList();
        }

        public void SetLines() {
            foreach (var l in data) {
                var lineParts = l.Split("->");

                var startString = lineParts[0].Split(",");
                var stopString = lineParts[1].Split(",");
                var start = new Point(int.Parse(startString[0].Trim()), int.Parse(startString[1].Trim()));
                var stop = new Point(int.Parse(stopString[0].Trim()), int.Parse(stopString[1].Trim()));
                var line = new Line { Start = start, Stop = stop };
                line.SetIsHorizontalOrVertical();

                if (line.IsHoriZontalOrVertical) {
                    line.SetLinePoints();
                }
                else {
                    line.SetDiagonalLinePoints();
                }

                Lines.Add(line);
            }
        }

        public void SetGrid() {
            SetGridSize();
            MarkGrid();

            // foreach (var line in Lines) {
            //     AddLineToGrid(line);
            // }
            // AddLineToGrid(Lines[3]);
        }

        private void MarkGrid() {
            foreach (var l in Lines) {
                foreach (var p in l.Points) {
                    Grid[p.Y][p.X]++;
                }
            }
        }

        private void AddLineToGrid(Line line) {
            if (line.Start.X == line.Stop.X) {
                var startY = line.Start.Y <= line.Stop.Y ? line.Start.Y : line.Stop.Y;
                var stopY = line.Start.Y > line.Stop.Y ? line.Stop.Y : line.Start.Y;

                do {
                    Console.Write($"start{startY}");
                    startY++;

                    Console.Write($"neverDone{startY}");
                } while (startY != stopY);
            }
        }

        private void SetGridSize() {
            // var xMin = FindMin(0);
            // var yMin = FindMin(1);
            var xMax = FindMax(0);
            var yMax = FindMax(1);
            var tmplListList = new List<List<int>>();

            for (int i = 0; i < yMax + 1; i++) {
                tmplListList.Add(new List<int>(new int[xMax + 1]));
            }

            Grid = tmplListList;
        }

        private int FindMax(int p0) {
            var max = 0;

            foreach (var line in Lines) {
                if (p0 == 0) {
                    if (line.Start.X > max) {
                        max = line.Start.X;
                    }

                    if (line.Stop.X > max) {
                        max = line.Stop.X;
                    }
                }
                else {
                    if (line.Start.X > max) {
                        max = line.Start.X;
                    }

                    if (line.Stop.X > max) {
                        max = line.Stop.X;
                    }
                }
            }

            return max;
        }

        private int FindMin(int p0) {
            var min = int.MaxValue;

            foreach (var line in Lines) {
                if (p0 == 0) {
                    if (line.Start.X < min) {
                        min = line.Start.X;
                    }

                    if (line.Stop.X < min) {
                        min = line.Stop.X;
                    }
                }
                else {
                    if (line.Start.X < min) {
                        min = line.Start.X;
                    }

                    if (line.Stop.X < min) {
                        min = line.Stop.X;
                    }
                }
            }

            return min;
        }

        public void FindMultiMarked() {
            var marked = 0;

            foreach (var row in Grid) {
                foreach (var r in row) {
                    if (r > 1) {
                        marked++;
                    }
                }
            }

            Console.WriteLine(marked);
        }
    }
}