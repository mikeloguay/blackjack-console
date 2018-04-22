using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blackjack.Entities;
using Blackjack.Services.Interface;
using System.Linq;

namespace Blackjack.Services.Test
{
    [TestClass]
    public class GameServiceTest
    {
        private static IGameService _gameService;
        private static IDeckService _deckService;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            _deckService = new DeckServiceStub();
            _gameService = new GameService(_deckService);
        }

        [TestMethod]
        public void TestJoinGame_Basic()
        {
            Player player = new Player() { Name = "Pepe", Credit = 1000 };
            Table table = _gameService.JoinGame(player);

            // Table created
            Assert.IsNotNull(table);

            // Cards dealt
            Assert.IsTrue(table.Player.Hand.Cards.Count >= 2);
            Assert.IsTrue(table.Dealer.Hand.Cards.Count >= 2);

            // Everything is a 3 spades because of the stub
            Assert.IsTrue(table.Player.Hand.Cards.All(c => c.Type == CardType.Three 
                && c.Suit == CardSuit.Spade));
            Assert.IsTrue(table.Dealer.Hand.Cards.All(c => c.Type == CardType.Three
                && c.Suit == CardSuit.Spade));

            // Players playing or finished
            Assert.IsFalse(table.Player.State == PlayerState.Waiting 
                && table.Dealer.State == PlayerState.Waiting);
        }

        [TestMethod]
        public void TestHit_DealerWin()
        {
            Player player = new Player() { Name = "Pepe", Credit = 1000 };
            Table table = CreateBasicTable();
            int intialPlayerCardsCount = table.Player.Hand.Cards.Count;
            int initialRound = table.Round;
            table = _gameService.Hit(player.Name, table);

            Assert.IsTrue(table.Player.Hand.Cards.Count == intialPlayerCardsCount + 1);
            Assert.IsTrue(table.Round > initialRound);

            Assert.AreEqual(GameStatus.InProgress, table.GetGameStatus());
            table.Player.State = PlayerState.Standing;
            table.Dealer.State = PlayerState.Standing;
            Assert.AreEqual(GameStatus.DealerWin, table.GetGameStatus());
        }

        [TestMethod]
        public void TestHit_PlayerWin()
        {
            Player player = new Player() { Name = "Pepe", Credit = 1000 };
            Table table = CreateBasicTable();
            int intialPlayerCardsCount = table.Player.Hand.Cards.Count;
            int initialRound = table.Round;
            table = _gameService.Hit(player.Name, table);
            table = _gameService.Hit(player.Name, table);
            table = _gameService.Hit(player.Name, table);

            Assert.AreEqual(GameStatus.InProgress, table.GetGameStatus());
            table.Player.State = PlayerState.Standing;
            table.Dealer.State = PlayerState.Standing;
            Assert.AreEqual(GameStatus.PlayerWin, table.GetGameStatus());
        }

        [TestMethod]
        public void TestStand_DealerWin()
        {
            Player player = new Player() { Name = "Pepe", Credit = 1000 };
            Table table = CreateBasicTable();
            int intialPlayerCardsCount = table.Player.Hand.Cards.Count;
            int initialRound = table.Round;
            table = _gameService.Stand(player.Name, table);

            Assert.IsTrue(table.Player.Hand.Cards.Count == intialPlayerCardsCount);
            Assert.IsTrue(table.Round > initialRound);

            Assert.AreEqual(GameStatus.DealerWin, table.GetGameStatus());
        }

        [TestMethod]
        public void TestStand_PlayerWin()
        {
            Player player = new Player() { Name = "Pepe", Credit = 1000 };
            Table table = CreateBasicTable();
            int intialPlayerCardsCount = table.Player.Hand.Cards.Count;
            table.Player.Hand.Cards.Add(_deckService.GetNextCard(table.Deck));
            table.Player.Hand.Cards.Add(_deckService.GetNextCard(table.Deck));
            table.Player.Hand.Cards.Add(_deckService.GetNextCard(table.Deck));
            table = _gameService.Stand(player.Name, table);

            Assert.AreEqual(GameStatus.PlayerWin, table.GetGameStatus());
        }

        private Table CreateBasicTable()
        {
            Table table = new Table();

            // Add player
            table.Player = new Player();

            // Add first hand
            Hand playerHand = new Hand();
            Card fiveSpades = new Card() { Type = CardType.Five, Suit = CardSuit.Spade };
            playerHand.Cards.Add(fiveSpades);
            Card sixHearts = new Card() { Type = CardType.Six, Suit = CardSuit.Heart };
            playerHand.Cards.Add(sixHearts);
            table.Player.Hand = playerHand;

            Hand dealerHand = new Hand();
            Card eightDiamonds = new Card() { Type = CardType.Eight, Suit = CardSuit.Diamond };
            dealerHand.Cards.Add(eightDiamonds);
            Card aceDiamonds = new Card() { Type = CardType.Ace, Suit = CardSuit.Diamond };
            aceDiamonds.Visible = false;
            dealerHand.Cards.Add(aceDiamonds);
            table.Dealer.Hand = dealerHand;

            return table;
        }
    }
}
