using NUnit.Framework;

namespace BlackjackSimulator.UnitTests
{
    public class DeckTests
    {
        [Test]
        public void DeckTest_ctor()
        {
            var actual = new Deck();

            Assert.AreEqual(52, actual.Cards.Count);
            // TODO test right quantity of card
        }
    }
}
