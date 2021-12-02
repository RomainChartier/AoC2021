using AoC2021.Cli;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace AoC2021.Tests
{
    public class TestsDay01
    {
        private readonly string[] values = File.ReadLines(@".\Data\Test\testday01.txt").ToArray();

        [Test]
        public void Part1()
        {
            var day = new Day01(values);

            Assert.That(day.Part1(), Is.EqualTo(0));
        }
        
        [Test]
        public void Part2()
        {
            var day = new Day01(values);

            Assert.That(day.Part2(), Is.EqualTo(0));
        }

        [Test]
        public void Result()
        {
            var day = new Day01();

            Assert.That(day.Part1(), Is.EqualTo(0));
            Assert.That(day.Part2(), Is.EqualTo(0));
        }        
    }
}
