using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulator
{
    public class Dealer
    {
        public Hand Hand;
        public string Name { get;}
        public IReadOnlyCollection<Card> Cards => Hand.Cards;

        public Dealer()
        {
            Name = "Dealer";
        }

        public void FirstShowHand()
        {
            Console.Write("Dealer Hand: ?? ");
            if (Cards.ElementAt(1).Suit == Suit.Hearts || Cards.ElementAt(1).Suit == Suit.Diamonds)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write($"{Cards.ElementAt(1)}\n\n");

                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;

                Console.Write($"{Cards.ElementAt(1)}\n\n");

                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void ShowHand()
        {
            foreach (var card in Cards)
            {
                if (card.Suit == Suit.Hearts || card.Suit == Suit.Diamonds)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write($"{card} ");

                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.Write($"{card} ");

                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void PlayDealersHand(Table table)
        {
            while (Hand.Value <= 17 && !Hand.IsBust)
            {
                Hand.AddCard(table.Shoe.TakeCard());
            }
            Console.Write($"Dealers Hand: ");
            ShowHand();
            Console.Write($"Dealers Hand Value: {Hand.Value}\n\n");
            if (Hand.IsBust)
            {
                Console.Write("Dealer Busts!\n\n");
            }
        }

        public void CheckPlayerBanks(Table table)
        {
            foreach (var player in table.Players)
            {
                if (player.PlayerBank < table.MinimumBet)
                {
                    player.IsBankrupt = true;
                }
            }
        }

        public void InitialDeal(Table table)
        {
            foreach (var player in table.Players)
            {
                if (player.WantsToPlay && !player.IsBankrupt)
                {
                    player.Hand = new Hand();
                    player.NumberOfHandsPlayed ++;
                    player.Hand.AddCard(table.Shoe.TakeCard());
                }
            }

            Hand = new Hand();
            Hand.AddCard(table.Shoe.TakeCard());

            foreach (var player in table.Players)
            {
                if (player.WantsToPlay && !player.IsBankrupt)
                {
                    player.Hand.AddCard(table.Shoe.TakeCard());
                }
            }

            Hand.AddCard(table.Shoe.TakeCard());
        }

        public void AskPlayersToBet(Table table)
        {
            foreach (var player in table.Players)
            {
                var betAmount = 0;
                if (!player.IsBankrupt)
                Console.WriteLine($"{player.PlayerName} Would you like to Bet on this hand? Enter Y or N: ");
                var playerDecision = Console.ReadLine().ToLower();

                if (playerDecision == "y")
                {
                    player.WantsToPlay = true;
                    while (betAmount < table.MinimumBet)
                    {
                        Console.WriteLine($"Table Minimum is: {table.MinimumBet}, How much would you like to bet?");
                        betAmount = int.Parse(Console.ReadLine());
                    }

                    player.Hand.PlaceBetOnHand(player, betAmount);
                }
                if (playerDecision == "n")
                {
                    player.WantsToPlay = false;
                }
            }
        }

        public void AskPlayersPlayAgain(List<Player> players)
        {
            // Dealer asks players if they want to play again
            foreach (var player in players)
            {
                if(!player.IsBankrupt)
                { 
                    Console.WriteLine($"{player.PlayerName}: Do you want to play again? Enter Y or N: ");
                    var playerDecision = Console.ReadLine();

                    if (playerDecision == "n")
                    {
                        player.WantsToPlay = false;
                    }
                }
            }
           
        }

        public void PlayerHitOrStand(Player player,Table table, bool wantsToStand)
        {
            if (wantsToStand)
            {
                player.ShowHand();
                Console.Write($"Player Stood on: {player.Hand.Value}\n\n");
                player.IsStood = true;
                return;
            }

            player.Hand.AddCard(table.Shoe.TakeCard());
            player.ShowHand();

            if (player.Hand.IsBust)
            {
                Console.Write("Player has bust!\n\n"); //Deal with disposal of cards.
            }
        }



    }
}