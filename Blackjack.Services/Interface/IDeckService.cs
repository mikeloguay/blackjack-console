using Blackjack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Services.Interface
{
    public interface IDeckService
    {
        Card GetNextCard(Deck deck);
    }
}
