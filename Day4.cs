using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day4 : IDay
    {
        public string GetResult(string[] inputs)
        {
            List<Card> cards = new List<Card>();
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i].Length == 0) { continue; }
                Card card= new Card();
                string[] cardData = inputs[i].Split(':');
                card.id = Convert.ToInt32(cardData[0].Replace("Card ", ""));
                cardData = cardData[1].Split('|');
                string[] winningNumbers = cardData[0].Split(' ');
                string[] cardNumbers = cardData[1].Split(' ');
                for (int j = 0; j < cardNumbers.Length; j++)
                {
                    if (cardNumbers[j].Length == 0) { continue; }
                    if (winningNumbers.Contains(cardNumbers[j]))
                    {
                        card.result++;
                    }
                }
                cards.Add(card);
                for (int j = 0; j < card.result; j++)
                {
                    queue.Enqueue(card.id + 1 + j);
                }
            }

            List<Card> newCards = new List<Card>();
            while (queue.Count > 0)
            {
                int id = queue.Dequeue();
                Card card = cards[id-1];
                newCards.Add(card);
                for (int j = 0; j < card.result; j++)
                {
                    queue.Enqueue(card.id + 1 + j);
                }
            }

            return (cards.Count + newCards.Count).ToString();
        }
    }

    class Card
    {
        public int id;
        public int result;
    }
}
