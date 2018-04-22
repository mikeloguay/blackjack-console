using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Blackjack.Entities.Test
{
    [TestClass]
    public class TableTest
    {
        [TestMethod]
        public void TestIsFinished_InitialState()
        {
            Table table = CreateBasicTable();
            Assert.IsFalse(table.IsGameFinished());
        }

        [TestMethod]
        public void TestIsFinished_Positive()
        {
            Table table = CreateBasicTable();
            table.Player.State = PlayerState.Standing;
            table.Dealer.State = PlayerState.Standing;
            Assert.IsTrue(table.IsGameFinished());

        }

        [TestMethod]
        public void TestGetGameStatus_InProgress()
        {
            Table table = CreateBasicTable();
            Assert.AreEqual(GameStatus.InProgress, table.GetGameStatus());
        }

        [TestMethod]
        public void TestGetGameStatus_DealerWin()
        {
            Table table = CreateBasicTable();
            table.Player.State = PlayerState.Standing;
            table.Dealer.State = PlayerState.Standing;
            Assert.AreEqual(GameStatus.DealerWin, table.GetGameStatus());
        }

        [TestMethod]
        public void TestGetGameStatus_PlayerWin()
        {
            Table table = CreateBasicTable();
            table.Player.Hand.Cards.Add(new Card() { Type = CardType.Ten, Suit = CardSuit.Club });
            table.Player.State = PlayerState.Standing;
            table.Dealer.State = PlayerState.Standing;
            Assert.AreEqual(GameStatus.PlayerWin, table.GetGameStatus());
        }

        [TestMethod]
        public void TestGetGameStatus_NormalPush()
        {
            Table table = CreateBasicTable();
            table.Player.Hand.Cards.Add(new Card() { Type = CardType.Eight, Suit = CardSuit.Club });
            table.Player.State = PlayerState.Standing;
            table.Dealer.State = PlayerState.Standing;
            Assert.AreEqual(GameStatus.Push, table.GetGameStatus());
        }

        [TestMethod]
        public void TestGetGameStatus_TwoBlackjackPush()
        {
            Table table = CreateBasicTable();
            Card aceClub = new Card() { Type = CardType.Ace, Suit = CardSuit.Club };
            Card jackClub = new Card() { Type = CardType.Jack, Suit = CardSuit.Club };
            table.Player.Hand.Cards = new List<Card>() { aceClub, jackClub };
            table.Dealer.Hand.Cards = new List<Card>() { aceClub, jackClub };
            table.Player.State = PlayerState.Standing;
            table.Dealer.State = PlayerState.Standing;
            Assert.AreEqual(GameStatus.Push, table.GetGameStatus());
        }

        [TestMethod]
        public void TestGetGameStatus_DealerWinsMoreCards()
        {
            Table table = CreateBasicTable();
            Card nineClub = new Card() { Type = CardType.Nine, Suit = CardSuit.Club };
            Card sevenHearts = new Card() { Type = CardType.Seven, Suit = CardSuit.Heart };
            Card threeSpades = new Card() { Type = CardType.Three, Suit = CardSuit.Spade };
            Card threeClubs = new Card() { Type = CardType.Three, Suit = CardSuit.Club };
            Card tenHearts = new Card() { Type = CardType.Ten, Suit = CardSuit.Heart };
            Card sevenDiamonds = new Card() { Type = CardType.Seven, Suit = CardSuit.Diamond };
            table.Player.Hand.Cards = new List<Card>() { nineClub, sevenHearts };
            table.Dealer.Hand.Cards = new List<Card>() { threeSpades, threeClubs, tenHearts, sevenDiamonds};
            table.Player.State = PlayerState.Standing;
            table.Dealer.State = PlayerState.Standing;
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
