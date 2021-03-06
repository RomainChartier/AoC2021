using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AoC2021.Cli
{
    public class Day08 : IDay
    {
        private readonly string[] values;

        public Day08() : this(File.ReadLines(@".\Data\day08.txt").ToArray()) { }
        public Day08(string[] values) { this.values = values; }


        public readonly record struct SignalPattern(Signals Value);
        public readonly record struct Output(Signals Value);
        public readonly record struct Entry(SignalPattern[] Patterns, Output[] Outputs);

        [Flags]
        public enum Segments
        {
            None = 0,
            Top = 0x01,
            TopLeft = 0x02,
            TopRight = 0x04,
            Mid = 0x08,
            BottomLeft = 0x10,
            BottomRight = 0x20,
            Bottom = 0x40
        }
        
        [Flags]
        public enum Signals
        {
            None = 0,
            A = 0x01,
            B = 0x02,
            C = 0x04,
            D = 0x08,
            E = 0x10,
            F = 0x20,
            G = 0x40
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
            var signalMapping = new Dictionary<Segments, Signals>(7);
            var digitMapping = entry.Patterns.Select(x => (value: x, digit: TryGuessEasyNumber(x.Value))).Where(x => x.digit.HasValue).ToDictionary(x => x.digit.Value, x => x.value.Value);

            // 3 is the only of length 5 that contains both 1 segment
            AddPatternOf(3, 5, x => x.Value.HasSignal(digitMapping[1]));

            signalMapping[Segments.Top] = digitMapping[7].Except(digitMapping[1]).Single();
            signalMapping[Segments.TopLeft] = digitMapping[4].Except(digitMapping[3]).Single();
            signalMapping[Segments.Bottom] = digitMapping[3]
                .Except(digitMapping[4])
                .Except(digitMapping[7])
                .Single();

            // 5 is the only of length 5 that contains the topleft segment
            AddPatternOf(5, 5, x => x.Value.HasSignal(signalMapping[Segments.TopLeft]));
            AddPatternOf(2, 5, x => x.Value != digitMapping[3] && x.Value != digitMapping[5]);

            signalMapping[Segments.TopRight] = digitMapping[1].Except(digitMapping[5]).Single();
            signalMapping[Segments.BottomRight] = digitMapping[1].Except(signalMapping[Segments.TopRight]).Single();
            signalMapping[Segments.BottomLeft] = digitMapping[2]
                .Except(digitMapping[5])
                .Except(digitMapping[1])
                .Single();

            signalMapping[Segments.Mid] = digitMapping[8].Except(signalMapping.Values.Select(x => x)).Single();

            // 0 is the only of length 6 that don't contains mid segment
            AddPatternOf(0, 6, x => !x.Value.HasSignal(signalMapping[Segments.Mid]));
            AddPatternOf(9, 6, x => !x.Value.HasSignal(signalMapping[Segments.BottomLeft]));
            AddPatternOf(6, 6, x => !x.Value.HasSignal(signalMapping[Segments.TopRight]));

            var patternMapping = digitMapping.ToDictionary(x => x.Value, x => x.Key);

            return patternMapping[entry.Outputs[0].Value] * 1000
                + patternMapping[entry.Outputs[1].Value] * 100
                + patternMapping[entry.Outputs[2].Value] * 10
                + patternMapping[entry.Outputs[3].Value];


            void AddPatternOf(byte digit, int length, Func<SignalPattern, bool> filter)
            {
                var p = entry.Patterns
                    .Where(x => x.Value.GetBitCount() == length)
                    .Where(filter)
                    .Single();

                digitMapping.Add(digit, p.Value);
            }
        }

        private Entry ParseEntry(string line)
        {
            var tokens = line.Split('|');
            return new Entry(
                tokens[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => new SignalPattern(ParseSignals(x))).ToArray(),
                tokens[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => new Output(ParseSignals(x))).ToArray());
        }

        private static Signals ParseSignals(string value)
        {
            var result = Signals.None;
            foreach(var c in value)
            {
                var newBit = c switch
                {
                    'a' => Signals.A,
                    'b' => Signals.B,
                    'c' => Signals.C,
                    'd' => Signals.D,
                    'e' => Signals.E,
                    'f' => Signals.F,
                    'g' => Signals.G,
                    _ => throw new NotImplementedException()
                };

                result |= newBit;
            }
            return result;
        }

        private static byte? TryGuessEasyNumber(Signals x)
        {
            return x.GetBitCount() switch
            {
                2 => 1,
                4 => 4,
                3 => 7,
                7 => 8,
                _ => null
            };
        }
    }

    public static class SignalsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte GetBitCount(this Day08.Signals s) 
            => (byte)System.Runtime.Intrinsics.X86.Popcnt.PopCount((uint)s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasSignal(this Day08.Signals s, Day08.Signals v) 
            => s.HasFlag(v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Day08.Signals Except(this Day08.Signals s, Day08.Signals v) 
            => s & (~v);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Day08.Signals Except(this Day08.Signals s, IEnumerable<Day08.Signals> values)
        {
            var result = s;
            foreach(var v in values)
            {
                result &= (~v);
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Day08.Signals Single(this Day08.Signals s)
        {
            if(s.GetBitCount() != 1) {  throw new Exception($"too much bits set: {s.GetBitCount()}"); }
            return s;
        }
    }
}
