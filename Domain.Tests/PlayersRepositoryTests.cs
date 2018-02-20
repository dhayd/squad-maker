using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Domain.Serialization;
using Fluent.Tests;
using Moq;
using Xunit;

namespace Domain.Tests
{
    public class PlayersRepositoryTests
    {
        [Fact]
        public void ShouldGetPlayersFromFile()
        {
            WhenGettingAllPlayers()
                .Then()
                .ShouldCall((IPlayerDataProvider dp) => dp.Get());
        }

        [Fact]
        public void ShouldMapPlayersToDomain()
        {
            var playerData = 6.ObjectsOf<PlayerData>();
            WhenGettingAllPlayers()
                .And((IPlayerDataProvider dp) => dp.Get()).Returns(playerData)
                .Then()
                .ShouldCall((IPlayerMapper m) => m.Map(It.IsAny<PlayerData>()), Times.Exactly(6));
        }

        [Fact]
        public void ShouldReturnMappedPlayers()
        {
            var playerData = new PlayerData();
            var player = new Mock<IPlayer>().Object;
            WhenGettingAllPlayers()
                .And((IPlayerDataProvider dp) => dp.Get()).Returns(new [] {playerData})
                .And((IPlayerMapper m) => m.Map(playerData)).Returns(player)
                .Then()
                .ShouldReturn(players => ReferenceEquals(players.Single(), player));
        }

        private PlayersRepositoryFluentTest WhenGettingAllPlayers()
        {
            return new PlayersRepositoryFluentTest();
        }

        private class PlayersRepositoryFluentTest : FluentTest<PlayersRepository, IEnumerable<IPlayer>>
        {
            public override FluentTest<PlayersRepository, IEnumerable<IPlayer>> Then()
            {
                Exec = () => Sut.All();
                return this;
            }
        }
    }
}
