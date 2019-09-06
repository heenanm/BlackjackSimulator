using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulator
{
    public class Shoe
    {
        private readonly List<Card> _disposedCards;
        private readonly List<Card> _cards;
        public IReadOnlyCollection<Card> Cards => _cards;

        public Shoe(int deckCount)
        {
            var cards = new List<Card>();

            for (var i = 0; i < deckCount; i++)
            {
                cards.AddRange(new Deck().Cards);
            }

            _cards = cards;
        }
        
        public void Shuffle()
        {
            _cards.AddRange(_disposedCards);
            _disposedCards.Clear();
            _cards = _cards.OrderBy(c => Guid.NewGuid())
                .ToList();
        }

        public Card TakeCard()
        {
            var card = _cards.First();
            _cards.RemoveAll(c => c.Id == card.Id);

            return card;
        }

        public void DisposeCards(IReadOnlyCollection<Card> cards)
        {
            _disposedCards.AddRange(cards);
        }
    }
}
