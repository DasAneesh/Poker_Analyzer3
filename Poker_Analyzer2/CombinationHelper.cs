﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Analyzer2
{
    public static class CombinationHelper
    {
        private const int TWO_PAIR_MODIFIER = 30;
        private const int SET_MODIFIER = 80;
        private const int STRAIGHT_MODIFIER = 110;
        private const int FLUSH_MODIFIER = 160;
        private const int FULLHOUSE_MODIFIER = 220;
        private const int FOUROFAKIND_MODIFIER = 290;
        private const int STRAIGHTFLUSH_MODIFIER = 310;
        private const int ROYALEFLUSH_MODIFIER = 320;
        public static int GetMaxScore(List<Card> cards)
        {
            return GetCombinations(cards).Max(x =>
            {
                return x != null ? x.Score : 0;
            });
        }
        // HighCard,
        //Pair,*
        //TwoPair,
        //Set,*
        //Straight,*
        //Flush,*
        //Fullhouse,
        //Fourofakind,*
        //StraightFlush,
        //RoyalFlush
        public static List<Combination> GetCombinations(List<Card> cards)
        {
            List<Combination> result = new List<Combination>();
            result.AddRange(GetAllPairs(cards));
            result.AddRange(GetSet(cards));
            result.Add(GetFourOfAKind(cards));
            cards.Sort(delegate (Card c1, Card c2) { return c1.GetValue().CompareTo(c2.GetValue()); });
            result.AddRange(GetStraight(cards));
            cards.Sort(delegate (Card c1, Card c2) { return c1.GetSuitOrder().CompareTo(c2.GetSuitOrder()); });
            result.AddRange(GetFlush(cards));
            result.Add(CheckRoyalFlush(result));
            result.Add(CheckStraightFlush(result));
            result.Add(CheckFullHouse(result));
            result.Add(CheckTwoPair(result));
            result.Add(CheckHighCard(result, cards));
            return result;

        }

        private static Combination CheckHighCard(List<Combination> result, List<Card> cards)
        {
            if (result.Count == 0)
            {
                int highCard = cards.Max(x => x.GetValue());
                int highIndex = cards.FindIndex(x => x.GetValue() == highCard);
                return new Combination(highCard, new List<Card>() { cards[highIndex] }, CombinationTitle.HighCard);
            }
            return null;


        }

        private static Combination CheckTwoPair(List<Combination> result)
        {
            List<Combination> Twopair = new List<Combination>();
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] == null) continue;
                if (result[i].CombinationTitle == CombinationTitle.Pair)
                {
                    Twopair.Add(result[i]);
                }
            }
            if (Twopair.Count >= 2)
            {
                Twopair.OrderBy(x => x.Score);
                var last = Twopair.Last();
                var secondToLast = Twopair[Twopair.Count - 2];
                var twoPairCards = new List<Card>();
                twoPairCards.AddRange(last.Cards);
                twoPairCards.AddRange(secondToLast.Cards);
                Console.WriteLine("find TwoPair");
                return new Combination(twoPairCards.Sum(x => x.GetValue()) + TWO_PAIR_MODIFIER, twoPairCards, CombinationTitle.TwoPair);
            }
            else
            {
                return null;
            }


        }

        private static Combination CheckFullHouse(List<Combination> result)
        {
            List<Combination> Twopair = new List<Combination>();
            for (int i = 0; i < result.Count; i++)

            {
                if (result[i] == null) continue;
                if (result[i].CombinationTitle == CombinationTitle.Pair)
                {
                    Twopair.Add(result[i]);
                }
            }
            List<Combination> Set = new List<Combination>();
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] == null) continue;
                if (result[i].CombinationTitle == CombinationTitle.Set)
                {
                    Set.Add(result[i]);
                }
            }

            if (Twopair.Count == 0 || Set.Count == 0)
            {
                return null;

            }

            else if (Twopair.Count > 1 && Set.Count > 0)
            {
                Twopair.OrderBy(x => x.Score);
                Set.OrderBy(x => x.Score);
                Combination MaxSet = Set.Last();
                for (int i = Twopair.Count - 1 - 1; i >= 0; i--)
                {
                    if (Twopair[i].Cards.Last().GetValue() != MaxSet.Cards.Last().GetValue())
                    {


                        var cards = new List<Card>();
                        cards.AddRange(Twopair[i].Cards);
                        cards.AddRange(MaxSet.Cards);
                        Console.WriteLine("find FullHouse");
                        return new Combination(MaxSet.Cards.Sum(x => x.GetValue()) + Twopair[i].Cards.Sum(x => x.GetValue()) +
                            FULLHOUSE_MODIFIER, cards, CombinationTitle.Fullhouse);

                    }

                }

            }
            return null;



        }

        private static Combination CheckStraightFlush(List<Combination> result)
        {
            List<int> straightIndices = new List<int>();
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] == null) continue;
                if (result[i].CombinationTitle == CombinationTitle.Straight)
                {
                    straightIndices.Add(i);
                }
            }
            for (int j = 0; j < straightIndices.Count; j++)
            {
                if (GetFlush(result[straightIndices[j]].Cards) != null)
                {
                    Console.WriteLine("find StraightFlush");
                    return new Combination (result[straightIndices[j]].Cards.Sum(x => x.GetValue()) + STRAIGHTFLUSH_MODIFIER, result[straightIndices[j]].Cards, CombinationTitle.StraightFlush);
                }
            }
            return null;
        }
    private static Combination CheckRoyalFlush(List<Combination> result)
    {
            List<int> straightIndices = new List<int>();
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] == null) continue;
                if (result[i].CombinationTitle == CombinationTitle.Straight)
                {
                    straightIndices.Add(i);
                }
            }
            for (int j = 0; j < straightIndices.Count; j++)
            {
                if (GetFlush(result[straightIndices[j]].Cards) != null && result[straightIndices[j]].Cards.First().GetValue() == 10)
                {
                    Console.WriteLine("find RoyaleFlush");
                    return new Combination(result[straightIndices[j]].Cards.Sum(x => x.GetValue()) + ROYALEFLUSH_MODIFIER, result[straightIndices[j]].Cards, CombinationTitle.RoyalFlush);
                }
            }
            return null;
        }

    private static List<Combination> GetFlush(List<Card> cards)
    {
        List<Combination> combinations = new List<Combination>();
        for (int i = 0; i < cards.Count - 3; i++)
        {
            for (int j = i + 1; j < cards.Count - 2; j++)
            {
                for (int k = j + 1; k < cards.Count - 1; k++)
                {
                    for (int l = k + 1; l < cards.Count(); l++)
                    {
                        for (int g = l + 1; g < cards.Count(); g++)
                        {

                            if (cards[i].Suit == cards[j].Suit &&
                                cards[i].Suit == cards[k].Suit &&
                                cards[i].Suit == cards[l].Suit &&
                                cards[i].Suit == cards[g].Suit)
                            {
                                Console.WriteLine("find Flush");
                                combinations.Add(
                                    new Combination(
                                        cards[i].GetValue() +
                                        cards[j].GetValue() +
                                        cards[k].GetValue() +
                                        cards[l].GetValue() +
                                        cards[g].GetValue() +
                                        FLUSH_MODIFIER,
                                        new List<Card>() {
                                        cards[i],
                                        cards[j],
                                        cards[k],
                                        cards[l],
                                        cards[g],
                                        },
                                       CombinationTitle.Flush));
                            }
                        }
                    }
                }
            }
        }
        return combinations;
    }

    private static List<Combination> GetStraight(List<Card> cards)
    {
        List<Combination> combinations = new List<Combination>();
        for (int i = 0; i < cards.Count - 4; i++)
        {
            for (int j = i + 1; j < cards.Count - 3; j++)
            {
                for (int k = j + 1; k < cards.Count - 2; k++)
                {
                    for (int l = k + 1; l < cards.Count() - 1; l++)
                    {
                        for (int g = l + 1; g < cards.Count(); g++)
                        {

                            if (cards[i].GetValue() == cards[j].GetValue() - 1 &&
                                cards[i].GetValue() == cards[k].GetValue() - 2 &&
                                cards[i].GetValue() == cards[l].GetValue() - 3 &&
                                cards[i].GetValue() == cards[g].GetValue() - 4)
                            {
                                Console.WriteLine("find Straight");
                                combinations.Add(
                                    new Combination(
                                        cards[i].GetValue() +
                                        cards[j].GetValue() +
                                        cards[k].GetValue() +
                                        cards[l].GetValue() +
                                        cards[g].GetValue() +
                                        STRAIGHT_MODIFIER,
                                        new List<Card>() {
                                        cards[i],
                                        cards[j],
                                        cards[k],
                                        cards[l],
                                        cards[g],
                                        },
                                       CombinationTitle.Straight));
                            }
                        }
                    }
                }
            }
        }
        return combinations;
    }

    private static Combination GetFourOfAKind(List<Card> cards)
    {
        for (int i = 0; i < cards.Count - 3; i++)
        {
            for (int j = i + 1; j < cards.Count - 2; j++)
            {
                for (int k = j + 1; k < cards.Count - 1; k++)
                {
                    for (int l = k + 1; l < cards.Count(); l++)
                    {
                        if (cards[j].Rank == cards[i].Rank &&
                            cards[j].Rank == cards[k].Rank &&
                            cards[j].Rank == cards[l].Rank)
                        {
                            Console.WriteLine("find 4");
                            return new Combination(
                                cards[i].GetValue() +
                                cards[j].GetValue() +
                                cards[k].GetValue() +
                                cards[l].GetValue() +
                                FOUROFAKIND_MODIFIER,
                                new List<Card>() {
                                        cards[i],
                                        cards[j],
                                        cards[k],
                                        cards[l]
                                },
                               CombinationTitle.Fourofakind);
                        }
                    }
                }
            }
        }
        return null;
    }

    private static List<Combination> GetSet(List<Card> cards)
    {
        List<Combination> combinations = new List<Combination>();
        for (int i = 0; i < cards.Count - 2; i++)
        {
            for (int j = i + 1; j < cards.Count - 1; j++)
            {
                for (int k = j + 1; k < cards.Count; k++)
                {
                    if (cards[j].Rank == cards[i].Rank &&
                        cards[i].Rank == cards[k].Rank)
                    {
                        Console.WriteLine("find 3");
                        combinations.Add(
                            new Combination(
                                cards[i].GetValue() +
                                cards[j].GetValue() +
                                cards[k].GetValue() +
                                SET_MODIFIER,
                                new List<Card>() {
                                        cards[i],
                                        cards[j],
                                        cards[k]
                                },
                               CombinationTitle.Set));
                    }
                }
            }
        }
        return combinations;
    }

    private static List<Combination> GetAllPairs(List<Card> cards)
    {
        List<Combination> combinations = new List<Combination>();
        for (int i = 0; i < cards.Count - 1; i++)
        {
            for (int j = i + 1; j < cards.Count; j++)
            {
                if (cards[j].Rank == cards[i].Rank)
                {
                    Console.WriteLine("find pair");
                    combinations.Add(
                        new Combination(
                            cards[i].GetValue() +
                            cards[j].GetValue(),
                            new List<Card>() {
                                    cards[i],
                                    cards[j]
                            },
                            CombinationTitle.Pair));
                }
            }
        }

        return combinations;
    }
}
}
