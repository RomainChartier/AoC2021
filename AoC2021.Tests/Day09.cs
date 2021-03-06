using AoC2021.Cli;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace AoC2021.Tests
{
    public class TestsDay09
    {
        private readonly string[] values = File.ReadLines(@".\Data\Test\testday09.txt").ToArray();

        [Test]
        public void Part1()
        {
            var day = new Day09(values);

            Assert.That(day.Part1(), Is.EqualTo(15));
        }
        
        [Test]
        public void Part2()
        {
            var day = new Day09(values);

            Assert.That(day.Part2(), Is.EqualTo(1134));
        }

        [Test]
        public void Result()
        {
            var day = new Day09();

            Assert.That(day.Part1(), Is.EqualTo(444));
            Assert.That(day.Part2(), Is.EqualTo(1168440));
        }        
    }
}
