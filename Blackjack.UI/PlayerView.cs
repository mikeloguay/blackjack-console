using Blackjack.Entities;
using Blackjack.UI.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blackjack.UI
{
    public class PlayerView : IPlayerView
    {
        public void GreetUser()
        {
            Console.WriteLine("Welcome to the Blackjack table (by Miguel Fernández)");
        }

        public void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Please, select one of the following options:");
            Console.WriteLine("1. Play a round");
            Console.WriteLine("9. Quit");
            Console.WriteLine();
        }

        public int AskForBet(Player humanPlayer)
        {
            Console.WriteLine("How much do you want to bet (1-{0})?", humanPlayer.Credit);
            return int.Parse(Console.ReadLine());
        }

        public PlayerAction AskForAction()
        {
            PlayerAction playerAction;
            Console.WriteLine("What do you want to do? (H)it · (S)tand");
            string actionString = Console.ReadLine().ToUpper();
            switch (actionString)
            {
                case "H":
                    playerAction = PlayerAction.Hit;
                    break;
                case "S":
                    playerAction = PlayerAction.Stand;
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            return playerAction;

        }

        public void ShowTableStatus(IPlayerModel playerModel)
        {
            SimulateDelay();
            Console.WriteLine();
            Console.WriteLine("{0}", playerModel.ToString());
        }

        private void SimulateDelay()
        {
            for (int i = 0; i < 6; i++)
            {
                Console.Write(".");
                Thread.Sleep(250);    
            }
        }

        public string AskForName()
        {
            Console.WriteLine("Please, enter your name: ");
            return Console.ReadLine();
        }
    }
}
