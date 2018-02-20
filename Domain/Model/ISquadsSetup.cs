using System.Collections.Generic;

namespace Domain.Model
{
    public interface ISquadsSetup
    {
        IEnumerable<IPlayer> WaitingList { get; }

        IEnumerable<ISquad> Squads { get; }
    }
}