using System;
using System.Linq;
using Domain.Model;

namespace Domain.Serialization
{
    public class PlayerMapper : IPlayerMapper
    {
        public IPlayer Map(PlayerData playerData)
        {
            if (playerData == null) { throw new ArgumentNullException(nameof(playerData)); }
            if (string.IsNullOrEmpty(playerData._id)) { throw new ArgumentException("Failed to map player data. Player with no ID detected in data source.", nameof(playerData)); }
            if (playerData.skills == null) { throw new ArgumentException($"Failed to map player data. Skills for player with id {playerData._id} are null.", nameof(playerData)); }

            var firstName = playerData.firstName ?? string.Empty;
            var lastName = playerData.lastName ?? string.Empty;

            if (firstName.Equals(string.Empty) && lastName.Equals(string.Empty))
            {
                throw new ArgumentException($"Failed to map player data. Name not provided for player with id {playerData._id}.", nameof(playerData));
            }

            var shootingRate = playerData.skills
                                   .SingleOrDefault(s => "Shooting".Equals(s.type, StringComparison.OrdinalIgnoreCase))
                                   ?.rating ?? 0;
            var skatingRate = playerData.skills
                                  .SingleOrDefault(s => "Skating".Equals(s.type, StringComparison.OrdinalIgnoreCase))
                                  ?.rating ?? 0;
            var checkingRate = playerData.skills
                                  .SingleOrDefault(s => "Checking".Equals(s.type, StringComparison.OrdinalIgnoreCase))
                                  ?.rating ?? 0;

            return new Player(
                playerData._id, 
                firstName, 
                lastName,
                new Skills(shootingRate, skatingRate, checkingRate));
        }
    }
}