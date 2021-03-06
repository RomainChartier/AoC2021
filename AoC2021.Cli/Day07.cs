using System;
using System.IO;
using System.Linq;

namespace AoC2021.Cli
{
    public class Day07 : IDay
    {
        private readonly int[] values;

        public Day07() : this(File.ReadLines(@".\Data\day07.txt").ToArray()) { }
        public Day07(string[] values) { this.values = values[0].Split(',').Select(int.Parse).ToArray(); }

        public object Part1()
        {
            var dest = Median(values);
            return values.Select(crabX => Math.Abs(crabX - dest)).Sum();
        }

        public object Part2()
        {
            var avg = values.Average();
            return Math.Min(
                CalculateTotalFuelPart2((int)Math.Ceiling(avg)),
                CalculateTotalFuelPart2((int)Math.Floor(avg)));
        }

        private int CalculateTotalFuelPart2(int dest) 
            => values.Select(crabX =>
            {
                var dist = Math.Abs(crabX - dest);
                return dist * (dist + 1) / 2;
            }).Sum();

        private static int Median(int[] values)
        {
            var sorted = values.OrderBy(x => x).ToArray();
            var mid = (sorted.Length - 1) / 2;
            return sorted[mid];
        }
    }
}
