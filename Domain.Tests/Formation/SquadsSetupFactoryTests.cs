using System.Collections.Generic;
using System.Linq;
using Domain.Formation;
using Domain.Model;
using Fluent.Tests;
using Moq;
using Xunit;

namespace Domain.Tests.Formation
{
    public class SquadsSetupFactoryTests
    {
        [Fact]
        public void ShouldReturn4SquadsFrom42Players()
        {
            WhenCreatingSquads(4)
                .FromPlayers(42.ObjectsOf<IPlayer>())
                .Then()
                .ShouldReturn(ss => ss.Squads.Count() == 4);
        }

        [Fact]
        public void ShouldReturnSquadsOf10From42Players()
        {
            WhenCreatingSquads(4)
                .FromPlayers(42.ObjectsOf<IPlayer>())
                .Then()
                .ShouldReturn(ss => ss.Squads.All(s => s.Players.Count() == 10));
        }

        [Fact]
        public void ShouldReturnWaitingListOf2From42Players()
        {
            WhenCreatingSquads(4)
                .FromPlayers(42.ObjectsOf<IPlayer>())
                .Then()
                .ShouldReturn(ss => ss.WaitingList.Count() == 2);
        }

        [Fact]
        public void ShouldReturnEmptyWaitingListWhenForming5SquadsFrom15Players()
        {
            WhenCreatingSquads(5)
                .FromPlayers(15.ObjectsOf<IPlayer>())
                .Then()
                .ShouldReturn(ss => !ss.WaitingList.Any());
        }

        [Fact]
        public void ShouldReturnAllPlayersInWaitingListWhenForming5SquadsFrom4Players()
        {
            WhenCreatingSquads(5)
                .FromPlayers(4.ObjectsOf<IPlayer>())
                .Then()
                .ShouldReturn(ss => ss.WaitingList.Count() == 4);
        }

        [Fact]
        public void ShouldCreateNoSquadsWhenForming5SquadsFrom4Players()
        {
            WhenCreatingSquads(5)
                .FromPlayers(4.ObjectsOf<IPlayer>())
                .Then()
                .ShouldReturn(ss => !ss.Squads.Any());
        }

        private SquadsSetupFactoryFluentTest WhenCreatingSquads(int numberOfSquads)
        {
            return new SquadsSetupFactoryFluentTest(numberOfSquads);
        }
        private class SquadsSetupFactoryFluentTest : FluentTest<SquadsSetupFactory, ISquadsSetup>
        {
            private readonly int _numberOfSquads;
            private IEnumerable<IPlayer> _players;

            public SquadsSetupFactoryFluentTest(int numberOfSquads)
            {
                _numberOfSquads = numberOfSquads;
            }

            public SquadsSetupFactoryFluentTest FromPlayers(IEnumerable<IPlayer> players)
            {
                _players = players;
                return this;
            }

            public override FluentTest<SquadsSetupFactory, ISquadsSetup> Then()
            {
                Exec = () => Sut.New(_players, _numberOfSquads);
                return this;
            }
        }
    }
}
