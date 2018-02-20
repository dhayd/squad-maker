using System.Collections.Generic;
using Domain.Model;

namespace Domain
{
    public interface IPlayersRepository
    {
        IEnumerable<IPlayer> All();
    }
}
