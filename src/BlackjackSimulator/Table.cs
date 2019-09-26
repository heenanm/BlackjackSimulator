using System.Collections.Generic;
using System.Threading;

namespace BlackjackSimulator
{
    public class Table
    {
        private readonly List<Player> _playersAtTable;
        private readonly List<CPUPlayer> _CPUPlayersAtTable;

        public readonly int MinimumBet;
        public Dealer TableDealer;
        public Shoe Shoe;

        public List<CPUPlayer> CPUPlayers => _CPUPlayersAtTable;
        public List<Player> Players => _playersAtTable;
        public Table(int minimumBet)
        {
            MinimumBet = minimumBet;
            Shoe = new Shoe();
            TableDealer = new Dealer();
            _playersAtTable = new List<Player>();
            _CPUPlayersAtTable = new List<CPUPlayer>();
        }

        public void AddPlayer(Player player)
        {
            _playersAtTable.Add(player);
        }

        public void AddCPUPlayer(CPUPlayer cPUPlayer)
        {
            _playersAtTable.Add(cPUPlayer);
        }
    }
}