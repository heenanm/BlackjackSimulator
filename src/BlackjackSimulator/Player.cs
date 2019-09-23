using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulator
{
    public class Player
    {
        private int _playerBank;

        public bool WantsToPlay = true;
        public bool IsStood { get; set; }
        public bool BetPlaced { get; set; }
        public Hand Hand;
        public string PlayerName { get; private set; }
        public int PlayerBank => _playerBank;
        public IReadOnlyCollection<Card> Cards => Hand.Cards;

        public Player(int startingBank, string playerName)
        {
            _playerBank = startingBank;
            PlayerName = playerName;
            IsStood = false;
        }

        public void Split()
        {
            Hand.SplitHand();
        }

        public void PlaceBet(int takeFromBank)
        {
            _playerBank -= takeFromBank;
        }

        public void DepositWinnings(int bankWinnings)
        {
            _playerBank += bankWinnings;
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

            Console.Write($"Current Hand Value: {Hand.Value}\n\n");
            
        }
    }
}