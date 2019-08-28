using NUnit.Framework;

namespace BlackjackSimulator.UnitTests
{
    public class ShoeTests
    {
        [Test]
        public void ShoeTest_ctor()
        {
            var actual = new Shoe(2);

            Assert.AreEqual(104, actual.Cards.Count);
        }

        [Test]
        public void ShoeTest_Shuffle()
        {
            // TODO check card order changed after shuffle, using Id's
        }
    }
}