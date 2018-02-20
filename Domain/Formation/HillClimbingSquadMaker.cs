using System.Linq;
using Domain.Model;

namespace Domain.Formation
{
    public class HillClimbingSquadMaker : ISquadMaker
    {
        public const int NumberOfIterations = 1000;
        private readonly ISetupSourceFactory _setupSourceFactory;
        private readonly ISquadsSetupFactory _squadSetupFactory;
        private readonly IDistanceCalculator _distanceCalculator;

        public HillClimbingSquadMaker(ISetupSourceFactory setupSourceFactory, ISquadsSetupFactory squadSetupFactory, IDistanceCalculator distanceCalculator)
        {
            _setupSourceFactory = setupSourceFactory;
            _squadSetupFactory = squadSetupFactory;
            _distanceCalculator = distanceCalculator;
        }

        public ISquadsSetup Make(int numberOfSquads)
        {
            var source = _setupSourceFactory.New();
            var players = source.Players.Shuffle();
            var squadSetup = _squadSetupFactory.New(players, numberOfSquads);
            var distance = _distanceCalculator.Calculate(source.AverageSkills, squadSetup.Squads.Select(s => s.AverageSkills));

            for (int i = 0; i < NumberOfIterations; i++)
            {
                var newPlayers = players.RandomSwap();
                squadSetup = _squadSetupFactory.New(newPlayers, numberOfSquads);
                var newDistance = _distanceCalculator.Calculate(source.AverageSkills, squadSetup.Squads.Select(s => s.AverageSkills));

                if (newDistance < distance)
                {
                    players = newPlayers;
                    distance = newDistance;
                }
            }

            return _squadSetupFactory.New(players, numberOfSquads);
        }
    }
}
