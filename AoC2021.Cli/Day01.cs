using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2021.Cli
{
    public class Day01 : IDay
    {
        private readonly int[] values;

        public Day01() : this(File.ReadLines(@".\Data\day01.txt").ToArray()) { }
        public Day01(string[] values)
        {
            this.values = values
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(int.Parse)
                .ToArray();
        }

        public object Part1() =>
            values.Skip(1)
                .Aggregate((result: 0, previousValue: values[0]),
                    (acc, value) => acc.previousValue < value
                        ? (acc.result + 1, value)
                        : (acc.result, value))
                .result;

        public object Part2()
        {
            var windowsValues = EnumerateWindows(values).Select(v => v.Item1 + v.Item2 + v.Item3).ToArray();
            return windowsValues.Skip(1)
                .Aggregate((result: 0, previousValue: windowsValues[0]),
                    (acc, value) => acc.previousValue < value
                            ? (acc.result + 1, value)
                            : (acc.result, value))
                .result;
        }

        private static IEnumerable<(int, int, int)> EnumerateWindows(int[] values)
        {
            var x = values[0];
            var y = values[1];
            foreach (var z in values.Skip(2))
            {
                yield return (x, y, z);
                x = y;
                y = z;
            }
        }
    }
}
