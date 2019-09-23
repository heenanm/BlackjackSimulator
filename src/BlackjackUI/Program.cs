using System;

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

            //Initialise Variables to play the game
            var table = new Table(10);
            Console.Write($"Minimum bet at this table is: {table.MinimumBet}\n\n");

            //Console.WriteLine("Enter number of Players 1 - 7: ");
            //var numberOfPlayers = Console.ReadLine();

            var player = new Player(10000, "Fred");
            var player1 = new Player(10000, "John");
            var dealer = table.TableDealer;
            table.AddPlayer(player);
            table.AddPlayer(player1);

            //Start game loop 
            while (player.PlayerBank > table.MinimumBet && player.WantsToPlay)
            {

                player.IsStood = false;

                dealer.AskPlayersToBet(table);

                dealer.InitialDeal(table);

                dealer.FirstShowHand();

                foreach (var currentPlayer in table.Players)
                {
                    currentPlayer.ShowHand();
                }


                

                if (player.Hand.IsBlackjack) 
                {
                    Console.WriteLine("Player has Blackjack - You Win!");
                    player.DepositWinnings(Convert.ToInt32(player.Hand.BetOnHand * 2.5));
                }

                while (!player.IsStood  && !player.Hand.IsBust && !player.Hand.IsBlackjack)
                {
                    Console.WriteLine("Player Hit or Stand? (Enter H or S): ");

                    var decision = Console.ReadLine();
                    if (decision == "H" || decision == "h")
                    {
                        dealer.PlayerHitOrStand(player, table, false);
                    }
                    else if (decision == "S" || decision == "s")
                    {
                        dealer.PlayerHitOrStand(player, table, true);
                    }
                }

                if (!player.Hand.IsBust)
                {
                    dealer.PlayDealersHand(table);
                }

                if (player.Hand.Value > table.TableDealer.Hand.Value && !player.Hand.IsBust || table.TableDealer.Hand.IsBust)
                {
                    Console.Write("You are a winner!!! \n\n");
                    player.DepositWinnings(Convert.ToInt32(player.Hand.BetOnHand * 2));
                    player.NumberOfWins ++;
                }
                else
                {
                    Console.Write("You lose!\n\n");
                    player.NumberOfLosses ++;
                }

                foreach (var currentPlayer in table.Players)
                {
                    currentPlayer.ShowPlayerStats();
                }

                dealer.AskPlayersPlayAgain(table.Players);
            }

            Console.WriteLine("Game Over");

        }
    }
}
