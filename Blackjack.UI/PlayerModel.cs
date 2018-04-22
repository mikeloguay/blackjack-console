using Blackjack.Entities;
using Blackjack.UI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.UI
{
    public class PlayerModel : IPlayerModel
    {
        public int Id { get; set; }
        public Table Table { get; set; }

        public bool IsMyTurn() 
        {
            // TODO - future: on multiplayer scenario, search player by ID and see if status == "Playing"
            return true;
        }

        public override string ToString()
        {
            return Table.ToString();
        }
    }
}
