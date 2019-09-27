using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackjackSimulator
{
    public class CPUPlayer : Player
    {
        CPUPlayer(int startingBank, string cpuName) : base(startingBank, cpuName)
        {
            this.WantsToPlay = true;
        }

        public void SplitHandCPU(Table table)
        {
            for (var hand = 0; hand <= PlayerHands.Count - 1; hand++)
            {
                if (PlayerHands[hand].IsPair && !PlayerHands[hand].IsSplit && PlayerBank > PlayerHands[hand].BetOnHand)
                {
                    PlayerHands[hand].ShowHand(this);
                    Console.WriteLine($"{PlayerName} Would you like to split this hand? Enter Y or N: ");
                    if (PlayerHands[hand].Cards.ElementAt(0).Rank == Rank.Ace || PlayerHands[hand].Cards.ElementAt(0).Rank == Rank.Eight)
                    {
                        Console.WriteLine("Y");
                        var splitHand = new Hand();
                        splitHand.SplitHandBet(this, PlayerHands[hand].BetOnHand);
                        splitHand.AddCard(PlayerHands[hand].Cards.ElementAt(1));
                        PlayerHands[hand].Cards.Remove(PlayerHands[hand].Cards.ElementAt(1));
                        splitHand.AddCard(table.Shoe.TakeCard());
                        PlayerHands.Add(splitHand);
                        PlayerHands[hand].Cards.Add(table.Shoe.TakeCard());
                        PlayerHands[hand].IsSplit = true;
                        break;
                    }
                    Console.WriteLine("N");
                }
            }
        }

        public void DescisionToDouble()
        {

        }

        public override void AskToPlayAgain()
        {
            WantsToPlay = !IsBankrupt;
        }

        public override int GetPlayerBetAmount(int minimumBet)
        {
                return minimumBet;
        }

        public override bool AskToDouble()
        {
            throw new NotImplementedException();
        }
    }
} 
