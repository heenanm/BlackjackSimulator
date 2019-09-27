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
            var MaxNumberOfPlayers = 7;

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
                var player = new HumanPlayer(1000, playerName);
                table.AddPlayer(player);
            }
            
            // Initialise the dealer.
            var dealer = table.TableDealer;
            
            // Set game loop to active
            bool gameActive = true;

            //Start game loop 
            while (gameActive)
            {
                // Update start of round player balances.
                foreach (var player in table.Players)
                {
                    player.RoundStartBalance = player.PlayerBank;
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
                        // If player has Blackjack pay winnings. 
                        if (currentPlayer.PlayerHands[0].IsBlackjack)
                        {
                            currentPlayer.PlayerHands[0].ShowHand(currentPlayer);

                            Console.WriteLine($"{currentPlayer.PlayerName} has Blackjack - You Win!");
                            currentPlayer.DepositWinnings(Convert.ToInt32(currentPlayer.PlayerHands[0].BetOnHand * 2.5));
                            currentPlayer.NumberOfWins++;
                            continue;
                        }

                        // If not Blackjack Offer chance to split if option available
                        dealer.CheckForSplitHand(currentPlayer, table);

                        // Now loop through and check all current player hands 
                        foreach (var hand in currentPlayer.PlayerHands)
                        {
                            hand.ShowHand(currentPlayer);
                            // Offer chance to double if funds available
                            if (currentPlayer.PlayerBank >= hand.BetOnHand)
                            {
                                dealer.OfferPlayerChanceToDouble(table, currentPlayer, hand);
                            }

                            // Offer player chance to take more cards.
                            while (!hand.IsStood && !hand.IsBust)
                            {
                                Console.WriteLine($"{currentPlayer.PlayerName} Hit or Stand? (Enter H or S): ");

                                var decision = Console.ReadLine().ToLower();
                                if (decision == "h")
                                {
                                    dealer.PlayerHitOrStand(currentPlayer, hand, table, false);
                                }
                                else if (decision == "s")
                                {
                                    dealer.PlayerHitOrStand(currentPlayer, hand, table, true);
                                }
                            }

                        }
                    }
                }
                    
                // If any players have stood dealer plays their hand.
                if (table.Players.Any(p => p.PlayerHands.Any(h => h.IsStood)))
                {
                    dealer.PlayDealersHand(table);
                }

                // Each player hand is played against the dealers.
                foreach (var currentPlayer in table.Players) 
                {
                    foreach (var hand in currentPlayer.PlayerHands.Where(h => h.IsStood && !h.IsBust))
                    {
                        if (hand.Value > table.TableDealer.Hand.Value || table.TableDealer.Hand.IsBust)
                        {
                            hand.ShowHand(currentPlayer);
                            Console.Write($"{currentPlayer.PlayerName} You are a winner!!! \n\n");
                            currentPlayer.DepositWinnings(hand.BetOnHand * 2);
                            currentPlayer.NumberOfWins++;
                        }
                        else if (hand.Value == table.TableDealer.Hand.Value)
                        {
                            hand.ShowHand(currentPlayer);
                            Console.Write($"{currentPlayer.PlayerName} You are a winner!!! \n\n");
                            currentPlayer.DepositWinnings(hand.BetOnHand * 1);
                            currentPlayer.NumberOfWins++;
                        }
                        else
                        {
                            hand.ShowHand(currentPlayer);
                            Console.Write($"{currentPlayer.PlayerName} You lose!\n\n");
                            currentPlayer.NumberOfLosses++;
                        }

                        if (currentPlayer.PlayerHands.IndexOf(hand) > 0)
                        {
                            currentPlayer.NumberOfHandsPlayed++;
                        }

                        Console.WriteLine("To Continue Press Any key");
                        Console.ReadLine();
                    }
                }

                // Dispose of cards used in the round.
                foreach (var player in table.Players)
                {
                    foreach (var hand in player.PlayerHands)
                    {
                        var cards = hand.Cards;
                        table.Shoe.DisposeCards(cards);
                    }
                    player.PlayerHands.Clear();
                }

                // If less than one deck remains shuffle the shoe
                if (table.Shoe.Cards.Count < 52)
                {
                    table.Shoe.Shuffle();
                }

                // Check player banks and end game if everyone is bankrupt.
                dealer.CheckPlayerBanks(table);

                if(table.Players.All(x => x.IsBankrupt))
                {
                    gameActive = false;
                }

                // Show round statistics.
                foreach (var player in table.Players.Where(x => !x.IsBankrupt))
                {
                    player.ShowPlayerStats();
                    player.ShowPlayerRoundStats();
                    player.ShowPlayerBankStats();
                    Console.WriteLine("");
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
            }

            Console.WriteLine("Game Over");
        }

       
    }
}
