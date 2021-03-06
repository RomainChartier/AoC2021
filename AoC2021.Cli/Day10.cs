using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2021.Cli
{
    public class Day10 : IDay
    {
        private readonly string[] values;

        public Day10() : this(File.ReadLines(@".\Data\day10.txt").ToArray()) { }
        public Day10(string[] values) { this.values = values; }

        public object Part1() 
            => values
                .Select(GetCorruptedScore)
                .Where(x => x.HasValue)
                .Sum();

        public object Part2() 
            => Median(values
                .Where(x => !GetCorruptedScore(x).HasValue)
                .Select(GetCompletionScore));

        private static readonly HashSet<char> openings = new HashSet<char> { '(', '[', '{', '<', };

        private long GetCompletionScore(string line)
        {
            var stack = new Stack<char>();
            foreach(var c in line)
            {
                if (openings.Contains(c))
                {
                    stack.Push(c);
                    continue;
                }

                stack.Pop();
            }

            return stack.Aggregate(0L, (acc, c) => acc * 5 + (c switch
            {
                '(' => 1,
                '[' => 2,
                '{' => 3,
                '<' => 4,
                _ => throw new Exception()
            }));
        }

        private int? GetCorruptedScore(string line)
        {
            var stack = new Stack<char>();
            foreach(var c in line)
            {
                if (openings.Contains(c))
                {
                    stack.Push(c);
                    continue;
                }

                var popped = stack.Pop();
                if(!IsPair(popped, c))
                {
                    return c switch
                    {
                        ')' => 3,
                        ']' => 57,
                        '}' => 1197,
                        '>' => 25137,
                        _ => null
                    };
                }
            }

            return null;
        }

        private static bool IsPair(char opening, char ending)
            => opening switch
            {
                '(' => ending == ')',
                '[' => ending == ']',
                '{' => ending == '}',
                '<' => ending == '>',
                _ => false
            };

        private static long Median(IEnumerable<long> values)
        {
            var sorted = values.OrderBy(x => x).ToArray();
            var mid = (sorted.Length - 1) / 2;
            return sorted[mid];
        }
    }
}
