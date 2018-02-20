using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Model;

namespace Domain.Formation
{
    /// <summary>
    /// The class implements a calculation of how far the average skills of the sqads are from the average skill of all players together.
    /// It uses a pretty random modeling which assumes that each particular skill can be represented as a dimension in a vector,
    /// and calculates sum of Euclidian distances between all players' and each squad's average skills.
    /// </summary>
    public class DistanceCalculator : IDistanceCalculator
    {
        public double Calculate(ISkills referentSkill, IEnumerable<ISkills> skills)
        {
            return skills.Sum(s => Math.Sqrt(
                                       Math.Pow(referentSkill.Shooting - s.Shooting, 2)
                                       + Math.Pow(referentSkill.Skating - s.Skating, 2)
                                       + Math.Pow(referentSkill.Checking - s.Checking, 2)));
        }
    }
}