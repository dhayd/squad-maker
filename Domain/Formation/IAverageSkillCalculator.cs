using System.Collections.Generic;
using Domain.Model;

namespace Domain.Formation
{
    public interface IAverageSkillCalculator
    {
        ISkills Calculate(IEnumerable<IPlayer> players);
    }
}