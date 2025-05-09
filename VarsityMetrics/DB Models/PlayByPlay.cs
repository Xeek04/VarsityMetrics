using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using Supabase.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VarsityMetrics.DB_Models
{
    [Table("PlayByPlay")]
    public class PlayByPlay : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("game_id")]
        public int GameId { get; set; }

        [Column("players")]
        public string[]? Players { get; set; }

        [Column("yards")]
        public int? Yards { get; set; }

        [Column("yard_type")]
        public string? YardType { get; set; }

        [Column("sack")]
        public bool Sack { get; set; } = false;

        [Column("interception")]
        public bool Interception { get; set; } = false;

        [Column("fumble")]
        public bool Fumble { get; set; } = false;

        [Column("penalty")]
        public bool Penalty { get; set; } = false;

        [Column("penalty_yards")]
        public int? PenaltyYards { get; set; }

        [Column("next_play")]
        public int? NextPlay { get; set; }

        [Column("td")]
        public bool TD { get; set; } = false;

        [Column("game_time")]
        public string? GameTime { get; set; }

        [Column("down")]
        public int? Down { get; set; }

        [Column("quarter")]
        public int Quarter { get; set; } = 1;

        [Column("play_id")]
        public int? PlayId { get; set; }

        [JsonIgnore]
        public string Description
        {
            get
            {
                if (Players == null || Players.Count() == 0) return "";

                string result = Players[0];
                if (YardType == "Pass" && Players.Count() == 2)
                {
                    result += " pass to " + Players[1];
                }
                else if (YardType == "Pass" && Players.Count() == 1)
                {
                    result += " pass";
                }
                else
                {
                    result += " rush";
                }
                result += " for " + Yards + " yards";
                return result;
            }
        }
    }
}
