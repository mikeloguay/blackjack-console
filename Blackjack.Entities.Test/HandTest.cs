using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Blackjack.Entities.Test
{
    [TestClass]
    public class HandTest
    {
        [TestMethod]
        public void TestGetPoints_TwoNumbers()
        {
            Card fiveHearts = new Card() { Suit = CardSuit.Heart, Type = CardType.Five };
            Card sixDiamonds = new Card() { Suit = CardSuit.Diamond, Type = CardType.Six };

            Hand hand1 = new Hand();
            hand1.Cards = new List<Card>() { fiveHearts, sixDiamonds };

            Assert.AreEqual(11, hand1.GetPoints());
        }

        [TestMethod]
        public void TestGetPoints_WithFaceAndAce()
        {
            Card fiveHearts = new Card() { Suit = CardSuit.Heart, Type = CardType.Five };
            Card queenDiamonds = new Card() { Suit = CardSuit.Diamond, Type = CardType.Queen };
            Card aceSpade = new Card() { Suit = CardSuit.Spade, Type = CardType.Ace };

            Hand hand1 = new Hand();
            hand1.Cards = new List<Card>() { fiveHearts, queenDiamonds, aceSpade };

            Assert.AreEqual(26, hand1.GetPoints());
        }

        [TestMethod]
        public void TestGetPublicPoints_AllVisibles()
        {
            Card fiveHearts = new Card() { Suit = CardSuit.Heart, Type = CardType.Five, Visible = true };
            Card sixDiamonds = new Card() { Suit = CardSuit.Diamond, Type = CardType.Six, Visible = true };

            Hand hand1 = new Hand();
            hand1.Cards = new List<Card>() { fiveHearts, sixDiamonds };

            Assert.AreEqual(11, hand1.GetPublicPoints());
        }

        [TestMethod]
        public void TestGetPublicPoints_OneInvisible()
        {
            Card fiveHearts = new Card() { Suit = CardSuit.Heart, Type = CardType.Five, Visible = true };
            Card sixDiamonds = new Card() { Suit = CardSuit.Diamond, Type = CardType.Six, Visible = false };

            Hand hand1 = new Hand();
            hand1.Cards = new List<Card>() { fiveHearts, sixDiamonds };

            Assert.AreEqual(5, hand1.GetPublicPoints());
        }

        [TestMethod]
        public void TestIsBlackJack_No21()
        {
            Card fiveHearts = new Card() { Suit = CardSuit.Heart, Type = CardType.Five };
            Card sixDiamonds = new Card() { Suit = CardSuit.Diamond, Type = CardType.Six };

            Hand hand1 = new Hand();
            hand1.Cards = new List<Card>() { fiveHearts, sixDiamonds };

            Assert.IsFalse(hand1.IsBlackjack());
        }

        [TestMethod]
        public void TestIsBlackJack_21NoBlackjack()
        {
            Card fiveHearts = new Card() { Suit = CardSuit.Heart, Type = CardType.Five };
            Card sixDiamonds = new Card() { Suit = CardSuit.Diamond, Type = CardType.Six };
            Card tenSpaces = new Card() { Suit = CardSuit.Spade, Type = CardType.Ten };

            Hand hand1 = new Hand();
            hand1.Cards = new List<Card>() { fiveHearts, sixDiamonds, tenSpaces };

            Assert.IsFalse(hand1.IsBlackjack());
        }

        [TestMethod]
        public void TestIsBlackJack_21Blackjack()
        {
            Card aceDiamonds = new Card() { Suit = CardSuit.Diamond, Type = CardType.Ace };
            Card tenSpaces = new Card() { Suit = CardSuit.Spade, Type = CardType.Ten };

            Hand hand1 = new Hand();
            hand1.Cards = new List<Card>() { aceDiamonds, tenSpaces };

            Assert.IsTrue(hand1.IsBlackjack());
        }

        [TestMethod]
        public void TestIsBusted_Positive()
        {
            Card fiveHearts = new Card() { Suit = CardSuit.Heart, Type = CardType.Five };
            Card sixDiamonds = new Card() { Suit = CardSuit.Diamond, Type = CardType.Six };
            Card aceSpades = new Card() { Suit = CardSuit.Spade, Type = CardType.Ace };

            Hand hand1 = new Hand();
            hand1.Cards = new List<Card>() { fiveHearts, sixDiamonds, aceSpades };

            Assert.IsTrue(hand1.IsBust());
        }

        [TestMethod]
        public void TestIsBusted_Negative()
        {
            Card fiveHearts = new Card() { Suit = CardSuit.Heart, Type = CardType.Five };
            Card sixDiamonds = new Card() { Suit = CardSuit.Diamond, Type = CardType.Six };
            Card tenSpaces = new Card() { Suit = CardSuit.Spade, Type = CardType.Ten };

            Hand hand1 = new Hand();
            hand1.Cards = new List<Card>() { fiveHearts, sixDiamonds, tenSpaces };

            Assert.IsFalse(hand1.IsBust());
        }
    }
}
