using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("Gamelog")]
    public class Gamelog : BaseModel
    {
        [PrimaryKey("game_id")]
        public int GameId { get; set; }

        [Column("date")]
        public DateTime? Date { get; set; }

        [Column("for_team")]
        public string ForTeam { get; set; } = "";

        [Column("opp_team")]
        public string OppTeam { get; set; } = "";

        [Column("for_score")]
        public string? ForScore { get; set; }

        [Column("opp_score")]
        public string? OppScore { get; set; }

        [Column("home_game")]
        public bool HomeGame { get; set; } = true;

        [Column("location")]
        public string? Location { get; set; }

        [Column("banner_text")]
        public string BannerText => ForTeam + (HomeGame == true ? " vs " : " at ") + OppTeam;

        [Column("stats_entered")]
        public bool StatsEntered { get; set; } = false;
    }
}