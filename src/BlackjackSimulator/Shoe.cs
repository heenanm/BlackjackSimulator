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

        public Shoe(IEnumerable<Deck> decks)
        {
            _disposedCards = new List<Card>();

            var cards = new List<Card>();

            foreach (var deck in decks)
            {
                cards.AddRange(deck.Cards);
            }

            _cards = cards;

            Shuffle();
        }

        public void Shuffle()
        {
            var shuffleAttemptCounter = 0;

            while (shuffleAttemptCounter <= 2)
            {
                _cards.AddRange(_disposedCards);
                _disposedCards.Clear();

                var cards = _cards.ToList();
                var shuffledCards = _cards.OrderBy(c => Guid.NewGuid())
                    .ToList();
                _cards.Clear();
                _cards.AddRange(shuffledCards);

                if (cards.SequenceEqual(shuffledCards))
                {
                    shuffleAttemptCounter += 1;
                    continue;
                }

                break;
            }
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
