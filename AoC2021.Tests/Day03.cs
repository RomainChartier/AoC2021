using AoC2021.Cli;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace AoC2021.Tests
{
    public class TestsDay03
    {
        private readonly string[] values = File.ReadLines(@".\Data\Test\testday03.txt").ToArray();

        [Test]
        public void Part1()
        {
            var day = new Day03(values);

            Assert.That(day.Part1(), Is.EqualTo(198));
        }
        
        [Test]
        public void Part2()
        {
            var day = new Day03(values);

            Assert.That(day.Part2(), Is.EqualTo(230));
        }

        [Test]
        public void Result()
        {
            var day = new Day03();

            Assert.That(day.Part1(), Is.EqualTo(845186));
            Assert.That(day.Part2(), Is.EqualTo(4636702));
        }        
    }
}
