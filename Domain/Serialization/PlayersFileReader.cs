using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Domain.Serialization
{
    class PlayersFileReader : IPlayerDataProvider
    {
        public IEnumerable<PlayerData> Get()
        {
            var assembly = Assembly.GetAssembly(typeof(PlayersRepository));
            var resourceStream = assembly.GetManifestResourceStream("Domain.Serialization.players.json");
            string content;
            using (var sr = new StreamReader(resourceStream, Encoding.UTF8))
            {
                content = sr.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<PlayerDataRoot>(content).players;
        }
    }
}