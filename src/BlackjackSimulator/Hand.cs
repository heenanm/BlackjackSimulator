using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulator
{
    public class Hand
    {
        private readonly List<Card> _cards;
        private static readonly Dictionary<Rank, int> _rankValues;

        public int BetOnHand { get; private set; }
        public List<Card> Cards => _cards;
        public bool IsSplit = false;
        public bool IsStood = false;
        public bool IsBust => Value > 21;
        public bool IsBlackjack => Value == 21 && _cards.Count == 2;
        public bool IsPair => _cards.Count == 2 && _cards[0].Rank == _cards[1].Rank;
        public int Value
        {
            get
            {
                var value = 0;

                foreach (var card in _cards)
                {
                    value += _rankValues[card.Rank];

                    if (value > 21 && card.Rank == Rank.Ace)
                    {
                        value -= 10;
                    }
                }

                return value;
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

        public void InitialBetOnHand(Player player)
        {
            BetOnHand += player.BetBeforeDeal;
            player.BetBeforeDeal = 0;
        }

        public void DoubleDown(Player player)
        {
            player.PlaceBet(BetOnHand);
            BetOnHand += BetOnHand;
            IsStood = true;
        }

        public void SplitHandBet(Player player, int bet)
        {
            player.PlaceBet(bet);
            BetOnHand += bet;
        }


    }
}
