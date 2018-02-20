using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Model
{
    internal class Squad : ISquad
    {
        public Squad(IEnumerable<IPlayer> players, ISkills averageSkills)
        {
            Players = players;
            AverageSkills = averageSkills;
        }

        public IEnumerable<IPlayer> Players { get; }
        public ISkills AverageSkills { get; }
    }
}