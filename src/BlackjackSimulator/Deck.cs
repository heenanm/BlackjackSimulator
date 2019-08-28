using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulator
{
    public class Deck
    {
        public IReadOnlyCollection<Card> Cards { get; }

        public Deck()
        {
            var cards = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
            {
                foreach (var rank in Enum.GetValues(typeof(Rank)).Cast<Rank>())
                {
                    cards.Add(new Card(suit, rank));
                }
            }

            Cards = cards;
        }
    }
}
