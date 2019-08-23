using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BlackjackSimulator
{
    // Implement Rank and add to card. 
    // Create a class called Deck with 52 cards.
    // Pass a collection of decks int class Shoe.
    // Expose a value on card (numerical value type int).
    // Create hand class - Collection of cards. Method add card. Numerical value of hand.
    public class Game
    {
        public static void Play()
        {
            var deck = new Deck();
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
        Ace,
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
        King
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
