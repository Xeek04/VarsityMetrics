using Supabase.Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("GameStats")]
    public class GameStats
    {
        [PrimaryKey("game_id")]
        public int GameId { get; set; }

        // For Team
        [Column("for_pass_yards")]
        public int? ForPassYards { get; set; }

        [Column("for_rush_yards")]
        public int? ForRushYards { get; set; }

        [Column("for_total_plays")]
        public int? ForTotalPlays { get; set; }

        [Column("for_total_drives")]
        public int? ForTotalDrives { get; set; }

        [Column("for_ints_thrown")]
        public int? ForIntsThrown { get; set; }

        [Column("for_fums_lost")]
        public int? ForFumsLost { get; set; }

        [Column("for_times_sacked")]
        public int? ForTimesSacked { get; set; }

        [Column("for_1st_downs")]
        public int? For1stDowns { get; set; }

        [Column("for_3rd_eff")]
        public int[]? For3rdEff { get; set; }

        [Column("for_4th_eff")]
        public int[]? For4thEff { get; set; }

        [Column("for_poss_time")]
        public int[]? ForPossTime { get; set; }

        // Opposing Team
        [Column("opp_pass_yards")]
        public int? OppPassYards { get; set; }

        [Column("opp_rush_yards")]
        public int? OppRushYards { get; set; }

        [Column("opp_total_plays")]
        public int? OppTotalPlays { get; set; }

        [Column("opp_total_drives")]
        public int? OppTotalDrives { get; set; }

        [Column("opp_ints_thrown")]
        public int? OppIntsThrown { get; set; }

        [Column("opp_fums_lost")]
        public int? OppFumsLost { get; set; }

        [Column("opp_times_sacked")]
        public int? OppTimesSacked { get; set; }

        [Column("opp_1st_downs")]
        public int? Opp1stDowns { get; set; }

        [Column("opp_3rd_eff")]
        public int[]? Opp3rdEff { get; set; }

        [Column("opp_4th_eff")]
        public int[]? Opp4thEff { get; set; }

        [Column("opp_poss_time")]
        public int[]? OppPossTime { get; set; }
    }
}
