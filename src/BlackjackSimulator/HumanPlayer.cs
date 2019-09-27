using System;
using System.Collections.Generic;
using System.Text;

namespace BlackjackSimulator
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(int startingBank, string playerName) 
            : base(startingBank, playerName)
        {
        }

        public override void AskToPlayAgain()
        {
            if (!IsBankrupt)
            {
                var playerDecision = string.Empty;

                while (playerDecision == string.Empty)
                {
                    Console.WriteLine($"{PlayerName}: Do you want to play again? Enter Y or N: ");
                    playerDecision = Console.ReadLine().ToLower();

                    switch (playerDecision)
                    {
                        case "y":
                            WantsToPlay = true;
                            break;
                        case "n":
                            WantsToPlay = false;
                            break;
                        default:
                            playerDecision = string.Empty;
                            break;
                    }
                }
            }
        }

        public override int GetPlayerBetAmount(int minimumBet)
        {
            var betAmount = 0;

            while (betAmount < minimumBet)
            {
                Console.WriteLine($"{PlayerName} Table Minimum is: {minimumBet}, How much would you like to bet?");
                betAmount = int.Parse(Console.ReadLine());
                if (betAmount > PlayerBank)
                {
                    Console.WriteLine("Insufficient funds to bet that amount!");
                    betAmount = 0;
                }
            }

            return betAmount;
        }

        public override bool AskToDouble()
        {
            var playerDecision = string.Empty;
            var playerDoubleBool = false;
            while (playerDecision == string.Empty)
            {
                Console.WriteLine($"{PlayerName} Would you like to double down? Enter Y or N: ");
                playerDecision = Console.ReadLine().ToLower();

                switch (playerDecision)
                {
                    case "y":
                        playerDoubleBool = true;
                        break;
                    case "n":
                        playerDoubleBool = false;
                        break;
                    default:
                        playerDecision = string.Empty;
                        break;
                }
            }
            return playerDoubleBool;
        }
    }
}
