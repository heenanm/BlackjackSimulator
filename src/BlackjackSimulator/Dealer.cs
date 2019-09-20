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

        public void DealToPlayer(Player player, Table table)
        {
            player.Hand = new Hand();
            player.Hand.Bet(player, table.MinimumBet);
            player.Hand.AddCard(table.Shoe.TakeCard());
            player.Hand.AddCard(table.Shoe.TakeCard());
        }

        public void DealToDealer(Table table)
        {
            Hand = new Hand();
            Hand.AddCard(table.Shoe.TakeCard());
            Hand.AddCard(table.Shoe.TakeCard());
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