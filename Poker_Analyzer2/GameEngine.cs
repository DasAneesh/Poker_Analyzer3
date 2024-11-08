using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Analyzer2
{
    public class GameEngine
    {
        private Deck deck;
        private List<Card> river_cards;
        private SimplePlayer player1;
        private MonteCarloPlayer player2;
        public int Players1_bet;
        public int Players2_bet;
        public int pot;
        public int Winner;

        public GameEngine(SimplePlayer p1, MonteCarloPlayer p2)

        {
            deck = new Deck();
            river_cards = new List<Card>();
            this.player1 = p1;
            this.player2 = p2;
            Players1_bet = 0;
            Players2_bet = 0;
            pot = 0;
        }
        public void Start()
        {
            DealStartingHands();
            FirstRound();
            Flop();
            SecondRound();
            Turn();
            ThirdRound();
            River();
            FinalRound();
            DetermineWinner();

        }

        private void DealStartingHands()
        {
            player1.Hand.Add(deck.Deal());
            player1.Hand.Add(deck.Deal());
            deck.DrawCard();
            player2.Hand.Add(deck.Deal());
            player2.Hand.Add(deck.Deal());
            deck.DrawCard();
            Console.WriteLine(player1.Hand);
            Console.WriteLine(player2.Hand);
        }
        private void FirstRound()
        {
            // Логика первой ставки
            Players1_bet = (int)player1.SimpleStrategy();
            Players2_bet = player2.MonteCarloStrategy(river_cards,deck.Cards );

            // Обработка ставок игроков
            pot += Players1_bet + Players2_bet;
        }
        private void ShowRiverCards()
        {
            foreach (var card in river_cards)
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }
        }
        private void Flop()
        {
            river_cards.Add(deck.Deal());
            river_cards.Add(deck.Deal());
            river_cards.Add(deck.Deal());
            deck.DrawCard();
            ShowRiverCards();
        }
        private void SecondRound()
        {
            Players1_bet = (int)player1.SimpleStrategy();
            Players2_bet = player2.MonteCarloStrategy(river_cards, deck.Cards);

            pot += Players2_bet + Players1_bet;
        }

        private void Turn()
        {
            // Открываем одну карту
            river_cards.Add(deck.Deal());
            
            //ShowRiverCards();
        }
        private void ThirdRound()
        {

            Players1_bet = (int)player1.SimpleStrategy();
            Players2_bet = player2.MonteCarloStrategy(river_cards, deck.Cards);

            pot += Players2_bet + Players1_bet;
        }

        private void River()
        {
            // Открываем последнюю карту
            river_cards.Add(deck.Deal());

            //ShowRiverCards();
        }
        private void FinalRound()
        {

            Players1_bet = (int)player1.SimpleStrategy();
            Players2_bet = player2.MonteCarloStrategy(river_cards, deck.Cards);

            pot += Players1_bet + Players2_bet;
        }

      
        private void DetermineWinner()
        { 
            player1.Hand.AddRange(river_cards);
            player2.Hand.AddRange(river_cards);
            int player1Max = CombinationHelper.GetMaxScore(player1.Hand);
            int player2Max = CombinationHelper.GetMaxScore(player2.Hand);
            int WinnerPoints = player1Max > player2Max ? player1Max : player2Max;
            int LoserPoints = player1Max < player2Max ? player1Max : player2Max;
            SimplePlayer Winner = player1Max > player2Max ? player1 : player2;
            SimplePlayer NotWinner = player1Max < player2Max ? player1 : player2;
            
            Console.WriteLine($"Win {Winner.Strategy} with points {WinnerPoints}, with cards {Winner.ShowHands()}");
            Console.WriteLine($"Loses {NotWinner.Strategy} with points {LoserPoints}, with cards{NotWinner.ShowHands()}");

        }

    }
}
