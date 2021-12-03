namespace AOC21 {
    class SolveDay1 {
        public int Solve1(string[] content) {
            var count = 0;

            for (var i = 3; i < content.Length; i++) {
                var fourth = int.Parse(content[i]);
                var third = int.Parse(content[i - 1]);
                var second = int.Parse(content[i - 2]);
                var first = int.Parse(content[i - 3]);

                var start = first + second + third;
                var stop = second + third + fourth;
                
                
                if (stop > start) {
                    count++;
                } 
            }

            return count;
        }
    }
}