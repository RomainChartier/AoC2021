using AoC2021.Cli;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.IO;

namespace AoC2021.Bench
{
    public class DayBench
    {
        private Day01 day;

        public DayBench()
        {
            day = new Day01(File.ReadAllLines(@".\Data\day01.txt"));
        }

        [Benchmark]
        public void m1()
        {
            day.Part1();
        }

        [Benchmark]
        public void m2()
        {
            day.Part1();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<DayBench>();
        }
    }
}
