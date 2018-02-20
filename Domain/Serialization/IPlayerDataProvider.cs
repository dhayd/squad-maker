using System.Collections.Generic;

namespace Domain.Serialization
{
    public interface IPlayerDataProvider
    {
        IEnumerable<PlayerData> Get();
    }
}
