using System;

namespace BlackjackSimulator
{
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

        public override bool Equals(object obj)
        {
            if (!(obj is Card card))
            {
                return false;
            }

            return Id == card.Id;
        }

        public override string ToString()
        {
            return $"{Rank.GetDescription()}{Suit.GetDescription()}";
        }
    }
}
