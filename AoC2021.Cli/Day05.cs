using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2021.Cli
{
    public class Day05 : IDay
    {
        private readonly string[] values;

        public Day05() : this(File.ReadLines(@".\Data\day05.txt").ToArray()) { }
        public Day05(string[] values) { this.values = values; }


        public readonly record struct Pos(int X, int Y)
        {
            public static Pos Parse(string v)
            {
                var coords = v.Split(",");
                return new Pos(int.Parse(coords[0]), int.Parse(coords[1]));
            }
        };

        public readonly record struct Line(Pos Start, Pos End)
        {
            public static Line Parse(string v)
            {
                var points = v.Split("->");
                return new Line(Pos.Parse(points[0]), Pos.Parse(points[1])); ;
            }
        };

        public object Part1()
        {
            var lines = values
                .Select(Line.Parse)
                .Where(l => l.Start.X == l.End.X || l.Start.Y == l.End.Y);

            return GetOverlapCount(lines);
        }

        public object Part2()
        {
            var lines = values
                .Select(Line.Parse);

            return GetOverlapCount(lines);
        }

        private static int GetOverlapCount(IEnumerable<Line> lines) 
            => lines
                .SelectMany(EnumeratePos)
                .GroupBy(x => x)
                .Where(g => g.Count() >= 2)
                .Count();

        private static IEnumerable<Pos> EnumeratePos(Line l)
        {
            var xIncrement = l.Start.X < l.End.X ? 1 
                : l.Start.X > l.End.X ? -1 : 0;
            
            var yIncrement = l.Start.Y < l.End.Y ? 1 
                : l.Start.Y > l.End.Y ? -1 : 0;

            var x = l.Start.X;
            var y = l.Start.Y;

            while(x != l.End.X || y != l.End.Y)
            {
                yield return new Pos(x, y);
                x += xIncrement;
                y += yIncrement;
            }

            yield return new Pos(x, y);            
        }
    }
}
