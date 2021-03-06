using AoC2021.Cli;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace AoC2021.Tests
{
    public class TestsDay07
    {
        private readonly string[] values = File.ReadLines(@".\Data\Test\testday07.txt").ToArray();

        [Test]
        public void Part1()
        {
            var day = new Day07(values);

            Assert.That(day.Part1(), Is.EqualTo(37));
        }
        
        [Test]
        public void Part2()
        {
            var day = new Day07(values);

            Assert.That(day.Part2(), Is.EqualTo(168));
        }

        [Test]
        public void Result()
        {
            var day = new Day07();

            Assert.That(day.Part1(), Is.EqualTo(357353));
            Assert.That(day.Part2(), Is.EqualTo(104822130));
        }        
    }
}
