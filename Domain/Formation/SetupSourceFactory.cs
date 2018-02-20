using System.Linq;
using Domain.Model;

namespace Domain.Formation
{
    public class SetupSourceFactory : ISetupSourceFactory
    {
        private readonly IPlayersRepository _repository;
        private readonly IAverageSkillCalculator _averageSkillCalculator;

        public SetupSourceFactory(IPlayersRepository repository, IAverageSkillCalculator averageSkillCalculator)
        {
            _repository = repository;
            _averageSkillCalculator = averageSkillCalculator;
        }

        public ISetupSource New()
        {
            var players = _repository.All();
            var playersArray = players as IPlayer[] ?? players.ToArray();
            var averageSkills = _averageSkillCalculator.Calculate(playersArray);

            return new SetupSource(playersArray, averageSkills);
        }
    }
}