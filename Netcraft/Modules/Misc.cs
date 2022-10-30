using System.Threading.Tasks;
using System.Net.Http;
using Netcraft.Entities;

namespace Netcraft.Modules
{
    public class MiscModule
    {
        private readonly HttpClient Client;

        public MiscModule(HttpClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Get the leaderboard entries available at <a href="https://report.netcraft.com/stats/leaderboard"></a>.
        /// </summary>
        /// <returns></returns>
        public async Task<LeaderboardEntry[]> GetLeaderboard()
        {
            HttpResponseMessage res = await Client.Request(HttpMethod.Get, "stats/leaderboard");

            return (await res.Deseralize<LeaderboardContainer>()).Entries;
        }
    }
}