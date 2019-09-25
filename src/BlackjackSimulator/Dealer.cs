using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackSimulator
{
    public class Dealer
    {
        public Hand Hand;
        public string Name { get; }
        public IReadOnlyCollection<Card> Cards => Hand.Cards;

        public Dealer()
        {
            Name = "Dealer";
        }
        // Dealer actions
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

        public void ShowHand() // now in Hand class.
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

        // Dealer interactions with PLayers
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

        public void AskPlayersToBet(Table table)
        {
            foreach (var player in table.Players)
            {
                if (!player.IsBankrupt)
                {
                    var playerDecision = string.Empty;

                    while (playerDecision == string.Empty)
                    {
                        Console.WriteLine($"{player.PlayerName} Would you like to Bet on this hand? Enter Y or N: ");
                        playerDecision = Console.ReadLine().ToLower();

                        switch (playerDecision)
                        {
                            case "y":
                                player.WantsToPlay = true;
                                break;
                            case "n":
                                player.WantsToPlay = false;
                                break;
                            default:
                                playerDecision = string.Empty;
                                break;
                        }
                    }

                    if (player.WantsToPlay)
                    {
                        var betAmount = 0;
                        while (betAmount < table.MinimumBet)
                        {
                            Console.WriteLine($"Table Minimum is: {table.MinimumBet}, How much would you like to bet?");
                            betAmount = int.Parse(Console.ReadLine());
                            if (betAmount > player.PlayerBank)
                            {
                                Console.WriteLine("Insufficient funds to bet that amount!");
                                betAmount = 0;
                            }
                        }

                        // Player must place bet before cards are dealt.
                        player.PlaceBet(betAmount);
                        player.BetBeforeDeal = betAmount;
                    }
                }
            }
        }

        public void InitialDeal(Table table)
        {
            foreach (var player in table.Players)
            {
                if (player.WantsToPlay && !player.IsBankrupt)
                {
                    var hand = new Hand();
                    hand.InitialBetOnHand(player);
                    player.NumberOfHandsPlayed++;
                    hand.AddCard(table.Shoe.TakeCard());
                    player.PlayerHands.Add(hand);
                }
            }

            Hand = new Hand();
            Hand.AddCard(table.Shoe.TakeCard());

            foreach (var player in table.Players)
            {
                if (player.WantsToPlay && !player.IsBankrupt)
                {
                    player.PlayerHands[0].AddCard(table.Shoe.TakeCard());
                }
            }

            Hand.AddCard(table.Shoe.TakeCard());
        }

        public void AskPlayersPlayAgain(Table table)
        {
            // Dealer asks players if they want to play again
            foreach (var player in table.Players)
            {
                if (!player.IsBankrupt)
                {
                    var playerDecision = string.Empty;

                    while (playerDecision == string.Empty) 
                    {
                        Console.WriteLine($"{player.PlayerName}: Do you want to play again? Enter Y or N: ");
                        playerDecision = Console.ReadLine().ToLower();

                        switch (playerDecision)
                        {
                            case "y":
                                player.WantsToPlay = true;
                                break;
                            case "n":
                                player.WantsToPlay = false;
                                break;
                            default:
                                playerDecision = string.Empty;
                                break;
                        }
                    }
                }
            }
        }

        public void PlayerHitOrStand(Player player, Table table, bool wantsToStand)
        {
            if (wantsToStand)
            {
                Console.Write($"{player.PlayerName} Stood on: {player.Hand.Value}\n\n");
                player.IsStood = true;
                Console.WriteLine("To Continue. Press Any key");
                Console.ReadLine();
                return;
            }

            player.Hand.AddCard(table.Shoe.TakeCard());
            player.ShowHand();

            if (player.Hand.IsBust)
            {
                Console.Write($"{player.PlayerName} has bust! You Lose.\n\n"); //Deal with disposal of cards.
                player.NumberOfLosses++;
                Console.WriteLine("To Continue. Press Any key");
                Console.ReadLine();
            }
        }

        public void CheckForSplitHand(Player player, Table table)
        {
            for (var hand = 0; hand < player.PlayerHands.Count - 1; hand ++) // cant modify this while looping through with foreach changed to for loop.
            {
                if (player.PlayerHands[hand].IsPair && !player.PlayerHands[hand].IsSplit && player.PlayerBank > player.PlayerHands[hand].BetOnHand)
                {
                    var playerDecision = string.Empty;

                    while (playerDecision == string.Empty)
                    {
                        Console.WriteLine($"{player.PlayerName} Would you like to split this hand? Enter Y or N: ");
                        playerDecision = Console.ReadLine().ToLower();

                        switch (playerDecision)
                        {
                            case "y":
                                playerDecision = "y";
                                var splitHand = new Hand();
                                player.PlaceBet(player.PlayerHands[hand].BetOnHand);
                                splitHand.SplitHandBet(player, player.PlayerHands[hand].BetOnHand);
                                splitHand.AddCard(player.PlayerHands[hand].Cards.ElementAt(1));
                                player.PlayerHands[hand].Cards.Remove(player.PlayerHands[hand].Cards.ElementAt(1));
                                splitHand.AddCard(table.Shoe.TakeCard());
                                player.PlayerHands.Add(splitHand);
                                player.PlayerHands[hand].Cards.Add(table.Shoe.TakeCard());
                                player.PlayerHands[hand].IsSplit = true;
                                break;
                            case "n":
                                playerDecision = "n";
                                break;
                            default:
                                playerDecision = string.Empty;
                                break;
                        }
                    }
                }
            }
        }

        public void CheckForBlackjack(Player player, Hand hand) // use this method after refactoring.
        {
            if (hand.IsBlackjack)
            {
                Console.WriteLine($"{player.PlayerName} has Blackjack - You Win!");
                player.DepositWinnings(Convert.ToInt32(player.Hand.BetOnHand * 2.5));
                player.NumberOfWins++;
            }
        }
    }
}