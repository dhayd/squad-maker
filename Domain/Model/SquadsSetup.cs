using System.Collections.Generic;

namespace Domain.Model
{
    internal class SquadsSetup : ISquadsSetup
    {
        public SquadsSetup(IEnumerable<IPlayer> waitingList, IEnumerable<ISquad> squads)
        {
            WaitingList = waitingList;
            Squads = squads;
        }

        public IEnumerable<IPlayer> WaitingList { get; }
        public IEnumerable<ISquad> Squads { get; }
    }
}