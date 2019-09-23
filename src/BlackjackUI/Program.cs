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
            var player = new Player(10000, "Fred");
            var dealer = table.TableDealer;
            table.AddPlayer(player);

            //Start game loop 
            while (player.PlayerBank > table.MinimumBet && player.WantsToPlay)
            {

                player.IsStood = false;

                dealer.InitialDeal(table);
            
                dealer.FirstShowHand();
                // Dealer asks if player wants to double bet?


                player.ShowHand();

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
                    Console.WriteLine("You are a winner!!!");
                    player.DepositWinnings(Convert.ToInt32(player.Hand.BetOnHand * 2));

                }
                else
                {
                    Console.WriteLine("You lose!");
                }

                dealer.AskPlayersPlayAgain(table.Players);
            }

            Console.WriteLine("Game Over");

        }
    }
}
