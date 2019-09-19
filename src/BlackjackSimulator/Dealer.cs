using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulator
{
    public class Dealer
    {
        public int Bank { get; private set; }
        public Hand Hand;

        public IReadOnlyCollection<Card> Cards => Hand.Cards;
        public Dealer()
        {
            Bank = 10000;
        }

        public void FirstShowHand()
        {
            Console.Write("Dealer Hand: ?? ");
            if (Cards.ElementAt(1).Suit == Suit.Hearts || Cards.ElementAt(1).Suit == Suit.Diamonds)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write($"{Cards.ElementAt(1)}\n");

                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;

                Console.Write($"{Cards.ElementAt(1)}\n");

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

        public void Play(Shoe shoe)
        {
            Hand = new Hand(0);
            Hand.AddCard(shoe.TakeCard());
            Hand.AddCard(shoe.TakeCard());
        }

        public void DealToPlayer(Player player, Shoe shoe)
        {
            //player.Bet(table.minimumBet);
            player.Hand.AddCard(shoe.TakeCard());
            player.Hand.AddCard(shoe.TakeCard());
        }

    }
}