using Domain.Model;

namespace Domain.Formation
{
    public interface ISetupSource
    {
        IPlayer[] Players { get; }

        ISkills AverageSkills { get; }
    }
}