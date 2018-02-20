using System.Collections.Generic;
using System.Linq;
using Domain.Model;

namespace Domain.Formation
{
    public class SquadsSetupFactory : ISquadsSetupFactory
    {
        private readonly IAverageSkillCalculator _averageSkillCalculator;

        public SquadsSetupFactory(IAverageSkillCalculator averageSkillCalculator)
        {
            _averageSkillCalculator = averageSkillCalculator;
        }

        public ISquadsSetup New(IEnumerable<IPlayer> players, int numberOfSquads)
        {
            var playersArray = players as IPlayer[] ?? players.ToArray();

            int squadSize = playersArray.Length / numberOfSquads;
            if (squadSize == 0)
            {
                return new SquadsSetup(playersArray, Enumerable.Empty<ISquad>());
            }

            var playersInSquadsAndWaitingList = playersArray.ChunkBy(squadSize).ToArray();

            var waitingList = playersInSquadsAndWaitingList.Length == numberOfSquads
                ? Enumerable.Empty<IPlayer>()
                : playersInSquadsAndWaitingList.Last();

            var squads = playersInSquadsAndWaitingList
                .Take(numberOfSquads)
                .Select(ps =>
                {
                    var playersInSquad = ps as IPlayer[] ?? ps.ToArray();
                    return new Squad(playersInSquad, _averageSkillCalculator.Calculate(playersInSquad));
                });

            return new SquadsSetup(waitingList, squads);
        }
    }
}