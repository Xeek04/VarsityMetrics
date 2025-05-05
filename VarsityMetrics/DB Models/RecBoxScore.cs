using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("RecBoxScore")]
    public class RecBoxScore : BaseModel
    {
        [PrimaryKey("rec_id")]
        public int RecId { get; set; }

        [Column("game_id")]
        public int GameId { get; set; }

        [Column("fname")]
        public string? Fname { get; set; }

        [Column("lname")]
        public string? Lname { get; set; }

        [Column("rec/tar")]
        public int[]? Rec_Tar {  get; set; }

        [Column("yards")]
        public int? Yards { get; set; }

        [Column("tds")]
        public int? TDs { get; set; }
    }
}
