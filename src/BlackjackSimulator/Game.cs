using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulator
{
    public class Game
    {
        public bool WantsToPlay { get; private set; }

        public Game()
        {
            var table = new Table(5);
        }

        public void DecidetoPlay()
        {
            var playerDecision = "";

            while (playerDecision !="y"||playerDecision != "n")
            {
                Console.WriteLine("Would you like to Play Blackjack? Enter Y or N: ");
                playerDecision = Console.ReadLine().ToLower();

            }

            if (playerDecision == "y")
            {
                WantsToPlay = true;
            }

            if (playerDecision == "n")
            {
                return;
            }

        }
    }
}
