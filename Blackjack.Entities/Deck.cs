using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utils;

namespace Blackjack.Entities
{
    public class Deck
    {
        public Deck()
        {
            IList<Card> cards = CreateCardsOrdered();
            cards.Shuffle();
            Cards = new Stack<Card>(cards);
        }

        public Stack<Card> Cards { get; set; }

        private List<Card> CreateCardsOrdered()
        {
            List<Card> cards = new List<Card>();

            var cardSuits = Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>();
            var cardTypes = Enum.GetValues(typeof(CardType)).Cast<CardType>();

            foreach (var cardSuit in cardSuits)
            {
                foreach (var cardType in cardTypes)
                {
                    cards.Add(new Card() { Suit = cardSuit, Type = cardType });
                }
            }

            return cards;
        }
    }
}
