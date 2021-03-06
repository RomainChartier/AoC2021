using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2021.Cli
{
    public class Day06 : IDay
    {
        private readonly string[] values;

        public Day06() : this(File.ReadLines(@".\Data\day06.txt").ToArray()) { }
        public Day06(string[] values) { this.values = values; }

        public object Part1()
            => GrowFishesEfficient(values, 80);

        public object Part2()
            => GrowFishesEfficient(values, 256);

        public static long GrowFishesEfficient(string[] values, int dayCount)
        {
            var fishes = values[0].Split(',')
                .Select(int.Parse)
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => (long)x.Count());

            var nextFishes = new Dictionary<int, long>();
            for (int day = 0; day < dayCount; day++)
            {
                foreach (var fish in fishes)
                {
                    if (fish.Key == 0)
                    {
                        AddOrUpdate(nextFishes, 6, fish.Value);
                        AddOrUpdate(nextFishes, 8, fish.Value);
                    }
                    else
                    {
                        AddOrUpdate(nextFishes, fish.Key - 1, fish.Value);
                    }
                }

                fishes = nextFishes;
                nextFishes = new Dictionary<int, long>();
            }

            return fishes.Sum(x => x.Value);
        }

        private static void AddOrUpdate(Dictionary<int, long> dic, int key, long value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = dic[key] + value;
            }
            else
            {
                dic.Add(key, value);
            }
        }

        public static int GrowFishesNaive(string[] values, int dayCount)
        {
            var fishes = values[0].Split(',').Select(int.Parse).ToList();
            var nextFishes = new List<int>();

            for (int day = 0; day < dayCount; day++)
            {
                foreach (var fish in fishes)
                {
                    if (fish == 0)
                    {
                        nextFishes.Add(6);
                        nextFishes.Add(8);
                    }
                    else
                    {
                        nextFishes.Add(fish - 1);
                    }
                }

                fishes = nextFishes;
                nextFishes = new List<int>();
            }

            return fishes.Count;
        }
    }
}
