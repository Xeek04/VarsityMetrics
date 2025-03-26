using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace VarsityMetrics.DB_Models
{
    [Table("PlayerStats")]
    public class PlayerStats : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("fname")]
        public String? Fname { get; set; }

        [Column("lname")]
        public String? Lname { get; set; }

        [Column("position")]
        public String? Position { get; set; }

        // Passing Stats
        [Column("pass_att")]
        public int? PassAtt{ get; set; }
        
        [Column("pass_comp")]
        public int? PassComp{ get; set; }

        [Column("passing_yards")]
        public int? PassingYards { get; set; }
        
        [Column("pass_tds")]
        public int? PassTDs{ get; set; }

        [Column("interceptions")]
        public int? Interceptions{ get; set; }

        // Rushing Stats

        [Column("rush_att")]
        public int? RushAtt { get; set; }

        [Column("rush_yards")]
        public int? RushYards { get; set; }
        
        [Column("rush_tds")]
        public int? RushTDs { get; set; }

        [Column("fumbles")]
        public int? Fumbles { get; set; }

        // Receiving Stats
        
        [Column("targets")]
        public int? Targets { get; set; }
        
        [Column("receptions")]
        public int? Receptions { get; set; }

        [Column("rec_yards")]
        public int? RecYards { get; set; }

        [Column("rec_tds")]
        public int? RecTDs { get; set; }
    }
}
