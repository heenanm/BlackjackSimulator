using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulator
{
    public class Player
    {
        private int _playerBank;
        private int _bet;
        public Hand Hand;

        public string PlayerName { get; private set; }
        public int PlayerBank => _playerBank;
        public IReadOnlyCollection<Card> Cards => Hand.Cards;

        public Player(int startingBank, string playerName)
        {
            _playerBank = startingBank;
            PlayerName = playerName;
        }

        public void HitOrStand(Shoe shoe, bool wantsToStand)
        {
            if (wantsToStand)
            {
                Console.Write($"Player Stood on: {Hand.Value}");
                return;
            }
            
            Hand.AddCard(shoe.TakeCard());
            ShowHand();
            if (Hand.IsBust)
            {
                Console.WriteLine("Player has bust!"); //Deal with disposal of cards.
            }
        }

        public int Stand()
        {
            return Hand.Value;
        }

        public void Split()
        {
            Hand.SplitHand();
        }

        public bool Bet(int bet)
        {
            var betPlaced = false;

            if (_playerBank >= bet)
            {
                _playerBank -= bet;
                _bet = bet;
                betPlaced = true;
            }

            return betPlaced;
        }

        public void ShowHand()
        {
            // Add in Hand value
            Console.Write("Player Hand: ");
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
            var bet = 5;
            Hand = new Hand(bet);
            _playerBank -= bet;
            Hand.AddCard(shoe.TakeCard());
            Hand.AddCard(shoe.TakeCard());
        }
    }
}