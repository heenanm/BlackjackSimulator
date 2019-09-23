using System.Collections.Generic;
using System.Threading;

namespace BlackjackSimulator
{
    public class Table
    {
        private readonly List<Player> _playersAtTable;

        public readonly int MinimumBet;
        public Dealer TableDealer;
        public Shoe Shoe;

        public List<Player> Players => _playersAtTable;
        public Table(int minimumBet)
        {
            MinimumBet = minimumBet;
            Shoe = new Shoe();
            TableDealer = new Dealer();
            _playersAtTable = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            _playersAtTable.Add(player);
        }
    }
}