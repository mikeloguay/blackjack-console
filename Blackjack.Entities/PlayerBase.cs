using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Entities
{
    public abstract class PlayerBase
    {
        // TODO - future: some kind of ID would be needed. The Name is enough by now
        public String Name { get; set; }
        public PlayerState State { get; set; }
        public Hand Hand { get; set; }

        public PlayerBase()
        {
            State = PlayerState.Waiting;
        }
    }
}
