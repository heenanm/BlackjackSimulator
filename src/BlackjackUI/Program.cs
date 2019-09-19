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
            var table = new Table();
            
            var player = new Player(10000, "Fred");
            
            table.AddPlayer(player);
            
            // Dealer asks player if they want to play
            Console.WriteLine("Do you want to play this hand?");
            Console.ReadLine();

            player.Play(table.Shoe);

            table.TableDealer.Play(table.Shoe);
            table.TableDealer.FirstShowHand();
            // Dealer asks if player wants to double bet?


            player.ShowHand();

            if (player.Hand.IsBlackjack) 
            {
                Console.WriteLine("Player has Blackjack - You Win!");
            }

            Console.WriteLine("Player Hit or Stand? (Enter H or S): ");

            var decision = Console.ReadLine();
            if (decision == "H" || decision == "h")
            {
                player.HitOrStand(table.Shoe, false);
            }
            else if (decision == "S" || decision == "s")
            {
                player.HitOrStand(table.Shoe, true);
            }
            // Handle invalid input


        }
    }
}
