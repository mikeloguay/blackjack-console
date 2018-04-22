using Blackjack.Entities;
using Blackjack.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blackjack.Services
{
    public class GameService : IGameService
    {
        public event EventHandler<TableEventArgs> TableUpdated;

        private IDeckService _deckService;

        public GameService(IDeckService deckService)
        {
            _deckService = deckService;
        }
        
        public Table JoinGame(Player player)
        {
            // TODO - future: on multiplayer scenario, the Table must be created just on first player join, and the
            // dealing process must be done on other place
            Table table = new Table();
            player.State = PlayerState.Playing;
            table.Player = player;
            table.Round = 1;

            // Deal 2 cards to the player (face up)
            Hand playerHand = new Hand();
            Card playerCard1 = _deckService.GetNextCard(table.Deck);
            playerHand.Cards.Add(playerCard1);
            Card playerCard2 = _deckService.GetNextCard(table.Deck);
            playerHand.Cards.Add(playerCard2);
            table.Player.Hand = playerHand;

            // Deal 2 cards to the dealer (1 face up and 1 face down)
            Hand dealerHand = new Hand();
            Card dealerCard1 = _deckService.GetNextCard(table.Deck);
            dealerHand.Cards.Add(dealerCard1);
            Card dealerCard2 = _deckService.GetNextCard(table.Deck);
            dealerCard2.Visible = false;
            dealerHand.Cards.Add(dealerCard2);
            table.Dealer.Hand = dealerHand;

            // If player has blackjack on the first hand, it is dealer's turn
            if (table.Player.Hand.IsBlackjack())
            {
                table.Player.State = PlayerState.Standing;
                ProcessDealerTurn(table);
            }

            // notify via event
            OnTableUpdated(new TableEventArgs() { Table = table });

            return table;
        }

        public Table Hit(string playerName, Table table)
        {
            Player currentPlayer = table.GetPlayerByName(playerName);
            table.Round++;
            Card newCard = _deckService.GetNextCard(table.Deck);
            currentPlayer.Hand.Cards.Add(newCard);
            newCard.Visible = true;

            // notify via event
            OnTableUpdated(new TableEventArgs() { Table = table, NewCardHit = newCard });

            if (currentPlayer.Hand.GetPoints() == 21 || currentPlayer.Hand.IsBust())
            {
                currentPlayer.State = PlayerState.Standing;
                ProcessDealerTurn(table);
            }

            return table;
        }

        private Table HitDealer(Table table)
        {
            table.Round++;
            Card newCard = _deckService.GetNextCard(table.Deck);
            table.Dealer.Hand.Cards.Add(newCard);
            newCard.Visible = true;

            // notify via event
            OnTableUpdated(new TableEventArgs() { Table = table, NewCardHit = newCard });

            return table;
        }

        public Table Stand(string playerName, Table table)
        {
            Player currentPlayer = table.GetPlayerByName(playerName);
            table.Round++;
            currentPlayer.State = PlayerState.Standing;

            // notify via event
            OnTableUpdated(new TableEventArgs() { Table = table });

            ProcessDealerTurn(table);

            return table;
        }

        private Table StandDealer(Table table)
        {
            table.Round++;
            table.Dealer.State = PlayerState.Standing;

            // notify via event
            OnTableUpdated(new TableEventArgs() { Table = table });

            return table;
        }

        protected virtual void OnTableUpdated(TableEventArgs e)
        {
            EventHandler<TableEventArgs> handler = TableUpdated;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void ProcessDealerTurn(Table table)
        {
            table.Dealer.State = PlayerState.Playing;
            table.Dealer.Hand.Cards.ToList().ForEach(c => c.Visible = true);

            // Notify to show the hidden card
            OnTableUpdated(new TableEventArgs() { Table = table });

            while (table.Dealer.Hand.GetPoints() < 17)
            {
                HitDealer(table);

            }
            StandDealer(table);

            // Game finished
            GameStatus gameStatus = table.GetGameStatus();
            table.UpdatePlayerCredit(gameStatus);

            // Last notificacion
            OnTableUpdated(new TableEventArgs() { Table = table });
        }
    }
}
