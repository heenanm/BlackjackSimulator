using System;
using System.Linq;

namespace BlackjackSimulator.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set console colors
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();

            // Initialise Variables to play the game
            var table = new Table(10);
            Console.Write($"Minimum bet at this table is: {table.MinimumBet}\n\n");

            // Initialise players at the table.
            var numberOfPlayers = 0;
            while (numberOfPlayers <= 0 || numberOfPlayers > 7)
            {
                Console.WriteLine("Enter number of Players 1 - 7: ");
                numberOfPlayers = int.Parse(Console.ReadLine());
            }

            for (int i = 1; i <= numberOfPlayers; i++)
            {
                Console.WriteLine($"Enter Player {i} Name: ");
                var playerName = Console.ReadLine();
                var player = new Player(1000, playerName);
                table.AddPlayer(player);
            }
            
            // Initialise the dealer.
            var dealer = table.TableDealer;
            
            // Set game loop to active
            bool gameActive = true;

            //Start game loop 
            while (gameActive)
            {
                // Reset player stood property
                foreach (var currentPlayer in table.Players)
                {
                    currentPlayer.IsStood = false;
                }

                // Dealer asks players for initial bets.
                dealer.AskPlayersToBet(table);

                // Dealer performs initial deal. 
                dealer.InitialDeal(table);

                // If any players want to play dealer shows his face card.
                if (table.Players.Any(x => x.WantsToPlay))
                {
                    dealer.FirstShowHand();
                }

                // Loop through players that have placed a bet.
                foreach (var currentPlayer in table.Players)
                {
                    if (currentPlayer.WantsToPlay)
                    {
                        currentPlayer.PlayerHands[0].ShowHand(currentPlayer);

                        // If player has Blackjack pay winnings. 
                        if (currentPlayer.PlayerHands[0].IsBlackjack)
                        {
                            Console.WriteLine($"{currentPlayer.PlayerName} has Blackjack - You Win!");
                            currentPlayer.DepositWinnings(Convert.ToInt32(currentPlayer.PlayerHands[0].BetOnHand * 2.5));
                            currentPlayer.NumberOfWins++;
                            break;
                        }

                        // If not Blackjack Offer chance to split if option available
                        dealer.CheckForSplitHand(currentPlayer, table);

                        // Now loop through and check all current player hands 
                        foreach (var hand in currentPlayer.PlayerHands)
                        {
                            // Offer chance to double if funds available
                            if (currentPlayer.PlayerBank >= hand.BetOnHand)
                            {
                                var playerDecision = string.Empty;

                                while (playerDecision == string.Empty)
                                {
                                    Console.WriteLine($"{currentPlayer.PlayerName} Would you like to double down? Enter Y or N: ");
                                    playerDecision = Console.ReadLine().ToLower();

                                    switch (playerDecision)
                                    {
                                        case "y":
                                            hand.DoubleDown(currentPlayer);
                                            hand.IsStood = true;
                                            hand.AddCard(table.Shoe.TakeCard());
                                            Console.WriteLine($"Bet on hand now {hand.BetOnHand}");
                                            hand.ShowHand(currentPlayer); // changed showhand method
                                            break;
                                        case "n":
                                            break;
                                        default:
                                            playerDecision = string.Empty;
                                            break;
                                    }
                                }
                                    if (hand.IsBust)
                                    {
                                        Console.Write($"{currentPlayer.PlayerName} has bust! You Lose.\n\n"); //Deal with disposal of cards.
                                        currentPlayer.NumberOfLosses++;
                                    }

                                    if (hand.IsStood)
                                    {
                                        Console.WriteLine($"{currentPlayer.PlayerName} stood on {hand.Value}");
                                    }

                                    Console.WriteLine("To Continue. Press Any key");
                                    Console.ReadLine();
                            }

                            // Offer player chance to take more cards.
                            while (!currentPlayer.IsStood && !hand.IsBust)
                            {
                                Console.WriteLine($"{currentPlayer.PlayerName} Hit or Stand? (Enter H or S): ");

                                var decision = Console.ReadLine().ToLower();
                                if (decision == "h")
                                {
                                    dealer.PlayerHitOrStand(currentPlayer, table, false);
                                }
                                else if (decision == "s")
                                {
                                    dealer.PlayerHitOrStand(currentPlayer, table, true);
                                }
                            }
                        }




                        // Offer chance to double if funds available
                        if (currentPlayer.PlayerBank >= currentPlayer.Hand.BetOnHand)
                        {
                            var playerDecision = string.Empty;

                            while (playerDecision == string.Empty)
                            {
                                Console.WriteLine($"{currentPlayer.PlayerName} Would you like to double down? Enter Y or N: ");
                                playerDecision = Console.ReadLine().ToLower();

                                switch (playerDecision)
                                {
                                    case "y":
                                        currentPlayer.Hand.DoubleDown(currentPlayer);
                                        currentPlayer.IsStood = true;
                                        currentPlayer.Hand.AddCard(table.Shoe.TakeCard());
                                        Console.WriteLine($"Bet on hand now {currentPlayer.Hand.BetOnHand}");
                                        currentPlayer.ShowHand();
                                        break;
                                    case "n":
                                        break;
                                    default:
                                        playerDecision = string.Empty;
                                        break;
                                }
                                if (currentPlayer.Hand.IsBust)
                                {
                                    Console.Write($"{currentPlayer.PlayerName} has bust! You Lose.\n\n"); //Deal with disposal of cards.
                                    currentPlayer.NumberOfLosses++;
                                }
                                else
                                {
                                    Console.WriteLine($"{currentPlayer.PlayerName} stood on {currentPlayer.Hand.Value}");
                                }

                                Console.WriteLine("To Continue. Press Any key");
                                Console.ReadLine();
                            }
                        }

                        // Offer player chance to take more cards.
                        while (!currentPlayer.IsStood && !currentPlayer.Hand.IsBust)
                        {
                            Console.WriteLine($"{currentPlayer.PlayerName} Hit or Stand? (Enter H or S): ");

                            var decision = Console.ReadLine().ToLower();
                            if (decision == "h")
                            {
                                dealer.PlayerHitOrStand(currentPlayer, table, false);
                            }
                            else if (decision == "s")
                            {
                                dealer.PlayerHitOrStand(currentPlayer, table, true);
                            }
                        }
                    }
                }


                // If any players have stood dealer plays their hand.
                if (table.Players.Any(x => x.IsStood))
                {
                    dealer.PlayDealersHand(table);
                }

                // Each player hand is played against the dealers.
                foreach (var currentPlayer in table.Players.Where(x => x.IsStood && !x.Hand.IsBust))
                {
                    if (currentPlayer.Hand.Value > table.TableDealer.Hand.Value || table.TableDealer.Hand.IsBust)
                    {
                        currentPlayer.ShowHand();
                        Console.Write($"{currentPlayer.PlayerName} You are a winner!!! \n\n");
                        currentPlayer.DepositWinnings(Convert.ToInt32(currentPlayer.Hand.BetOnHand * 2));
                        currentPlayer.NumberOfWins++;
                    }
                    else if (currentPlayer.Hand.Value == table.TableDealer.Hand.Value)
                    {
                        currentPlayer.ShowHand();
                        Console.Write($"{currentPlayer.PlayerName} You are a winner!!! \n\n");
                        currentPlayer.DepositWinnings(Convert.ToInt32(currentPlayer.Hand.BetOnHand * 1));
                        currentPlayer.NumberOfWins++;
                    }
                    else
                    {
                        currentPlayer.ShowHand();
                        Console.Write($"{currentPlayer.PlayerName} You lose!\n\n");
                        currentPlayer.NumberOfLosses++;
                    }

                    currentPlayer.ShowPlayerStats();
                }

                // Check player banks and end game if everyone is bankrupt.
                dealer.CheckPlayerBanks(table);

                if(table.Players.All(x => x.IsBankrupt))
                {
                    gameActive = false;
                }

                // If players are not bankrupt ask players is they want to play again. 
                if (table.Players.Any(x => !x.IsBankrupt))
                {
                    dealer.AskPlayersPlayAgain(table);
                }

                // If no players want to play then end the game.
                if (table.Players.All(x => !x.WantsToPlay))
                {
                    gameActive = false;
                }

                dealer.CheckPlayerBanks(table);
            }

            Console.WriteLine("Game Over");
        }
    }
}
