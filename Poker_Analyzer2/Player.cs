using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Analyzer2
{
    public class Player
    {

        public string Strategy { get; set; } // Имя игрока
        public List<Card> Hand { get; set; } // Рука игрока (две карты)

        public int Chips = 1000;
        
        public Player()
        {
            Strategy = "Bob";
            Hand = new List<Card>();
            Chips = 1000;
        }
        public Player(string strategy_)
        {
            Strategy = strategy_;
            Hand = new List<Card>();
            Chips = 1000;
        }

        public string ShowHands()
        {
            string ans = "";
            ans += this.Strategy + "\n";
            foreach (var card in Hand)
            {
                
                ans += $" {card.Rank} of {card.Suit} ";
            }
            return ans;
        }
        public int Play()
        {
            // Применяем стратегию игрока для принятия решения (ставка, фолд и т.д.)
            int bet = 0;

            // Простая логика для примера — игрок всегда ставит 1/4 своих фишек
            if (Chips > 0)
            {
                bet = Chips / 4;
            }

            // Обработка случая недостатка фишек
            if (bet > Chips)
            {
                bet = Chips; // Ставим все фишки, если это больше, чем у нас есть
            }

            Chips -= bet; // Уменьшаем количество фишек игрока
            Console.WriteLine($"{Strategy}: ставит {bet} фишек. Осталось фишек: {Chips}");

            return bet; // Возвращаем ставку
        }


    }   
}
