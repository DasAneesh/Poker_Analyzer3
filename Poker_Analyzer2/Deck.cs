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
                    cards.Add(new Card(suit, rank));
                }
            }
            Shuffle();
        }

        // Метод для перетасовки колоды
        public void Shuffle()
        {
            int n = cards.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                var value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        public void DrawCard()
        {
            if (cards.Count == 0)
                throw new InvalidOperationException("No cards left in the deck.");

            
            cards.RemoveAt(0);
            
        }

            public Card Deal()
        {
            Shuffle();
            if (cards.Count == 0) throw new InvalidOperationException("No cards left in the deck.");
            var card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
        public int CardsRemaining => cards.Count;

    }
}
