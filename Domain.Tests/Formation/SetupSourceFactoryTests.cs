using System.Collections.Generic;
using System.Linq;
using Domain.Formation;
using Domain.Model;
using Fluent.Tests;
using Moq;
using Xunit;

namespace Domain.Tests.Formation
{
    public class SetupSourceFactoryTests
    {
        [Fact]
        public void ShouldReturnPlayersFromRepository()
        {
            IEnumerable<IPlayer> players = 42.ObjectsOf<IPlayer>();
            WhenCreatingSetupSource()
                .And((IPlayersRepository r) => r.All()).Returns(players)
                .Then()
                .ShouldReturn(src => src.Players.SequenceEqual(players));
        }

        [Fact]
        public void ShouldReturnAverageForAllPlayers()
        {
            var skills = new Mock<ISkills>().Object;
            WhenCreatingSetupSource()
                .And((IAverageSkillCalculator c) => c.Calculate(It.IsAny<IEnumerable<IPlayer>>())).Returns(skills)
                .Then()
                .ShouldReturn(src => ReferenceEquals(src.AverageSkills, skills));
        }
        private SetupSourceFactoryFluentTest WhenCreatingSetupSource()
        {
            return new SetupSourceFactoryFluentTest();
        }
        private class SetupSourceFactoryFluentTest : FluentTest<SetupSourceFactory, ISetupSource>
        {
            public override FluentTest<SetupSourceFactory, ISetupSource> Then()
            {
                Exec = () => Sut.New();
                return this;
            }
        }
    }
}
