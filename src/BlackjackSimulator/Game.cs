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
            var score = hand.Value;
        }
    }
}
