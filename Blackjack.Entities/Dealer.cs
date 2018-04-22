using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Entities
{
    public class Dealer : PlayerBase
    {
        public Dealer()
        {
            Name = "Dealer";
        }

        public override string ToString()
        {
            string handStatus = string.Format("{0} points", Hand.GetPublicPoints());

            // Avoid reveal secrets :)
            if (Hand.Cards.All(c => c.Visible))
            {
                if (Hand.IsBlackjack())
                {
                    handStatus += " - Blackjack!";
                }
                else if (Hand.IsBust())
                {
                    handStatus += " - Busted!";
                }
            }

            string result = string.Format("{0}{1} ", Environment.NewLine, Name);
            result += string.Format("({0}) {1}({2}) ", State, Hand, handStatus);

            return result;
        }
    }
}
