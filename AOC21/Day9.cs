using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC21 {
    internal class VentPosition {
        private VentPosition aboveVent;
        private VentPosition leftVent;
        private VentPosition rightVent;
        private VentPosition belowVent;

        public VentPosition(int xPosition, int yPosition, int value) {
            XPos = xPosition;
            YPos = yPosition;
            Value = value;
            MyBasin = new List<VentPosition>();
        }

        public VentPosition() {
            MyBasin = new List<VentPosition>();
            Value = int.MaxValue;
        }

        public List<VentPosition> GetBasin(List<List<VentPosition>> grid) {
            var tmpBasins = new List<VentPosition>();
            InBasin = true;
            tmpBasins.Add(this);
            foreach (var vent in MyBasin.Where(vent => !vent.InBasin)) {
                if (!vent.LowPoint) {
                    tmpBasins.AddRange(vent.GetBasin(grid));
                } 
            }

            return tmpBasins;
        }

        public bool InBasin { get; set; }

        public int YPos { get; set; }

        public int XPos { get; set; }

        public int Value { get; set; }
        public VentPosition AboveVent {
            get => aboveVent;
            set {
                if (value.Value < 9 && value.Value > Value) {
                    MyBasin.Add(value);
                }

                aboveVent = value;
            }
        }
        public List<VentPosition> MyBasin { get; set; }
        public VentPosition LeftVent {
            get => leftVent;
            set {
                if (value.Value < 9 && value.Value > Value) {
                    MyBasin.Add(value);
                }

                leftVent = value;
            }
        }
        public VentPosition RightVent {
            get => rightVent;
            set {
                if (value.Value < 9 && value.Value > Value) {
                    MyBasin.Add(value);
                }

                rightVent = value;
            }
        }
        public VentPosition BelowVent {
            get => belowVent;
            set {
                if (value.Value < 9 && value.Value > Value) {
                    MyBasin.Add(value);
                }

                belowVent = value;
            }
        }
        public bool LowPoint => Value < AboveVent.Value && Value < BelowVent.Value && Value < RightVent.Value &&
                                Value < LeftVent.Value;
    }

    internal class Day9 {
        public readonly List<List<VentPosition>> Grid = new();

        public Day9(string[] data) {
            for (var i = 0; i < data.Length; i++) {
                var row = new List<VentPosition>();

                for (var j = 0; j < data[i].Length; j++) {
                    row.Add(new VentPosition(j, i, int.Parse(data[i][j].ToString())));
                }

                Grid.Add(row);
            }
        }

        public void SetNeighboursValue() {
            foreach (var row in Grid) {
                foreach (var vP in row) {
                    vP.AboveVent = GetNeighbour(vP.XPos, vP.YPos + -1);
                    vP.LeftVent = GetNeighbour(vP.XPos + -1, vP.YPos);
                    vP.RightVent = GetNeighbour(vP.XPos + 1, vP.YPos);
                    vP.BelowVent = GetNeighbour(vP.XPos, vP.YPos + 1);
                }
            }
        }

        private VentPosition GetNeighbour(int x, int y) {
            try {
                return Grid[y][x];
            }
            catch (Exception) {
                return new VentPosition();
            }
        }

        public void PrintRiskSum() {
            var sum = 0;
            var iterations = 0;

            foreach (var row in Grid) {
                foreach (var ventPosition in row) {
                    if (ventPosition.LowPoint) {
                        iterations++;
                        sum += ventPosition.Value + 1;
                    }
                }
            }

            Console.WriteLine($"sum is {sum}");
        }

        public void PrintBasinSum() {
            var tmplist = new List<List<VentPosition>>();
            var counter = 0;

            foreach (var row in Grid) {
                foreach (var ventPosition in row) {
                    if (ventPosition.LowPoint) {
                        counter++;
                        tmplist.Add(ventPosition.GetBasin(Grid));
                    }
                }
            }

            var tmp = tmplist.Select(l => l.Count).ToList();
            tmp.Sort();
            tmp.Reverse();

            Console.WriteLine($"Result is : {tmp.Take(3).Aggregate(1, (x, y) => x * y)}");

            foreach (var row in Grid) {
                foreach (var vP in row) {
                    if (!vP.InBasin && vP.Value != 9) {
                        Console.WriteLine($"Value = {vP.Value} x: {vP.XPos} y: {vP.YPos}");
                    }
                }
            }
        }
    }
}