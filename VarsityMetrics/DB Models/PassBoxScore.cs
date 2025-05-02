using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("PassBoxScore")]
    public class PassBoxScore : BaseModel
    {
        [PrimaryKey("pass_id")]
        public int PassId { get; set; }

        [Column("game_id")]
        public int Gameid { get; set; }

        [Column("fname")]
        public string? Fname { get; set; }

        [Column("lname")]
        public string? Lname { get; set; }

        [Column("c/att")]
        public int[]? C_Att { get; set; }

        [Column("yards")]
        public int? Yards { get; set; }

        [Column("tds")]
        public int? TDs { get; set; }

        [Column("ints")]
        public int? Ints { get; set; }

        [Column("sacks")]
        public int? Sacks { get; set; }
    }
}
