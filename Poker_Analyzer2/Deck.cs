using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Analyzer2
{
    public class Deck
    {
        private List<Card> cards = new List<Card>();
        private Random rng = new Random();

        public Deck()
        {
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
            
            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    Cards.Add(new Card(suit, rank));
                }
            }
            Shuffle();
        }

        // Метод для перетасовки колоды
        public void Shuffle()
        {
            int n = Cards.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                var value = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = value;
            }
        }

        public void DrawCard()
        {
            if (Cards.Count == 0)
                throw new InvalidOperationException("No cards left in the deck.");

            
            Cards.RemoveAt(0);
            
        }

            public Card Deal()
        {
            Shuffle();
            if (CardsRemaining == 0)
            {
                throw new InvalidOperationException("No cards left in the deck.");
            }
            var card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
        public int CardsRemaining => Cards.Count;

        public List<Card> Cards { get => cards; set => cards = value; }
    }
}
