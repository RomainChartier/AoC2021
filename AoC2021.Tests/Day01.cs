using AoC2021.Cli;
using NUnit.Framework;

namespace AoC2021.Tests
{
    public class TestsDay01
    {
        private readonly string[] values = new[]
        {
            "199",
            "200",
            "208",
            "210",
            "200",
            "207",
            "240",
            "269",
            "260",
            "263",
        };

        [Test]
        public void Part1()
        {
            var day = new Day01(values);

            Assert.That(day.Part1(), Is.EqualTo(7));
        }

        [Test]
        public void Part2()
        {
            var day = new Day01(values);

            Assert.That(day.Part2(), Is.EqualTo(5));
        }

        [Test]
        public void Result()
        {
            var day = new Day01();

            Assert.That(day.Part1(), Is.EqualTo(1482));
            Assert.That(day.Part2(), Is.EqualTo(1518));
        }
    }
}
