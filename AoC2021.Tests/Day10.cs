using AoC2021.Cli;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace AoC2021.Tests
{
    public class TestsDay10
    {
        private readonly string[] values = File.ReadLines(@".\Data\Test\testday10.txt").ToArray();

        [Test]
        public void Part1()
        {
            var day = new Day10(values);

            Assert.That(day.Part1(), Is.EqualTo(26397));
        }
        
        [Test]
        public void Part2()
        {
            var day = new Day10(values);

            Assert.That(day.Part2(), Is.EqualTo(288957));
        }

        [Test]
        public void Result()
        {
            var day = new Day10();

            Assert.That(day.Part1(), Is.EqualTo(374061));
            Assert.That(day.Part2(), Is.EqualTo(2116639949));
        }        
    }
}
