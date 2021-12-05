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


        public readonly record struct Pos(int X, int Y);
        public readonly record struct Line(Pos Start, Pos End);

        public object Part1()
        {
            var lines = values
                .Select(ParseLine)
                .Where(l => l.Start.X == l.End.X || l.Start.Y == l.End.Y);

            return GetOverlapCount(lines);
        }

        public object Part2()
        {
            var lines = values
                .Select(ParseLine);

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

        private static Line ParseLine(string v)
        {
            var points = v.Split("->");
            return new Line(ParsePos(points[0]), ParsePos(points[1]));
        }

        private static Pos ParsePos(string v)
        {
            var coords = v.Split(",");
            return new Pos(int.Parse(coords[0]), int.Parse(coords[1]));
        }
    }
}
