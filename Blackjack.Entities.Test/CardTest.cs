using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Entities.Test
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void TestIsFace_Positive()
        {
            Card diamondQueen = new Card() { Type = CardType.Queen, Suit = CardSuit.Diamond };
            Assert.IsTrue(diamondQueen.IsFace);
        }

        [TestMethod]
        public void TestIsFace_Negative()
        {
            Card diamonAce = new Card() { Type = CardType.Ace, Suit = CardSuit.Diamond };
            Assert.IsFalse(diamonAce.IsFace);
        }

        [TestMethod]
        public void TestGetPoints_Number()
        {
            Card diamondsSix = new Card() { Type = CardType.Six, Suit = CardSuit.Diamond };
            Assert.AreEqual(6, diamondsSix.GetPoints());
        }

        [TestMethod]
        public void TestGetPoints_Ace()
        {
            Card diamondsAce = new Card() { Type = CardType.Ace, Suit = CardSuit.Diamond };
            Assert.AreEqual(11, diamondsAce.GetPoints());
        }

        [TestMethod]
        public void TestGetPoints_Face()
        {
            Card diamondsJack = new Card() { Type = CardType.Jack, Suit = CardSuit.Diamond };
            Assert.AreEqual(10, diamondsJack.GetPoints());
        }
    }
}
