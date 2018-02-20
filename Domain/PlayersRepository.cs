using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Domain.Serialization;

namespace Domain
{
    public class PlayersRepository : IPlayersRepository
    {
        private readonly IPlayerDataProvider _dataProvider;
        private readonly IPlayerMapper _mapper;

        public PlayersRepository(IPlayerDataProvider dataProvider, IPlayerMapper mapper)
        {
            _dataProvider = dataProvider;
            _mapper = mapper;
        }

        public IEnumerable<IPlayer> All()
        {
            var playerData = _dataProvider.Get()
                .Select(d => _mapper.Map(d))
                .ToArray();
            return playerData;
        }
    }
}