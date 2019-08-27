using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulator
{
    public class Game
    {
        public static void Play()
        {
            var deck = new Deck();
            var hand = new Hand();
            hand.AddCard(new Card(Suit.Clubs, Rank.Ace));
            var score = hand.Score;
        }
    }

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
        public Rank Rank { get; }
        public Suit Suit { get; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }

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

    public class Hand
    {
        private readonly List<Card> _cards;
        private static readonly Dictionary<Rank, int> _rankValues;

        public IReadOnlyCollection<Card> Cards => _cards;

        public bool IsBust => Score > 21;

        public int Score
        {
            get
            {
                var score = 0;

                foreach (var card in _cards)
                {
                    score += _rankValues[card.Rank];

                    if (score > 21 && card.Rank == Rank.Ace)
                    {
                        score -= 10;
                    }
                }

                return score;
            }
        }

        static Hand()
        {
            _rankValues = new Dictionary<Rank, int>
            {
                { Rank.Two, 2 },
                { Rank.Three, 3 },
                { Rank.Four, 4 },
                { Rank.Five, 5 },
                { Rank.Six, 6 },
                { Rank.Seven, 7 },
                { Rank.Eight, 8 },
                { Rank.Nine, 9 },
                { Rank.Ten, 10 },
                { Rank.Jack, 10 },
                { Rank.Queen, 10 },
                { Rank.King, 10 },
                { Rank.Ace, 11 },
            };
        }

        public Hand()
        {
            _cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }
    }

    public class Shoe
    {
        public IReadOnlyCollection<Card> Cards { get; }

        public Shoe(int deckCount)
        {
            var cards = new List<Card>();

            for (var i = 0; i < deckCount; i++)
            {
                cards.AddRange(new Deck().Cards);
            }

            Cards = cards;
        }
    }

    public class Table
    {
    }

    public class Player
    {
    }

    public class Dealer
    {
    }
}
