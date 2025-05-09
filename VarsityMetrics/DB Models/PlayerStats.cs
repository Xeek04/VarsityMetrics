using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VarsityMetrics.DB_Models
{
    [Table("PlayerStats")]
    public class PlayerStats : BaseModel
    {
        [PrimaryKey,Column("player_id")]
        public int Id { get; set; }

        [Column("fname")]
        public string? Fname { get; set; }

        [Column("lname")]
        public string? Lname { get; set; }

        [Column("position")]
        public string? Position { get; set; }

        // Passing Stats
        [Column("pass_att")]
        public int? PassAtt{ get; set; }
        
        [Column("pass_comp")]
        public int? PassComp{ get; set; }

        [Column("pass_yards")]
        public int? PassYards { get; set; }

        [Column("pass_tds")]
        public int? PassTDs{ get; set; }

        [Column("interceptions")]
        public int? Interceptions{ get; set; }

        [JsonIgnore]
        public string PassCompletionPercentage => (PassComp == null) | (PassAtt == null) ? "-" : Math.Round((double)PassComp / (double)PassAtt * 100).ToString();

        [JsonIgnore]
        public string PassYardsPerAttempt => (PassYards == null) | (PassAtt == null) ? "-" : Math.Round((double)PassYards / (double)PassAtt).ToString();

        [JsonIgnore]
        public string PassYardsPerCompletion => (PassYards == null) | (PassComp == null) ? "-" : Math.Round((double)PassYards / (double)PassComp).ToString();

        // Rushing Stats

        [Column("rush_att")]
        public int? RushAtt { get; set; }

        [Column("rush_yards")]
        public int? RushYards { get; set; }
        
        [Column("rush_tds")]
        public int? RushTDs { get; set; }

        [Column("fumbles")]
        public int? Fumbles { get; set; }

        [JsonIgnore]
        public string RushYardsPerAttempt => (RushYards == null) | (RushAtt == null) ? "-" : Math.Round((double)RushYards / (double)RushAtt).ToString();

        // Receiving Stats

        [Column("targets")]
        public int? Targets { get; set; }
        
        [Column("receptions")]
        public int? Receptions { get; set; }

        [Column("rec_yards")]
        public int? RecYards { get; set; }

        [Column("rec_tds")]
        public int? RecTDs { get; set; }

        [JsonIgnore]
        public string RecYardsPerReception => (RecYards == null) | (Receptions == null) ? "-" : Math.Round((double)RecYards / (double)Receptions).ToString();
    }
}
