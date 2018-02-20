using Domain.Model;

namespace Domain.Serialization
{
    public interface IPlayerMapper
    {
        IPlayer Map(PlayerData playerData);
    }
}