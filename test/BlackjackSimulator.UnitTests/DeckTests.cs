using System.Linq;
using NUnit.Framework;

namespace BlackjackSimulator.UnitTests
{
    public class DeckTests
    {
        [Test]
        public void Deck_ctor()
        {
            // Arrange 
            var deckCards = new Deck();
            
            // Act
            

            // Assert
            Assert.AreEqual(52, deckCards.Cards.Count);
            Assert.True(deckCards.Cards.GroupBy(i => new {i.Suit, i.Rank}).Count() == deckCards.Cards.Count);
        }
    }
}
