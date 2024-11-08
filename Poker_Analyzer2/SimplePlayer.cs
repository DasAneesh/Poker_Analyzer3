using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Analyzer2
{
    public class SimplePlayer { 
    
        public string Strategy { get; set; } // Имя игрока
        public List<Card> Hand { get; set; } // Рука игрока (две карты)

        public double Chips = 1000;

        public int winrate = 0;
        
        public SimplePlayer()
        {

            this.Hand = new List<Card>();
            this.Chips = 1000;
            this.Strategy = "John";
            this.winrate = 0;

        }
        public SimplePlayer(string name, int chips)
        {
            Strategy = name;
            Hand = new List<Card>();
            Chips = chips;
            this.winrate=0;

        }
        public SimplePlayer(string name)
        {
            Strategy = name;
            Hand = new List<Card>();
            Chips = 1000;
            this.winrate = 0;
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
        public double SimpleStrategy()
        {
            // Применяем стратегию игрока для принятия решения (ставка, фолд и т.д.)
            double bet = 0;

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
