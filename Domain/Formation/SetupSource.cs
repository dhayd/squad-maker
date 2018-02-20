using Domain.Model;

namespace Domain.Formation
{
    internal class SetupSource : ISetupSource
    {
        public SetupSource(IPlayer[] players, ISkills averageSkills)
        {
            Players = players;
            AverageSkills = averageSkills;
        }

        public IPlayer[] Players { get; }
        public ISkills AverageSkills { get; }
    }
}