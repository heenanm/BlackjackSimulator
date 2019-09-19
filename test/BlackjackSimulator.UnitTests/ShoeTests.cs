using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BlackjackSimulator.UnitTests
{
    public class ShoeTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void Shoe_ctor(int deckCount)
        {
            // Arrange
            var decks = new List<Deck>();

            for (var i = 0; i < deckCount; i++)
            {
                decks.Add(new Deck());
            }

            var cards = decks.SelectMany(d => d.Cards.ToList())
                .ToList();

            // Act
            //var shoe = new Shoe(decks);
            var shoe = new Shoe(deckCount);

            // Assert
            Assert.AreEqual(cards.Count, shoe.Cards.Count);
            Assert.That(cards, Is.EquivalentTo(shoe.Cards));
            Assert.False(cards.SequenceEqual(shoe.Cards));
        }

        [Test]
        public void Shoe_Shuffle()
        {
            // Arrange
            //var shoe = new Shoe(new [] { new Deck() });
            var shoe = new Shoe();
            var cards = shoe.Cards.ToList();

            // Act
            shoe.Shuffle();

            // Assert
            var shuffledCards = shoe.Cards.ToList();

            Assert.AreEqual(cards.Count, shuffledCards.Count);
            Assert.That(cards, Is.EquivalentTo(shuffledCards));
            Assert.False(cards.SequenceEqual(shuffledCards));
            cards.SequenceEqual(shuffledCards).Should().BeFalse();
        }

        [Test]
        public void Shoe_TakeCard()
        {
            // Arrange
            //var shoe = new Shoe(new[] { new Deck() });
            var shoe = new Shoe();
            var cards = shoe.Cards.ToList();
            
            // Act
            var card = shoe.TakeCard();

            // Assert 
            Assert.AreEqual(cards.Count - 1, shoe.Cards.Count);
            Assert.AreEqual(cards[0], card);
            Assert.False(shoe.Cards.Contains(card));
        }
    }
}