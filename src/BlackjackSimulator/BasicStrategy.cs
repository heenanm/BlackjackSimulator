using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackjackSimulator
{
    public class BasicStrategy
    {
        public PlayerDecision GetDecision(Hand hand, Dealer dealer)
        {
            if (hand.IsPair)
            {
                if (hand.Cards[0].Rank == Rank.Ace || hand.Cards[0].Rank == Rank.Eight)
                {
                    return PlayerDecision.Split;
                }

                if ((hand.Cards[0].Rank == Rank.Two || hand.Cards[0].Rank == Rank.Three) && (dealer.Hand.Cards[1].Rank == Rank.Two || dealer.Hand.Cards[1].Rank == Rank.Three || dealer.Hand.Cards[1].Rank == Rank.Four || dealer.Hand.Cards[1].Rank == Rank.Five || dealer.Hand.Cards[1].Rank == Rank.Six || dealer.Hand.Cards[1].Rank == Rank.Seven))
                {
                    return PlayerDecision.Split;
                }

                if (hand.Cards[0].Rank == Rank.Four && (dealer.Hand.Cards[1].Rank == Rank.Five || dealer.Hand.Cards[1].Rank == Rank.Six))
                {
                    return PlayerDecision.Split;
                }

                if (hand.Cards[0].Rank == Rank.Six && (dealer.Hand.Cards[1].Rank == Rank.Two || dealer.Hand.Cards[1].Rank == Rank.Three || dealer.Hand.Cards[1].Rank == Rank.Four || dealer.Hand.Cards[1].Rank == Rank.Five || dealer.Hand.Cards[1].Rank == Rank.Six))
                {
                    return PlayerDecision.Split;
                }

                if (hand.Cards[0].Rank == Rank.Seven && (dealer.Hand.Cards[1].Rank == Rank.Two || dealer.Hand.Cards[1].Rank == Rank.Three || dealer.Hand.Cards[1].Rank == Rank.Four || dealer.Hand.Cards[1].Rank == Rank.Five || dealer.Hand.Cards[1].Rank == Rank.Six || dealer.Hand.Cards[1].Rank == Rank.Seven))
                {
                    return PlayerDecision.Split;
                }

                if (hand.Cards[0].Rank == Rank.Nine && (dealer.Hand.Cards[1].Rank == Rank.Two || dealer.Hand.Cards[1].Rank == Rank.Three || dealer.Hand.Cards[1].Rank == Rank.Four || dealer.Hand.Cards[1].Rank == Rank.Five || dealer.Hand.Cards[1].Rank == Rank.Six || dealer.Hand.Cards[1].Rank == Rank.Eight || dealer.Hand.Cards[1].Rank == Rank.Nine))
                {
                    return PlayerDecision.Split;
                }
            }

            var IsSoft = hand.Cards.Any(x => x.Rank == Rank.Ace);

            if (hand.Cards.Count == 2)
            {
                if ((hand.Value == 9 && !IsSoft) && (dealer.Hand.Cards[1].Rank == Rank.Three || dealer.Hand.Cards[1].Rank == Rank.Four || dealer.Hand.Cards[1].Rank == Rank.Five || dealer.Hand.Cards[1].Rank == Rank.Six))
                {
                    return PlayerDecision.Double;
                }

                if ((hand.Value == 10 && !IsSoft) && !(dealer.Hand.Cards[1].Rank == Rank.Ten || dealer.Hand.Cards[1].Rank == Rank.Jack || dealer.Hand.Cards[1].Rank == Rank.Queen || dealer.Hand.Cards[1].Rank == Rank.King || dealer.Hand.Cards[1].Rank == Rank.Ace))
                {
                    return PlayerDecision.Double;
                }

                if ((hand.Value == 11 && !IsSoft) && !(dealer.Hand.Cards[1].Rank == Rank.Ace))
                {
                    return PlayerDecision.Double;
                }

                if((hand.Value == 13 || hand.Value == 14 && IsSoft) && (dealer.Hand.Cards[1].Rank == Rank.Five || dealer.Hand.Cards[1].Rank == Rank.Six))
                {
                    return PlayerDecision.Double;
                }

                if ((hand.Value == 15 || hand.Value == 16 && IsSoft) && (dealer.Hand.Cards[1].Rank == Rank.Four ||dealer.Hand.Cards[1].Rank == Rank.Five || dealer.Hand.Cards[1].Rank == Rank.Six))
                {
                    return PlayerDecision.Double;
                }

                if ((hand.Value == 17 || hand.Value == 18 && IsSoft) && (dealer.Hand.Cards[1].Rank == Rank.Three || dealer.Hand.Cards[1].Rank == Rank.Four || dealer.Hand.Cards[1].Rank == Rank.Five || dealer.Hand.Cards[1].Rank == Rank.Six))
                {
                    return PlayerDecision.Double;
                }
            }

            if (hand.Value <= 11 && !IsSoft)
            {
                return PlayerDecision.Hit;
            }

            if ((hand.Value == 12 && !IsSoft) && !(dealer.Hand.Cards[1].Rank == Rank.Four || dealer.Hand.Cards[1].Rank == Rank.Five || dealer.Hand.Cards[1].Rank == Rank.Six))
            {
                return PlayerDecision.Hit;
            }

            if ((hand.Value >= 13 && hand.Value <= 16 && !IsSoft) && !(dealer.Hand.Cards[1].Rank == Rank.Two ||dealer.Hand.Cards[1].Rank == Rank.Three ||dealer.Hand.Cards[1].Rank == Rank.Four || dealer.Hand.Cards[1].Rank == Rank.Five || dealer.Hand.Cards[1].Rank == Rank.Six))
            {
                return PlayerDecision.Hit;
            }

            if (hand.Value <= 17 && IsSoft)
            {
                return PlayerDecision.Hit;
            }

            if ((hand.Value == 18 && IsSoft) && (dealer.Hand.Cards[1].Rank == Rank.Nine || dealer.Hand.Cards[1].Rank == Rank.Ten || dealer.Hand.Cards[1].Rank == Rank.Jack || dealer.Hand.Cards[1].Rank == Rank.Queen || dealer.Hand.Cards[1].Rank == Rank.King || dealer.Hand.Cards[1].Rank == Rank.Ace))
            {
                return PlayerDecision.Hit;
            }

            return PlayerDecision.Stand;
        }
    }
}
