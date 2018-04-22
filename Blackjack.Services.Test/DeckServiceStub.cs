using Blackjack.Entities;
using Blackjack.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Services.Test
{
    /// <summary>
    /// Avoid random cards because of the behaviour of the DeckService (standard implementation).
    /// It returns always 3-spade
    /// </summary>
    public class DeckServiceStub : IDeckService
    {
        public Card GetNextCard(Deck deck)
        {
            return new Card() { Type = CardType.Three, Suit = CardSuit.Spade };
        }
    }
}
