using System.Reflection.Metadata;
using NUnit.Framework;

namespace BlackjackSimulator.UnitTests
{
    public class ShoeTests
    {
        [TestCase(1, 1 * 52)]
        [TestCase(2, 2 * 52)]
        [TestCase(3, 3 * 52)]
        [TestCase(4, 4 * 52)]
        [TestCase(5, 5 * 52)]
        [TestCase(6, 6 * 52)]
        public void ShoeTest_ctor(int n, int expected)
        {
            var actual = new Shoe(n);

            Assert.AreEqual(expected, actual.Cards.Count);
        }

        [Test]
        public void ShoeTest_Shuffle()
        {
            //Shoe.SequenceEqual(ShoeAfterShuffle) 
            // TODO check card order changed after shuffle, using Id's
        }
    }
}