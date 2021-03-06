using AoC2021.Cli;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace AoC2021.Tests
{
    public class TestsDay04
    {
        private readonly string[] values = File.ReadLines(@".\Data\Test\testday04.txt").ToArray();

        [Test]
        public void Part1()
        {
            var day = new Day04(values);

            Assert.That(day.Part1(), Is.EqualTo(4512));
        }
        
        [Test]
        public void Part2()
        {
            var day = new Day04(values);

            Assert.That(day.Part2(), Is.EqualTo(1924));
        }

        [Test]
        public void Result()
        {
            var day = new Day04();

            Assert.That(day.Part1(), Is.EqualTo(11536));
            Assert.That(day.Part2(), Is.EqualTo(1284));
        }        
    }
}
