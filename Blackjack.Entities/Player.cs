using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Entities
{
    public class Player : PlayerBase
    {
        public Player() : base()
        {
        }

        public int Credit { get; set; }
        public int Bet { get; set; }

        public override string ToString()
        {
            string handStatus;
            if (Hand.IsBlackjack())
            {
                handStatus = string.Format("{0} points - Blackjack!", Hand.GetPoints());
            }
            else if (Hand.IsBust())
            {
                handStatus = string.Format("{0} points - Busted!", Hand.GetPoints());
            }
            else
            {
                handStatus = string.Format("{0} points", Hand.GetPoints());
            }

            string result = string.Format("{0}{1} ", Environment.NewLine, Name);
            result += string.Format("({0}) {1}({2}) ", State, Hand, handStatus);
            result += string.Format("Credit:{0}, Bet:{1}", Credit, Bet);

            return result;
        }
    }
}
