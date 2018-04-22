using Blackjack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.UI.Interface
{
    public interface IPlayerView
    {
        void GreetUser();
        void ShowMenu();
        int AskForBet(Player humanPlayer);
        PlayerAction AskForAction();
        void ShowTableStatus(IPlayerModel playerModel);
        string AskForName();
    }
}
