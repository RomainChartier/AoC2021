using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2021.Cli
{
    public class Day08 : IDay
    {
        private readonly string[] values;

        public Day08() : this(File.ReadLines(@".\Data\day08.txt").ToArray()) { }
        public Day08(string[] values) { this.values = values; }


        public readonly record struct SignalPattern(string Value);
        public readonly record struct Output(string Value);
        public readonly record struct Entry(SignalPattern[] Patterns, Output[] Outputs);

        public enum Segment
        {
            Top,
            TopLeft,
            TopRight,
            Mid,
            BottomLeft,
            BottomRight,
            Bottom
        }

        public object Part1()
        {
            var entries = values.Select(ParseEntry).ToArray();
            return entries.SelectMany(x => x.Outputs.Select(y => TryGuessEasyNumber(y.Value))).Where(x => x.HasValue).Count();
        }

        public object Part2()
        {
            var entries = values.Select(ParseEntry).ToArray();
            return entries.Select(Decode).Sum();
        }

        //Pattern length
        // 0 => 6
        // 1 => 2
        // 2 => 5
        // 3 => 5
        // 4 => 4
        // 5 => 5
        // 6 => 6
        // 7 => 3
        // 8 => 7
        // 9 => 6

        // length 5: 2 3 5
        // length 6: 0 6 9
        
        private int Decode(Entry entry)
        {
            var signalMapping = new Dictionary<Segment, char>(7);
            var digitMapping = entry.Patterns.Select(x => (value: x, digit: TryGuessEasyNumber(x.Value))).Where(x => x.digit.HasValue).ToDictionary(x => x.digit.Value, x => x.value.Value);

            // 3 is the only of length 5 that contains both 1 segment
            AddPatternOf(3, 5, x => x.Value.Contains(digitMapping[1][0]) && x.Value.Contains(digitMapping[1][1]));

            signalMapping[Segment.Top] = digitMapping[7].Except(digitMapping[1]).Single();
            signalMapping[Segment.TopLeft] = digitMapping[4].Except(digitMapping[3]).Single();
            signalMapping[Segment.Bottom] = digitMapping[3]
                .Except(digitMapping[4])
                .Except(digitMapping[7])
                .Single();

            // 5 is the only of length 5 that contains the topleft segment
            AddPatternOf(5, 5, x => x.Value.Contains(signalMapping[Segment.TopLeft]));
            AddPatternOf(2, 5, x => x.Value != digitMapping[3] && x.Value != digitMapping[5]);

            signalMapping[Segment.TopRight] = digitMapping[1].Except(digitMapping[5]).Single();
            signalMapping[Segment.BottomRight] = digitMapping[1].Except(new[] { signalMapping[Segment.TopRight] }).Single();
            signalMapping[Segment.BottomLeft] = digitMapping[2]
                .Except(digitMapping[5])
                .Except(digitMapping[1])
                .Single();

            signalMapping[Segment.Mid] = digitMapping[8].Except(signalMapping.Values.Select(x => x)).Single();

            // 0 is the only of length 6 that don't contains mid segment
            AddPatternOf(0, 6, x => !x.Value.Contains(signalMapping[Segment.Mid]));
            AddPatternOf(9, 6, x => !x.Value.Contains(signalMapping[Segment.BottomLeft]));
            AddPatternOf(6, 6, x => !x.Value.Contains(signalMapping[Segment.TopRight]));

            var patternMapping = digitMapping.ToDictionary(x => x.Value, x => x.Key);

            return patternMapping[entry.Outputs[0].Value] * 1000
                + patternMapping[entry.Outputs[1].Value] * 100
                + patternMapping[entry.Outputs[2].Value] * 10
                + patternMapping[entry.Outputs[3].Value];


            void AddPatternOf(byte digit, int length, Func<SignalPattern, bool> filter)
            {
                var p = entry.Patterns
                    .Where(x => x.Value.Length == length)
                    .Where(filter)
                    .Single();

                digitMapping.Add(digit, p.Value);
            }
        }

        private Entry ParseEntry(string line)
        {
            var tokens = line.Split('|');
            return new Entry(
                tokens[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => new SignalPattern(new string(x.OrderBy(x => x).ToArray()))).ToArray(),
                tokens[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => new Output(new string(x.OrderBy(x => x).ToArray()))).ToArray());
        }

        private static byte? TryGuessEasyNumber(string x)
        {
            return x.Length switch
            {
                2 => 1,
                4 => 4,
                3 => 7,
                7 => 8,
                _ => null
            };
        }
    }
}
