using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Entities
{
    public class Card
    {
        public Card()
        {
            Visible = true;
        }

        public CardType Type { get; set; }
        public CardSuit Suit { get; set; }
        public bool Visible { get; set; }

        public bool IsFace
        {
            get
            {
                return Type == CardType.Jack || Type == CardType.Queen || Type == CardType.King;
            }
        }

        public int GetPoints()
        {
            int points = 0;

            switch (Type)
            {
                case CardType.Ace:
                    // TODO - future: allow also 1 as ace point (selected by user or automatically somehow)
                    points += 11;
                    break;
                case CardType.Jack:
                case CardType.Queen:
                case CardType.King:
                    points += 10;
                    break;
                default:
                    points += (int)Type;
                    break;
            }

            return points;
        }

        public override string ToString()
        {
            string result = string.Empty;

            if (Visible)
            {
                result += string.Format("{0}·{1}", Type, Suit);
            } 
            else
            {
                result += "???";
            }

            return result;
        }
    }
}
