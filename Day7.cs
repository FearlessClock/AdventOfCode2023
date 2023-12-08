using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day7 : IDay
    {
        public string GetResult(string[] inputs)
        {
            int sum = 0;
            List<CamelCardData> data = new List<CamelCardData>();
            for (int i = 0; i < inputs.Length; i++)
            {
                string[] cardSplit = inputs[i].Split(' ');
                CamelCardData cardData = new CamelCardData();
                cardData.cards = cardSplit[0];
                cardData.bid = int.Parse(cardSplit[1]);
                cardData.FindType();
                data.Add(cardData);
            }
            data.Sort();
            for (int i = 0; i < data.Count; i++)
            {
                sum += data[i].bid * (data.Count - i);
            }
            return sum.ToString();
        }
    }

    public enum eHandType { FIVE=6, FOUR=5, FULL=4, THREE=3, TWO_PAIR=2, PAIR=1, HIGH=0}

    public class CamelCardData : IComparable
    {
        public char[] cardOrder = new char[13] { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'};
        public string cards = "";
        public int bid;
        public eHandType handType = eHandType.FULL;

        public override string ToString()
        {
            return cards + " | " + bid + " | " + handType;
        }
        public void FindType()
        {
            if(cards == "K889J")
            {
                Console.WriteLine("Here");
            }
            Dictionary<char, int> letterCounter = new Dictionary<char, int>();
            for (int i = 0; i < cards.Length; i++)
            {
                if (letterCounter.ContainsKey(cards[i]))
                {
                    letterCounter[cards[i]]++;
                }
                else
                {
                    letterCounter.Add(cards[i], 1);
                }
            }

            char[] chars = letterCounter.Keys.ToArray();
            int[] counts = letterCounter.Values.ToArray();

            int jAmount = 0;
            bool containsJ = false;
            if (chars.Contains('J'))
            {
                for (int j = 0; j < chars.Length; j++)
                {
                    if (chars[j].Equals('J'))
                    {
                        containsJ = true;
                        jAmount = counts[j];
                        break;
                    }
                }
            }

            int highest = 0;
            int lowest = 6;
            for (int i = 0; i < counts.Length; i++)
            {
                int amount = 0;

                if (chars[i] != 'J')
                {
                    amount = counts[i] + jAmount;
                }
                else
                {
                    amount = counts[i];
                }
                if (amount > highest)
                {
                    highest = amount;
                }
                if (counts[i] < lowest)
                {
                    lowest = counts[i];
                }
            }

            if(chars.Length == 5 && !containsJ) 
            {
                handType = eHandType.HIGH;
            }
            else if((chars.Length == 4 && !containsJ) || (containsJ && chars.Length == 5 && highest == 2))
            {
                handType = eHandType.PAIR;
            }
            else if(highest == 5)
            {
                handType = eHandType.FIVE;
            }
            else if(((chars.Length == 2 && !containsJ) || containsJ && chars.Length == 3) && highest == 4)
            {
                handType = eHandType.FOUR;
            }
            else if(((chars.Length == 2 && !containsJ) && highest == 3 && lowest == 2)|| (containsJ && chars.Length == 3 && highest == 3 && lowest == 1))
            {
                handType = eHandType.FULL;
            }
            else if(((chars.Length == 3 && !containsJ) || (containsJ && chars.Length == 4))&& highest == 3 && lowest == 1)
            {
                handType = eHandType.THREE;
            }
            else if((chars.Length == 3 && !containsJ) && highest == 2 && lowest == 1)
            {
                handType = eHandType.TWO_PAIR;
            }
            else
            {
                Console.WriteLine("There was a problem calculating this hand " + cards);
            }
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;

            CamelCardData? camelCard = obj as CamelCardData;
            if (camelCard != null)
            {
                if(camelCard.handType == handType)
                {
                    for (int i = 0; i < cards.Length; i++)
                    {
                        if (camelCard.cards[i].Equals(cards[i]))
                        {
                            continue;
                        }
                        else
                        {
                            int currentCardIndex = 0;
                            int camelCardIndex = 0;
                            for (int j = 0; j < cardOrder.Length; j++)
                            {
                                if (cards[i].Equals(cardOrder[j]))
                                {
                                    currentCardIndex = j;
                                }
                                if (camelCard.cards[i].Equals(cardOrder[j]))
                                {
                                    camelCardIndex = j;
                                }
                            }
                            return camelCardIndex.CompareTo(currentCardIndex);
                        }
                    }
                }
                else
                {
                    return camelCard.handType.CompareTo(handType);
                }
            }
            else
                throw new ArgumentException("Object is not a Temperature");
            return 0;
        }
    }
}
