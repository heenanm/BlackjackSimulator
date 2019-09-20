using System;

namespace BlackjackSimulator.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();

            var table = new Table(5);
            
            var player = new Player(10000, "Fred");
            
            table.AddPlayer(player);
            
            // Dealer asks player if they want to play
            Console.WriteLine("Do you want to play this hand?");
            Console.ReadLine();

            table.TableDealer.DealToDealer(table);

            table.TableDealer.DealToPlayer(player, table);
            table.TableDealer.FirstShowHand();
            // Dealer asks if player wants to double bet?


            player.ShowHand();

            if (player.Hand.IsBlackjack) 
            {
                Console.WriteLine("Player has Blackjack - You Win!");
            }

            while (!player.IsStood  && !player.Hand.IsBust && !player.Hand.IsBlackjack)
            {
                Console.WriteLine("Player Hit or Stand? (Enter H or S): ");

                var decision = Console.ReadLine();
                if (decision == "H" || decision == "h")
                {
                    table.TableDealer.PlayerHitOrStand(player, table, false);
                }
                else if (decision == "S" || decision == "s")
                {
                    table.TableDealer.PlayerHitOrStand(player, table, true);
                }
                // Handle invalid input
            }

            if (!player.Hand.IsBust)
            {
                table.TableDealer.PlayDealersHand(table);
            }

            if (player.Hand.Value > table.TableDealer.Hand.Value && !player.Hand.IsBust || table.TableDealer.Hand.IsBust)
            {
                Console.WriteLine("You are a winner!!!");
            }
            else
            {
                Console.WriteLine("You lose!");
            }
        }
    }
}
