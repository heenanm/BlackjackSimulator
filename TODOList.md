## Blackjack Simulator

Create a console application to simulate the game of Blackjack using Object-Oriented principles.

### Requirements:

* CPU dealer

  * Stop dealing cards to dealer when their hand is 17 or more
* Human player
    * Start with an initial balance
    * Bet on a hand (before the hand has been dealt)
    * Prevent the player from betting if insufficient funds are available
    * Award the player (bet x 2) if they beat the dealer (hand is greater than the dealer)
    * Award the player (bet x 2.5) if they have Blackjack (Ace + 10) immediately after being dealt the hand
    * Punish the player (bet) if they lose to the dealer (hand is less than the dealer)
    * Punish the player (bet) if they go bust (over 21)
    * Option to split cards of the same rank (immediately after being dealt the hand)
    * Option to double the bet to create a second hand (immediately after being dealt the hand)
* Misc
  
  * ~~There are four suits (Clubs, Diamonds, Hearts, Spades)~~
  * ~~There are 13 ranks (2, 3, 4, 5, 6, 7, 8, 9, 10, J, Q, K, A)~~
  * ~~A deck consists of 52 cards (4 x 13)~~
  * ~~A shoe consists of a configurable amount of decks (default to 4)~~

* UI
  * Show each hand with colour depending on the suit (red/black)
  * Show the numerical value for each hand
  * Show the bet associated with each hand
  * Show the amount won/lost after each game
  * Show the number of hands played for each player
  * Show the number of wins for each player
  * Show the number of losses for each player
  * Show the original vs current balance for each player (and whether they're up or down)
* CPU player
  
  - Play accordingly to the [Blackjack Strategy](https://wizardofodds.com/games/blackjack/strategy/4-decks/#targetText=To%20use%20the%20basic%20strategy,soft%20totals%2C%20and%20splittable%20hands.)
  
    