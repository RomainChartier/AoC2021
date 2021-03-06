using AoC2021.Cli;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace AoC2021.Tests
{
    public class TestsDay05
    {
        private readonly string[] values = File.ReadLines(@".\Data\Test\testday05.txt").ToArray();

        [Test]
        public void Part1()
        {
            var day = new Day05(values);

            Assert.That(day.Part1(), Is.EqualTo(5));
        }
        
        [Test]
        public void Part2()
        {
            var day = new Day05(values);

            Assert.That(day.Part2(), Is.EqualTo(12));
        }

        [Test]
        public void Result()
        {
            var day = new Day05();

            Assert.That(day.Part1(), Is.EqualTo(6189));
            Assert.That(day.Part2(), Is.EqualTo(19164));
        }        
    }
}
