using System;
using System.IO;
using System.Linq;

namespace AoC2021.Cli
{
    public class Day02 : IDay
    {
        private readonly string[] values;

        public Day02() : this(File.ReadLines(@".\Data\day02.txt").ToArray()) { }
        public Day02(string[] values) { this.values = values; }

        public object Part1()
        {
            var (horiz, depth) = values.Select(Command.Parse)
                .Aggregate((horiz: 0, depth: 0),
                    (acc, command) => command.Direction switch
                    {
                        Direction.Up => (acc.horiz, acc.depth - command.Value),
                        Direction.Down => (acc.horiz, acc.depth + command.Value),
                        Direction.Forward => (acc.horiz + command.Value, acc.depth),
                        _ => throw new Exception($"Unknown direction {command.Direction}")
                    });

            return horiz * depth;
        }

        public object Part2()
        {
            var (aim, horiz, depth) = values.Select(Command.Parse)
               .Aggregate((aim: 0, horiz: 0, depth: 0),
                   (acc, command) => command.Direction switch
                   {
                       Direction.Up => (acc.aim - command.Value, acc.horiz, acc.depth),
                       Direction.Down => (acc.aim + command.Value, acc.horiz, acc.depth),
                       Direction.Forward => (acc.aim, acc.horiz + command.Value, acc.depth + acc.aim * command.Value),
                       _ => throw new Exception($"Unknown direction {command.Direction}")
                   });

            return horiz * depth;
        }

        public enum Direction { Forward, Up, Down };
        public readonly record struct Command(Direction Direction, int Value)
        {
            public static Command Parse(string line)
            {
                var tokens = line.Split(" ");
                var value = int.Parse(tokens[1]);
                return tokens[0] switch
                {
                    "forward" => new(Direction.Forward, value),
                    "up" => new(Direction.Up, value),
                    "down" => new(Direction.Down, value),
                    _ => throw new Exception($"Unknown direction {tokens[0]}")
                };
            }
        };
    }
}
