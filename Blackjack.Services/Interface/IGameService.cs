using Blackjack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Services.Interface
{
    public interface IGameService
    {
        /// <summary>
        /// Notify the observers with table updates
        /// </summary>
        event EventHandler<TableEventArgs> TableUpdated;

        Table JoinGame(Player player);
        Table Hit(string playerName, Table table);
        Table Stand(string playerName, Table table);
    }
}
