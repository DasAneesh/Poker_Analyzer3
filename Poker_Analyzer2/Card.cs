using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Analyzer2
{
    public class Card
    {
        private Card card;

        public string Suit { get; }
        public string Rank { get; }


        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
            

        }
       

        public Card(Card card)
        {
            this.card = card;
        }

        public override string ToString() => $"{Rank} of {Suit}";

        public int GetValue()
        {
            int val = 0;
            if (Rank == "Jack")
            {
                val = 11;
                return val;
            }
            else if (Rank == "Queen")
            {
                val = 12;
                return val;
            }
            else if (Rank == "King")
            {
                val = 13;
                return val;
            }
            else if (Rank == "Ace")
            {
                val = 14;
                return val;
            }
            else
            {
                val = int.Parse(Rank);
                return val;
            }
        }
        public int GetSuitOrder()
        {
            int val = 0;
            if (Suit == "Hearts")
            {
                val = 1;
                return val;
            }
            else if (Suit == "Diamonds")
            {
                val = 2;
                return val;
            }
            else if (Suit == "Clubs")
            {
                val = 3;
                return val;
            }
            else
            {
                val = 4;
                return val;
            }
        }

    }
}
