using AoC2021.Cli;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace AoC2021.Tests
{
    public class TestsDay06
    {
        private readonly string[] values = File.ReadLines(@".\Data\Test\testday06.txt").ToArray();

        [Test]
        public void Part1()
        {
            var day = new Day06(values);

            Assert.That(day.Part1(), Is.EqualTo(5934));
        }
        
        [Test]
        public void Part2()
        {
            var day = new Day06(values);

            Assert.That(day.Part2(), Is.EqualTo(26984457539));
        }

        [Test]
        public void Result()
        {
            var day = new Day06();

            Assert.That(day.Part1(), Is.EqualTo(379114));
            Assert.That(day.Part2(), Is.EqualTo(1702631502303));
        }        
    }
}
