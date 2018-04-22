using Blackjack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.UI.Interface
{
    public interface IPlayerModel
    {
        int Id { get; set; }
        Table Table { get; set; }
        bool IsMyTurn();
    }
}
