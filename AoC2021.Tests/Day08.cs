using AoC2021.Cli;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace AoC2021.Tests
{
    public class TestsDay08
    {
        private readonly string[] values = File.ReadLines(@".\Data\Test\testday08.txt").ToArray();

        [Test]
        public void Part1()
        {
            var day = new Day08(values);

            Assert.That(day.Part1(), Is.EqualTo(26));
        }
        
        [Test]
        public void Part2()
        {
            var day = new Day08(values);

            Assert.That(day.Part2(), Is.EqualTo(61229));
        }
        
        [Test]
        public void Part22()
        {
            var day = new Day08(new[]
            {
                "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf"
            });

            Assert.That(day.Part2(), Is.EqualTo(5353));
        }

        [Test]
        public void Result()
        {
            var day = new Day08();

            Assert.That(day.Part1(), Is.EqualTo(247));
            Assert.That(day.Part2(), Is.EqualTo(933305));
        }        
    }
}
