using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Domain.Serialization
{
    class PlayersFileReader : IPlayerDataProvider
    {
        public IEnumerable<PlayerData> Get()
        {
            var fileName = @"..\players.json";
            string content;
            using (var sr = new StreamReader(fileName))
            {
                content = sr.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<PlayerDataRoot>(content).players;
        }
    }
}