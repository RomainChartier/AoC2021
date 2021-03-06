using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2021.Cli
{
    public class Day03 : IDay
    {
        private readonly string[] values;

        public Day03() : this(File.ReadLines(@".\Data\day03.txt").ToArray()) { }
        public Day03(string[] values) { this.values = values; }

        public object Part1()
        {
            int bitCount = values[0].Length;
            var acc = new int[bitCount];

            foreach(var value in values)
            {
                for(var i = 0; i < bitCount; i++)
                {
                    if(value[i] == '0')
                    {
                        acc[i] -= 1;
                    }
                    else
                    {
                        acc[i] += 1;
                    }
                }
            }

            var gamma = 0;
            for (var i = 0; i < bitCount; i++)
            {
                if (acc[i] > 0)
                {
                    gamma = (gamma << 1) + 1;
                }
                else if (acc[i] < 0)
                {
                    gamma = gamma << 1;
                }
                else
                {
                    throw new Exception();
                }
            }

            var mask = 1;
            for(var i = 0; i < bitCount - 1; i++)
            {
                mask = mask << 1;
                mask += 1;
            }

            var epsilon = ~gamma & mask;
            return gamma * epsilon;
        }

        public object Part2()
        {
            var oxygen = GetOxygen();
            var co2 = GetCo2();

            return oxygen * co2;
        }

        private int GetCo2() 
            => Filter(x =>
                x.OrderBy(g => g.Key) //keep 0 in case of equality for co2
                .MinBy(g => g.Count()));

        private int GetOxygen()
            => Filter(x =>
                x.OrderByDescending(g => g.Key) //keep 1 in case of equality for oxygen
                .MaxBy(g => g.Count()));

        private int Filter(Func<IEnumerable<IGrouping<char, string>>, IEnumerable<string>> thunk)
        {
            int bitCount = values[0].Length;
            var shortList = values;

            for (var i = 0; i < bitCount; i++)
            {
                shortList = thunk(shortList
                    .GroupBy(x => x[i]))
                    .ToArray();

                if (shortList.Length == 1)
                {
                    return Convert.ToInt32(shortList[0], 2);
                }
            }

            throw new Exception("Shouldn't happen");
        }
    }
}
