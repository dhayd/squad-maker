using System.Collections.Generic;
using Domain.Model;

namespace Domain.Formation
{
    public interface IDistanceCalculator
    {
        double Calculate(ISkills referentSkill, IEnumerable<ISkills> skills);
    }
}