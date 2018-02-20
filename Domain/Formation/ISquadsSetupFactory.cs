using System.Collections.Generic;
using Domain.Model;

namespace Domain.Formation
{
    public interface ISquadsSetupFactory
    {
        ISquadsSetup New(IEnumerable<IPlayer> players, int numberOfSquads);
    }
}