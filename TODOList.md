# Blackjack Simulator

Create a console application to simulate the game of Blackjack using Object-Oriented principles.

## Game

* ~~Each card has a suit and a rank~~
* ~~There are four suits (Clubs, Diamonds, Hearts, Spades)~~
* ~~There are 13 ranks (2, 3, 4, 5, 6, 7, 8, 9, 10, J, Q, K, A)~~
* ~~The value of each card should be shortened to improve readability (e.g. AS = Ace of Spaces)~~
* ~~A deck consists of 52 cards (one for each Suit and Rank combination)~~
* ~~A shoe consists of a configurable amount of decks (default to 4)~~
* ~~A table has a dealer~~
* A table has one or more players
* ~~A table has a minimum bet~~
* ~~Each player has a hand~~
* ~~A hand consists of 2 or more cards~~
* ~~Jack, Queen, and King cards have a value of 10~~
* ~~Ace cards have a value of 1 or 11 (11 unless the value of the hand goes above 21)~~
* ~~The dealer is responsible for dealing cards from the shoe to each player and themself~~
* The dealer competes against each player individually
* ~~The dealer must must deal themself cards until they reach 17 or above~~
* Each player starts with an opening balance
* Each player is trying beat the dealer (they aren't in direct competition with each other)
* Each player must place a bet before the hands are dealt
* Players can't play if they don't have sufficient funds
* Each player is dealt their first card followed by the dealer before repeating the process for the second card
* Once all of the hands have been dealt, players take it in turns before the dealer gets their turn
* ~~Players go bust if the value of their hand is above 21~~
* ~~The dealer goes bust if the value of their hand is above 21~~
* ~~Players have Blackjack if their hand consists of a card with the value of 10 (10, J, Q, K) and an Ace~~
* If a player beats the dealer, they win 2 x bet
* If a player has Blackjack, they win 2.5 x bet
* If a player loses to the dealer, then lose 1 x bet
* If a player draws with the dealer, they win 1 x bet
* ~~If a player chooses to hit, the dealer will deal them another card from the shoe~~
* If a player chooses to stand, the dealer moves on to the next player
* If a player chooses to double (immediately after being dealt the hand or splitting the hand), they double the bet and only one subsequent card is dealt
	* The player must have sufficient funds
* If a player chooses to split (if the hand consists of two cards of the same rank), the hand is split and the player places the same bet on the second hand before two additional cards are dealt (one into each hand)
	* The player must have sufficient funds

## Requirements:

* UI
	* ~~Show each hand with colour depending on the suit (red/black)~~
	* ~~Show the numerical value for each hand~~
	* Show the bet associated with each hand
	* Show the amount won/lost after each game
	* Show the number of hands played for each player
	* Show the number of wins for each player
	* Show the number of losses for each player
	* Show the original vs current balance for each player (and whether they're up or down)
* Human player
* CPU dealer
* CPU player
	* Play accordingly to the [Blackjack Strategy](https://wizardofodds.com/games/blackjack/strategy/4-decks/#targetText=To%20use%20the%20basic%20strategy,soft%20totals%2C%20and%20splittable%20hands.)

