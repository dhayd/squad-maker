using System;
using Domain.Model;
using Domain.Serialization;
using Fluent.Tests;
using Xunit;

namespace Domain.Tests.Serialization
{
    public class PlayerMapperTests
    {
        private const string Shooting = "Shooting";
        private const string Skating = "Skating";
        private const string Checking = "Checking";

        [Fact]
        public void ShouldGuardAgainstNullData()
        {
            WhenMappingPlayerData(null)
                .Then()
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ShouldMapId()
        {
            var playerData = NewPlayerData();
            WhenMappingPlayerData(playerData)
                .Then()
                .ShouldReturn(p => p.Id.Equals(playerData._id));
        }
        
        [Fact]
        public void ShouldThrowWhenIdNotProvided()
        {
            WhenMappingPlayerData(NewPlayerData(p => p._id = null))
                .Then()
                .ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void ShouldMapFirstName()
        {
            var playerData = NewPlayerData();
            WhenMappingPlayerData(playerData)
                .Then()
                .ShouldReturn(p => p.FirstName.Equals(playerData.firstName));
        }

        [Fact]
        public void ShouldNotAssignNullFirstName()

        {
            WhenMappingPlayerData(NewPlayerData(p => p.firstName = null))
                .Then()
                .ShouldReturn(p => p.FirstName.Equals(string.Empty));
        }

        [Fact]
        public void ShouldMapLastName()
        {
            var playerData = NewPlayerData();
            WhenMappingPlayerData(playerData)
                .Then()
                .ShouldReturn(p => p.LastName.Equals(playerData.lastName));
        }

        [Fact]
        public void ShouldNotAssignNullLastName()

        {
            WhenMappingPlayerData(NewPlayerData(p => p.lastName = null))
                .Then()
                .ShouldReturn(p => p.LastName.Equals(string.Empty));
        }

        [Fact]
        public void ShouldThrowWhenNoNameProvided()
        {
            WhenMappingPlayerData(NewPlayerData(p => { p.firstName = null; p.lastName = null; }))
                .Then()
                .ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void ShouldThrowWhenSkillsAreNull()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = null))
                .Then()
                .ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void ShouldMapShootingWhenProvided()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = new[] { new SkillData { type = Shooting, rating = 42 } }))
                .Then()
                .ShouldReturn(p => p.Skills.Shooting == 42);
        }

        [Fact]
        public void ShouldMapShootingWhenProvidedInLowercase()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = new[] { new SkillData { type = Shooting.ToLower(), rating = 42 } }))
                .Then()
                .ShouldReturn(p => p.Skills.Shooting == 42);
        }

        [Fact]
        public void ShouldDefaultShootingTo0WhenNotProvided()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = new SkillData[]{}))
                .Then()
                .ShouldReturn(p => p.Skills.Shooting == 0);
        }

        [Fact]
        public void ShouldMapSkatingWhenProvided()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = new[] { new SkillData { type = Skating, rating = 55 } }))
                .Then()
                .ShouldReturn(p => p.Skills.Skating == 55);
        }

        [Fact]
        public void ShouldMapSkatingWhenProvidedInLowercase()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = new[] { new SkillData { type = Skating.ToLower(), rating = 15 } }))
                .Then()
                .ShouldReturn(p => p.Skills.Skating == 15);
        }

        [Fact]
        public void ShouldDefaultSkatingTo0WhenNotProvided()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = new SkillData[]{}))
                .Then()
                .ShouldReturn(p => p.Skills.Skating == 0);
        }

        [Fact]
        public void ShouldMapCheckingWhenProvided()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = new[] { new SkillData { type = Checking, rating = 88 } }))
                .Then()
                .ShouldReturn(p => p.Skills.Checking == 88);
        }

        [Fact]
        public void ShouldMapCheckingWhenProvidedInLowercase()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = new[] { new SkillData { type = Checking.ToLower(), rating = 88 } }))
                .Then()
                .ShouldReturn(p => p.Skills.Checking == 88);
        }

        [Fact]
        public void ShouldDefaultCheckingTo0WhenNotProvided()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = new SkillData[] { }))
                .Then()
                .ShouldReturn(p => p.Skills.Checking == 0);
        }

        [Fact]
        public void ShouldThrowWhenShootingIsAmbiguous()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = new[] { new SkillData { type = Shooting, rating = 5 }, new SkillData { type = Shooting, rating = 6 } }))
                .Then()
                .ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void ShouldThrowWhenSkatingIsAmbiguous()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = new[]{new SkillData {type = Skating, rating = 5}, new SkillData { type = Skating, rating = 6 } }))
                .Then()
                .ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void ShouldThrowWhenCheckingIsAmbiguous()
        {
            WhenMappingPlayerData(NewPlayerData(p => p.skills = new[] { new SkillData { type = Checking, rating = 5 }, new SkillData { type = Checking, rating = 6 } }))
                .Then()
                .ShouldThrow<InvalidOperationException>();
        }

        private PlayerData NewPlayerData()
        {
            return new PlayerData
            {
                _id = "abcd",
                firstName = "Joe",
                lastName = "Smith",
                skills = new SkillData[] {}
            };
        }

        private PlayerData NewPlayerData(Action<PlayerData> modifyPlayerData)
        {
            var playerData = NewPlayerData();
            modifyPlayerData(playerData);
            return playerData;
        }
        private PlayerMapperFluentTest WhenMappingPlayerData(PlayerData playerData)
        {
            return new PlayerMapperFluentTest(playerData);
        }
        private class PlayerMapperFluentTest : FluentTest<PlayerMapper, IPlayer>
        {
            private readonly PlayerData _playerData;

            public PlayerMapperFluentTest(PlayerData playerData)
            {
                _playerData = playerData;
            }

            public override FluentTest<PlayerMapper, IPlayer> Then()
            {
                Exec = () => Sut.Map(_playerData);
                return this;
            }
        }
    }
}
