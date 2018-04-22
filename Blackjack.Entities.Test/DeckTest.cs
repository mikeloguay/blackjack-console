using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Entities.Test
{
    [TestClass]
    public class DeckTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Deck deck = new Deck();
            Assert.AreEqual(52, deck.Cards.Count);
        }
    }
}
