using System.Linq;
using NUnit.Framework;

namespace BlackjackSimulator.UnitTests
{
    class CardTests
    {
        [Test]
        public void Card_ctor()
        {
            // Arrange
            var card = new Card(Suit.Clubs, Rank.Ace);

            // Act 

            //Assert
            Assert.IsInstanceOf(typeof(Card), card);
            Assert.True(card.GetType().GetProperty("Id") != null);
            Assert.True(card.GetType().GetProperty("Rank") != null);
            Assert.True(card.GetType().GetProperty("Suit") != null);
        }
    }
}
