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

            SimplePlayer p1 = new SimplePlayer();
            MonteCarloPlayer p2 = new MonteCarloPlayer();
            GameEngine game = new GameEngine(p1, p2);
            game.Start();
            //p1.ShowHands();
            //p2.ShowHands();


            Console.ReadKey();
        }
    }
}
