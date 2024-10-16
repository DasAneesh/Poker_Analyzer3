using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Analyzer2
{

    public enum CombinationTitle
    {
        HighCard,
        Pair,
        TwoPair,
        Set,
        Straight,
        Flush,
        Fullhouse,
        Fourofakind,
        StraightFlush,
        RoyalFlush


    }
    public class Combination
    {
        private int score;
        private List<Card> cards;
        private CombinationTitle combinationTitle;

        public Combination(int score,List<Card> cards, CombinationTitle combinationTitle)
        {
            this.score = score;
            this.cards = cards;
            this.combinationTitle = combinationTitle;
        }

        public int Score { get => score; set => score = value; }
        public List<Card> Cards { get => cards; set => cards = value; }
        public CombinationTitle CombinationTitle { get => combinationTitle; set => combinationTitle = value; }


    }
}
