using AoC2021.Cli;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace AoC2021.Tests
{
    public class TestsDay02
    {
        private readonly string[] values = File.ReadLines(@".\Data\Test\testday02.txt").ToArray();

        [Test]
        public void Part1()
        {
            var day = new Day02(values);

            Assert.That(day.Part1(), Is.EqualTo(150));
        }
        
        [Test]
        public void Part2()
        {
            var day = new Day02(values);

            Assert.That(day.Part2(), Is.EqualTo(900));
        }

        [Test]
        public void Result()
        {
            var day = new Day02();

            Assert.That(day.Part1(), Is.EqualTo(1480518));
            Assert.That(day.Part2(), Is.EqualTo(1282809906));
        }        
    }
}
