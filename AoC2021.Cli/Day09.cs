using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2021.Cli
{
    public class Day09 : IDay
    {
        private readonly int[] map;
        private readonly int height;
        private readonly int width;
        public Day09() : this(File.ReadLines(@".\Data\day09.txt").ToArray()) { }
        public Day09(string[] values)
        {
            map = values.SelectMany(l => l.Select(x => int.Parse($"{x}"))).ToArray();
            width = values[0].Length;
            height = values.Length;
        }

        public object Part1()
        {
            var sum = 0;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    int current = map[Index(x, y)];
                    if (GetNeighbors(x, y).All(n => map[Index(n.Item1, n.Item2)] > current))
                    {
                        sum += current + 1;
                    }
                }
            }

            return sum;
        }

        public object Part2()
        {
            var basinMap = new int[map.Length];
            var basinNumber = 1;
            var basinSizes = new List<int>();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var size = PopulateBasins(x, y, basinNumber, basinMap);
                    if (size > 0)
                    {
                        basinSizes.Add(size);
                        basinNumber++;
                    }
                }
            }

            return basinSizes.OrderByDescending(v => v).Take(3).Aggregate((acc, v) => acc * v);
        }

        private int PopulateBasins(int x, int y, int basinNumber, int[] basinMap)
        {
            int index = Index(x, y);
            if (map[index] == 9 || basinMap[index] != 0)
            {
                return 0;
            }

            basinMap[index] = basinNumber;

            var sum = 0;
            foreach (var (nx, ny) in GetNeighbors(x, y))
            {
                sum += PopulateBasins(nx, ny, basinNumber, basinMap);
            }

            return 1 + sum;
        }

        private static readonly (int, int)[] Neighboring = new[]
        {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1),
        };

        private IEnumerable<(int, int)> GetNeighbors(int x, int y)
        {
            foreach (var (sx, sy) in Neighboring)
            {
                int nx = x + sx;
                int ny = y + sy;
                if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                {
                    yield return (nx, ny);
                }
            }
        }

        private int Index(int x, int y) => x + y * width;
    }
}
