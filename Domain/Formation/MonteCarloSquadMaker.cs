using System.Linq;
using Domain.Formation;
using Domain.Model;

namespace Domain
{
    public class MonteCarloSquadMaker : ISquadMaker
    {
        public const int NumberOfShuffles = 1000;
        private readonly IPlayersRepository _repository;
        private readonly IAverageSkillCalculator _averageSkillCalculator;
        private readonly ISquadsSetupFactory _squadSetupFactory;
        private readonly IDistanceCalculator _distanceCalculator;

        public MonteCarloSquadMaker(IPlayersRepository repository, ISquadsSetupFactory squadSetupFactory, IAverageSkillCalculator averageSkillCalculator, IDistanceCalculator distanceCalculator)
        {
            _repository = repository;
            _squadSetupFactory = squadSetupFactory;
            _averageSkillCalculator = averageSkillCalculator;
            _distanceCalculator = distanceCalculator;
        }

        public ISquadsSetup Make(int numberOfSquads)
        {
            var players = _repository.All();
            var playersArray = players as IPlayer[] ?? players.ToArray();
            var averageSkills = _averageSkillCalculator.Calculate(playersArray);

            var candidateSetups = Enumerable.Range(0, NumberOfShuffles)
                .Select(i => _squadSetupFactory.New(playersArray.Shuffle(), numberOfSquads))
                .Select(ss => new
                {
                    SquadsSetup = ss,
                    DistanceFromAverage = _distanceCalculator.Calculate(averageSkills, ss.Squads.Select(s => s.AverageSkills))
                })
                .ToArray();

            var minDistanceFromAverageSkills = candidateSetups.Min(cs => cs.DistanceFromAverage);
            
            return candidateSetups.First(cs => cs.DistanceFromAverage == minDistanceFromAverageSkills).SquadsSetup;
        }
    }
}