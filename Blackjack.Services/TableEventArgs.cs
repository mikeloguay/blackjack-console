using Blackjack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Services
{
    public class TableEventArgs : EventArgs
    {
        public Table Table { get; set; }
        public Card NewCardHit { get; set; }
    }
}
