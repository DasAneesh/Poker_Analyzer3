using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Analyzer2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            //Console.WriteLine("Колода карт:");
            //Console.WriteLine(deck);

            //Console.WriteLine("Достаем 5 карт:");
            //for (int i = 0; i < 5; i++)
            //{
            //    Card card = deck.DrawCard();
            //    Console.WriteLine(card);
            //}
             
            //Console.WriteLine($"Карты в колоде: {deck.CardsRemaining}");

            //Player p1 = new Player();
            //Player p2 = new Player();
            //GameEngine game = new GameEngine(p1,p2);
            //game.Start();
            //p1.ShowHands();
            //p2.ShowHands();
            Console.WriteLine(
            CombinationHelper.GetMaxScore(new List<Card>()
            {
                new Card("Hearts","9"),
               
                new Card("Diamonds", "10"),
                new Card("Diamonds", "King"),
                new Card("Diamonds", "Queen"),
                new Card("Diamonds", "Ace"),
                new Card("Diamonds", "Jack"),
                new Card("Clubs", "10"),
            })
);

            Console.ReadKey();
        }
    }
}
