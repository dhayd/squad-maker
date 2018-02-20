using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Model;

namespace Domain.Formation
{
    public class AverageSkillCalculator : IAverageSkillCalculator
    {
        public ISkills Calculate(IEnumerable<IPlayer> players)
        {
            var playersArray = players as IPlayer[] ?? players.ToArray();
            return new Skills(
                (int) Math.Round(playersArray.Average(p => p?.Skills?.Shooting ?? 0)),
                (int) Math.Round(playersArray.Average(p => p?.Skills?.Skating ?? 0)),
                (int) Math.Round(playersArray.Average(p => p?.Skills?.Checking ?? 0)));
        }
    }
}