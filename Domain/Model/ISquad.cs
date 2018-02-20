using System.Collections.Generic;

namespace Domain.Model
{
    public interface ISquad
    {
        IEnumerable<IPlayer> Players { get; }

        ISkills AverageSkills { get; }
    }
}