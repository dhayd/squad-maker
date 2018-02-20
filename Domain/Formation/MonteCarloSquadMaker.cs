using System.Linq;
using Domain.Model;

namespace Domain.Formation
{
    public class MonteCarloSquadMaker : ISquadMaker
    {
        public const int NumberOfShuffles = 1000;
        private readonly ISetupSourceFactory _setupSourceFactory;
        private readonly ISquadsSetupFactory _squadSetupFactory;
        private readonly IDistanceCalculator _distanceCalculator;

        public MonteCarloSquadMaker(ISetupSourceFactory setupSourceFactory, ISquadsSetupFactory squadSetupFactory, IDistanceCalculator distanceCalculator)
        {
            _setupSourceFactory = setupSourceFactory;
            _squadSetupFactory = squadSetupFactory;
            _distanceCalculator = distanceCalculator;
        }

        public ISquadsSetup Make(int numberOfSquads)
        {
            var setupSource = _setupSourceFactory.New();

            var candidateSetups = Enumerable.Range(0, NumberOfShuffles)
                .Select(i => _squadSetupFactory.New(setupSource.Players.Shuffle(), numberOfSquads))
                .Select(ss => new
                {
                    SquadsSetup = ss,
                    DistanceFromAverage = _distanceCalculator.Calculate(setupSource.AverageSkills, ss.Squads.Select(s => s.AverageSkills))
                })
                .ToArray();

            var minDistanceFromAverageSkills = candidateSetups.Min(cs => cs.DistanceFromAverage);
            
            return candidateSetups.First(cs => cs.DistanceFromAverage == minDistanceFromAverageSkills).SquadsSetup;
        }
    }
}