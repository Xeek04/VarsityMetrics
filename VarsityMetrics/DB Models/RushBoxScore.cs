using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("RushBoxScore")]
    public class RushBoxScore : BaseModel
    {
        [PrimaryKey("rush_id")]
        public int RushId { get; set; }

        [Column("game_id")]
        public int GameId { get; set; }

        [Column("fname")]
        public string? Fname { get; set; }

        [Column("lname")]
        public string? Lname { get; set; }

        [Column("carries")]
        public int? Carries { get; set; }

        [Column("yards")]
        public int? Yards { get; set; }

        [Column("tds")]
        public int? TDs { get; set; }

        [Column("fumbles")]
        public int? Fumbles { get; set; }
    }
}
