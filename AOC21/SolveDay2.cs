using System;

namespace AOC21 {
    internal class Submarine {
        public int y = 0;
        public int x = 0;
        public int a = 0;

        public void ChangeX(int xChange) {
            Console.WriteLine(xChange);
            Console.WriteLine(x);
            x += xChange;
        }

        public void ChangeY(int yChange) {
            Console.WriteLine(yChange);
            Console.WriteLine(y);
            y += yChange;
        }

        public void ChangeAim(int aChange) {
            Console.WriteLine($"Aim change: {aChange}");
            a += aChange;
            Console.WriteLine($"A is: { a}");
            Console.WriteLine();
        }

        public void ChangeAX(int size) {
            x += size;
            y += a * size;
            Console.WriteLine($"change size {size} x: {x} a: {a} y: {a * size} ans: {x * y}");
            Console.WriteLine();
        }
    }

    internal class SolveDay2 {
        private Submarine s = new Submarine();

        public void Solve1(string[] c) {
            foreach (var change in c) {
                var line = change.Split(" ");
                var order = line[0];
                var size = int.Parse(line[1]);

                switch (order) {
                    case "forward":
                        s.ChangeX(size);

                        break;
                    case "down":
                        s.ChangeY(size);

                        break;
                    default:
                        s.ChangeY(size * -1);

                        break;
                }
            }

            Console.WriteLine(s.x * s.y);
        }

        public void Solve2(string[] instructions) {
            foreach (var change in instructions) {
                var line = change.Split(" ");
                var order = line[0];
                var size = int.Parse(line[1]);

                switch (order) {
                    case "forward":
                        s.ChangeAX(size);

                        break;
                    case "down":
                        s.ChangeAim(size);

                        break;
                    default:
                        s.ChangeAim(size * -1);

                        break;
                }
            }

            // Console.WriteLine($"y: {s.y} x: {s.x} z: {s.a} ans: {s.x * s.a}");
            Console.WriteLine($"ans: {s.x * s.y}");
        }
    }
}