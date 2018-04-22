using Blackjack.Entities;
using Blackjack.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Services
{
    public class DeckService : IDeckService
    {
        public Card GetNextCard(Deck deck)
        {
            return deck.Cards.Pop();
        }
    }
}
