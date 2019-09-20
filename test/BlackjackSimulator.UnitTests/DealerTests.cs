using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using NUnit.Framework;

namespace BlackjackSimulator.UnitTests
{
    class DealerTests
    {
        [Test]
        public void dealerBusts()
        {
            // Arrange
            var Bust = true;

            var dealer = new Dealer();
            var card = new Card(Suit.Spades, Rank.Ten);

            dealer.Hand.AddCard(card);
            dealer.Hand.AddCard(card);
            dealer.Hand.AddCard(card);

            // Act
            var DealerIsBust = dealer.Hand.IsBust;

            //Assert

            Assert.AreEqual(Bust , DealerIsBust);
        }
    }
}
