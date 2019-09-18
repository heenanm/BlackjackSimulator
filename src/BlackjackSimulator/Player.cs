using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace BlackjackSimulator
{
    public class Player
    {
        private List<Card> _cards;
        private int _playerBank;
        private int _bet;
        private Hand _hand;

        public int PlayerBank => _playerBank;
        public IReadOnlyCollection<Card> Cards => _cards;
        Player(int startingBank)
        {
            _playerBank = startingBank;
            var hand = new Hand();
            _hand = hand;
        }

        public void Hit()
        {

        }

        public void Stand()
        {

        }

        public void Split()
        {

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
    }
}