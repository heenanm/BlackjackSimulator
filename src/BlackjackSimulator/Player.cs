using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulator
{
    public class Player
    {
        private int _playerBank;

        public int BetBeforeDeal { get; set; }
        public bool IsBankrupt { get; set; }
        public int StartingBalance { get; set; }
        public int NumberOfWins { get; set; }
        public int NumberOfLosses { get; set; }
        public int NumberOfHandsPlayed { get; set; }
        public bool WantsToPlay = true;
        public bool IsStood { get; set; }
        public bool BetPlaced { get; set; }
        public Hand Hand;
        public string PlayerName { get; private set; }
        public int PlayerBank => _playerBank;
        public IReadOnlyCollection<Card> Cards => Hand.Cards;
        public List<Hand> PlayerHands = new List<Hand>();
        public Player(int startingBank, string playerName)
        {
            _playerBank = startingBank;
            PlayerName = playerName;
            IsStood = false;
            NumberOfHandsPlayed = 0;
            NumberOfWins = 0;
            NumberOfLosses = 0;
            StartingBalance = startingBank;
            IsBankrupt = false;
            BetBeforeDeal = 0;
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
            Console.Write($"{PlayerName} Hand: ");
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

            Console.Write($"Current Hand Value: {Hand.Value} Current bet on Hand {Hand.BetOnHand}\n\n");
            
        }

        public void ShowPlayerStats()
        {
            Console.Write($"{PlayerName} Current Statistics:\nHands Played: {NumberOfHandsPlayed}, Hands Won: {NumberOfWins}, Hands Lost {NumberOfLosses}\n\n");
        }
    }
}