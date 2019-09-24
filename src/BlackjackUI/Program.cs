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

                // Loop through players that have placed a bet and offer hit or stand.
                foreach (var currentPlayer in table.Players)
                {
                    if (currentPlayer.WantsToPlay)
                    {
                        currentPlayer.ShowHand();

                        // If player has Blackjack pay winnings. 
                        if (currentPlayer.Hand.IsBlackjack)
                        {
                            Console.WriteLine("Player has Blackjack - You Win!");
                            currentPlayer.DepositWinnings(Convert.ToInt32(currentPlayer.Hand.BetOnHand * 2.5));
                            break;
                        }

                        // Offer player chance to take more cards.
                        while (!currentPlayer.IsStood && !currentPlayer.Hand.IsBust)
                        {
                            Console.WriteLine("Player Hit or Stand? (Enter H or S): ");

                            var decision = Console.ReadLine();
                            if (decision == "H" || decision == "h")
                            {
                                dealer.PlayerHitOrStand(currentPlayer, table, false);
                            }
                            else if (decision == "S" || decision == "s")
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
                foreach (var currentPlayer in table.Players.Where(x => x.IsStood))
                {
                    if (currentPlayer.Hand.Value > table.TableDealer.Hand.Value || table.TableDealer.Hand.IsBust)
                    {
                        Console.Write($"{currentPlayer.PlayerName} You are a winner!!! \n\n");
                        currentPlayer.DepositWinnings(Convert.ToInt32(currentPlayer.Hand.BetOnHand * 2));
                        currentPlayer.NumberOfWins++;
                    }
                    else
                    {
                        Console.Write($"{currentPlayer.PlayerName} You lose!\n\n");
                        currentPlayer.NumberOfLosses++;
                    }

                    currentPlayer.ShowPlayerStats();
                }
               
                // Ask players is they want to play again. 
                dealer.AskPlayersPlayAgain(table);

                // If no players want to play then end the game.
                if (table.Players.All(x => !x.WantsToPlay))
                {
                    gameActive = false;
                }
            }

            Console.WriteLine("Game Over");
        }
    }
}
