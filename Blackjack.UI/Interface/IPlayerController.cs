using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.UI.Interface
{
    public interface IPlayerController
    {
        void ManageUserOption(string option);
        void PlayRound();
    }
}
