using System.Text.Json.Serialization;

namespace Netcraft.Entities
{
    public class LeaderboardContainer
    {
        [JsonPropertyName("leaderboard")]
        public LeaderboardEntry[] Entries { get; set; }
    }

    /// <summary>
    /// An entry in the reporter leaderboard at <a href="https://report.netcraft.com/stats/leaderboard"></a>.
    /// </summary>
    public class LeaderboardEntry
    {
        [JsonPropertyName("confirmed_attacks")]
        public int ConfirmedAttacks { get; set; }

        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }

        [JsonPropertyName("percentage")]
        public double Percentage { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }
    }
}