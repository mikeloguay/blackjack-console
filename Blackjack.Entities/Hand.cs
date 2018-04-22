using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Entities
{
    public class Hand
    {
        public Hand()
        {
            Cards = new List<Card>();
        }
        
        public ICollection<Card> Cards { get; set; }

        /// <summary>
        /// Get the points that sums the hand. No matter if some card are visible or not
        /// </summary>
        /// <returns>Number of points</returns>
        public int GetPoints()
        {
            int points = 0;

            foreach (var card in Cards)
            {
                points += card.GetPoints();
            }

            return points;
        }

        /// <summary>
        /// Get the points that sums the hand, just taking into account visible cards
        /// </summary>
        /// <returns>Number of points of visible cards</returns>
        public int GetPublicPoints()
        {
            int points = GetPoints();
            Card faceDownCard = this.Cards.SingleOrDefault(c => !c.Visible);
            if (faceDownCard != null)
            {
                points -= faceDownCard.GetPoints();
            }

            return points;
        }

        public bool IsBlackjack()
        {
            // Has to be on the first hand, and sum 21
            return Cards.Count == 2 && GetPoints() == 21;
        }

        public bool IsBust()
        {
            return GetPoints() > 21;
        }

        public override string ToString()
        {
            string result = "[";

            foreach (var card in Cards)
            {
                result += string.Format("{0},", card);    
            }

            // Last comma removed
            result = result.Substring(0, result.Length - 1);

            result += "]";

            return result;
        }
    }
}
