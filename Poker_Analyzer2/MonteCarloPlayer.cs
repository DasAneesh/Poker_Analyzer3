using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Poker_Analyzer2
{
    public class MonteCarloPlayer : SimplePlayer
    {

        public MonteCarloPlayer(string name, int chips) : base(name, chips) { }
        public MonteCarloPlayer(string name) : base(name) { }
        public MonteCarloPlayer() : base() { }

        public int MonteCarloStrategy(List<Card> river, List<Card> deck)


        {
            SortedDictionary<int, int> percentage = new SortedDictionary<int, int>();

            //percentage.Values.Max();

            List<Card> tmp;

            for (int i = 0; i < deck.Count() - 1; i++)
            {
                for (int j = i + 1; j < deck.Count(); j++)
                {
                    tmp = new List<Card>();
                    tmp.Add(deck[i]);
                    tmp.Add(deck[j]);
                    tmp.AddRange(river);
                    int score = CombinationHelper.GetMaxScore(tmp);
                    if(percentage.ContainsKey(score)){
                        percentage[score]++;
                    }
                    else
                    {
                        percentage.Add(score, 1);
                    }
                }
            }

            
            tmp = new List<Card>();
            tmp.AddRange(Hand);
            tmp.AddRange(river);
            var myCombination = CombinationHelper.GetMaxScore(tmp);
            var loose = 0;
            foreach (var pair in percentage)
            {
                if(pair.Key >= myCombination)
                {
                    loose += pair.Value;
                }
            }
            var perc = (double)loose/percentage.Values.Sum();
            return 0;
        }


    }

}
