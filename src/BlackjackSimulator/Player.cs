using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace BlackjackSimulator
{
    public class Player
    {
        private List<Card> _cards;
        public IReadOnlyCollection<Card> Cards => _cards;
        Player()
        {
            var hand = new Hand();
            var bank = 0;
            
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

        public int Bet()
        {
            return 0;
        }
    }
}