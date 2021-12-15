using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AOC21 {
    internal class Octopus {
        public bool newlyExploded = false;
        public int value;
        public int xPosition;
        public int yPosition;
        public List<Octopus> Neighbours = new();

        public Octopus(int val, int x, int y) {
            value = val;
            xPosition = x;
            yPosition = y;
        }

        public void SetNeighbours(int xLength, int yLength, List<List<Octopus>> octupi) {
            // (-1, -1), (0, -1), (+1, -1)
            // (-1, 0), (this!), (+1, 0)
            // (-1, +1), (0, +1), (+1, +1)
            var nB = new List<Point> {
                new(-1, -1), new(0, -1), new(1, -1),
                new(-1, 0), new(1, 0),
                new(-1, 1), new(0, 1), new(1, 1)
            };

            foreach (var p in nB) {
                if (xPosition + p.X < 0 || xPosition + p.X > xLength - 1 || yPosition + p.Y < 0 ||
                    yPosition + p.Y > yLength - 1) {
                    continue;
                }

                Neighbours.Add(octupi[yPosition + p.Y][xPosition + p.X]);
            }
        }

        public void Explode() {
            if (value <= 9 || newlyExploded) return;
            newlyExploded = true;

            foreach (var nB in Neighbours) {
                nB.value += 1;
                nB.Explode();
            }
        }

        public int Reset() {
            if (!newlyExploded) return 0;
            value = 0;
            newlyExploded = false;

            return 1;
        }
    }

    internal class Day11 {
        private List<List<Octopus>> octupi = new();

        public Day11(string[] data) {
            var count = 0;

            for (var y = 0; y < data.Length; y++) {
                var row = data[y];
                var lst = row.Select((o, x) => new Octopus(int.Parse((string)o.ToString()), x, y)).ToList();

                octupi.Add(lst);
            }

            foreach (var row in octupi) {
                foreach (var octopus in row) {
                    octopus.SetNeighbours(row.Count, octupi.Count, octupi);
                }
            }

            for (var x = 0; x < 1000; x++) {
                foreach (var octopus in octupi.SelectMany(row => row)) {
                    octopus.value += 1;
                }

                foreach (var octopus in octupi.SelectMany(row => row)) {
                    octopus.Explode();
                }

                var tmpCount = octupi.SelectMany(row => row).Sum(octopus => octopus.Reset());

                count += tmpCount;

                if (tmpCount == 100) {
                    Console.WriteLine($"Synced at {x + 1}");

                    break;
                }
            }

            Console.WriteLine(count);
        }
    }
}