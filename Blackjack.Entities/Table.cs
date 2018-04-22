using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Entities
{
    public class Table
    {
        public Table()
        {
            Dealer = new Dealer();
            Deck = new Deck();
        }

        // TODO - future: on multiplayer scenario, would be a list of players
        public Player Player { get; set; }
        public Dealer Dealer { get; set; }
        public Deck Deck { get; set; }
        public int Round { get; set; }

        public bool IsGameFinished()
        {
            return Player.State == PlayerState.Standing && Dealer.State == PlayerState.Standing;
        }

        public GameStatus GetGameStatus()
        {
            GameStatus gameStatus;

            if (!IsGameFinished())
            {
                gameStatus = GameStatus.InProgress;
            }
            else if (Player.Hand.IsBlackjack() && !Dealer.Hand.IsBlackjack()
                || Player.Hand.GetPoints() > Dealer.Hand.GetPoints() && !Player.Hand.IsBust()
                || Player.Hand.GetPoints() < Dealer.Hand.GetPoints() && !Player.Hand.IsBust()  && Dealer.Hand.IsBust())
            {
                gameStatus = GameStatus.PlayerWin;
            }
            else if (!Player.Hand.IsBlackjack() && Dealer.Hand.IsBlackjack()
                || Player.Hand.GetPoints() < Dealer.Hand.GetPoints() && !Dealer.Hand.IsBust()
                || Player.Hand.GetPoints() > Dealer.Hand.GetPoints() && Player.Hand.IsBust() && !Dealer.Hand.IsBust())
            {
                gameStatus = GameStatus.DealerWin;
            }
            else
            {
                gameStatus = GameStatus.Push;
            }

            return gameStatus;
        }

        public void UpdatePlayerCredit(GameStatus gameResult)
        {
            int creditDelta;

            // Blackjack is paid 3/2, normal win 1/1
            if (Player.Hand.IsBlackjack())
            {
                creditDelta = (Player.Bet * 3) / 2;
            }
            else
            {
                creditDelta = Player.Bet;
            }

            if (gameResult == GameStatus.PlayerWin)
            {
                Player.Credit += creditDelta;
            }
            else if (gameResult == GameStatus.DealerWin)
            {
                Player.Credit -= creditDelta;
            }

            // Initialize the bet
            Player.Bet = 0;
        }

        public Player GetPlayerByName(string playerName)
        {
            // TODO - future: on multiplayer scenario, search on the list
            return Player; 
        }

        public override string ToString()
        {
            string result = string.Format("{0}-------- ROUND {1} --------{2}{3}{4}",
                Environment.NewLine, Round, Player, Dealer, Environment.NewLine);

            //result += string.Format("Game status: {0}", GetGameStatus());

            switch (GetGameStatus())
            {
                case GameStatus.InProgress:
                    result += string.Format("Game status: {0}", "In progress");
                    break;
                case GameStatus.PlayerWin:
                    result += string.Format("{0} WINS!", Player.Name);
                    break;
                case GameStatus.DealerWin:
                    result += string.Format("The dealer WINS!");
                    break;
                case GameStatus.Push:
                    result += string.Format("You both get a PUSH");
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
