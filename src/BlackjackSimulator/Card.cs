using System;
using System.Collections.Generic;

namespace BlackjackSimulator
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Rank
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
    public class Card
    {
        public Guid Id { get;  }
        public Rank Rank { get; }
        public Suit Suit { get; }

        public Card(Suit suit, Rank rank)
        {
            Id = Guid.NewGuid();
            Suit = suit;
            Rank = rank;
        }
    }
}
