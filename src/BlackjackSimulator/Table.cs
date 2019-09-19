using System.Collections.Generic;
using System.Threading;

namespace BlackjackSimulator
{
    public class Table
    {
        private readonly List<Player> _playersAtTable;
        public Dealer TableDealer;
        public Shoe Shoe;

        public IReadOnlyCollection<Player> Players => _playersAtTable;
        public Table()
        {
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