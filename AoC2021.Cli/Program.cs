using System;

namespace AoC2021.Cli
{

    class Program
    {
        static void Main(string[] args)
        {
            PrintDay<Day01>();
        }

        private static void PrintDay<T>() where T : IDay, new()
        {
            var day = new T();
            Console.WriteLine($"{typeof(T).Name} part1: " + day.Part1());
            Console.WriteLine($"{typeof(T).Name} part2: " + day.Part2());
        }
    }
}
