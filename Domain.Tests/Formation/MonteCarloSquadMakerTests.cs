using System.Collections.Generic;
using Domain.Formation;
using Domain.Model;
using Fluent.Tests;
using Moq;
using Xunit;

namespace Domain.Tests.Formation
{
    public class MonteCarloSquadMakerTests
    {
        [Fact]
        public void ShouldGetAllPlayers()
        {
            WhenMakingSquads(6)
                .Then()
                .ShouldCall((IPlayersRepository repo) => repo.All());
        }

        [Fact]
        public void ShouldCalculateAverageSkillsForAllPlayers()
        {
            var players = 42.ObjectsOf<IPlayer>();
            WhenMakingSquads(4)
                .And((IPlayersRepository r) => r.All()).Returns(players)
                .Then()
                .ShouldCall<IAverageSkillCalculator>(isc => isc.Calculate(players));
        }

        [Fact]
        public void ShouldCreateSquadSetups()
        {
            WhenMakingSquads(6)
                .Then()
                .ShouldCall<ISquadsSetupFactory>(f => f.New(It.IsAny<IEnumerable<IPlayer>>(), 6), Times.Exactly(MonteCarloSquadMaker.NumberOfShuffles));
        }

        [Fact]
        public void ShouldCalculateAllDistances()
        {
            WhenMakingSquads(6)
                .Then()
                .ShouldCall<IDistanceCalculator>(c => c.Calculate(It.IsAny<ISkills>(), It.IsAny<IEnumerable<ISkills>>()), Times.Exactly(MonteCarloSquadMaker.NumberOfShuffles));
        }

        private SquadMakerFluentTest WhenMakingSquads(int numberOfSquads)
        {
            return new SquadMakerFluentTest(numberOfSquads);
        }

        private class SquadMakerFluentTest : FluentTest<MonteCarloSquadMaker, ISquadsSetup>
        {
            private readonly int _numberOfSquads;

            public SquadMakerFluentTest(int numberOfSquads)
            {
                _numberOfSquads = numberOfSquads;
                MockFor<IPlayersRepository>().Setup(r => r.All()).Returns(42.ObjectsOf<IPlayer>());
                MockFor<IAverageSkillCalculator>().Setup(c => c.Calculate(It.IsAny<IEnumerable<IPlayer>>())).Returns(new Mock<ISkills>().Object);
                MockFor<ISquadsSetupFactory>().Setup(f => f.New(It.IsAny<IEnumerable<IPlayer>>(), It.IsAny<int>()))
                    .Returns(new Mock<ISquadsSetup>().Object);
            }

            public override FluentTest<MonteCarloSquadMaker, ISquadsSetup> Then()
            {
                Exec = () => Sut.Make(_numberOfSquads);
                return this;
            }
        }
    }
}
