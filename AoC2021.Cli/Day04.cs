using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2021.Cli
{
    public class Day04 : IDay
    {
        private readonly string[] values;

        public Day04() : this(File.ReadLines(@".\Data\day04.txt").ToArray()) { }
        public Day04(string[] values) { this.values = values; }

        public object Part1()
        {
            var numbers = values[0].Split(',').Select(int.Parse);
            var cards = ParseCards(values);
            
            foreach (var draw in numbers)
            {
                foreach (var card in cards)
                {
                    ApplyDraw(card, draw);

                    if (IsWon(card))
                    {
                        return CalculateScore(card, draw);
                    }
                }               
            }

            throw new Exception("No winning card found");
        }

        public object Part2()
        {
            var numbers = values[0].Split(',').Select(int.Parse);
            var cards = ParseCards(values);
            var remainingCards = new HashSet<Card>(cards);

            foreach (var draw in numbers)
            {
                foreach (var card in cards)
                {
                    ApplyDraw(card, draw);

                    if (IsWon(card))
                    {
                        if(remainingCards.Count == 1)
                        {
                            return CalculateScore(card, draw);
                        }
                        remainingCards.Remove(card);
                    }
                }

                cards = remainingCards.ToArray();
            }

            throw new Exception("No winning card found");
        }

        private static void ApplyDraw(Card card, int draw)
        {
            for (var i = 0; i < card.Numbers.Length; i++)
            {
                if (card.Numbers[i] == draw)
                {
                    card.Drawn[i] = true;
                };
            }
        }

        private int CalculateScore(Card card, int currentDraw)
        {
            var sumOfNonDrawn = card.Numbers
                .Select((v, i) => card.Drawn[i] ? 0 : v)
                .Sum();

            return sumOfNonDrawn * currentDraw;
        }

        private static bool IsWon(Card card)
        {
            // Horizontally
            for (var x = 0; x < 5; x++)
            {
                for (var y = 0; y < 5; y++)
                {
                    if (!card.Drawn[Index(x, y)])
                    {
                        break;
                    };

                    if (y == 4)
                    {
                        return true;
                    }
                }
            }
            // Vertically
            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    if (!card.Drawn[Index(x, y)])
                    {
                        break;
                    };

                    if (x == 4)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public readonly record struct Card(int[] Numbers, bool[] Drawn)
        {
            public override string ToString()
            {
                var builder = new StringBuilder();
                for (var y = 0; y < 5; y++)
                {
                    for (var x = 0; x < 5; x++)
                    {
                        if(Drawn[Index(x, y)])
                        {
                            builder.Append('*');
                        }
                        builder.Append(Numbers[Index(x,y)]);
                        builder.Append('\t');
                    }
                    builder.AppendLine();
                }

                return builder.ToString();
            }
        };

        private static Card[] ParseCards(string[] values)
        {
            var result = new List<Card>();

            var currentCard = new int[5 * 5];
            var currentLine = 0;

            foreach (var line in values.Skip(1))
            {
                if (string.IsNullOrEmpty(line))
                {
                    currentCard = new int[5 * 5];
                    currentLine = 0;
                    continue;
                }

                var currentV = line.Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToArray();
                for (var x = 0; x < 5; x++)
                {
                    currentCard[Index(x, currentLine)] = currentV[x];
                }

                currentLine++;
                if(currentLine == 5)
                {
                    var card = new Card(currentCard, new bool[5 * 5]);
                    result.Add(card);
                }
            }

            return result.ToArray();
        }

        private static int Index(int x, int y) => x + y * 5;

    }
}
