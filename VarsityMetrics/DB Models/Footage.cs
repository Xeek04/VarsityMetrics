using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("Footage")]
    public class Footage
    {

        [PrimaryKey, AutoIncrement, Column("Pk")]
        public int Pk { get; set; }

        [Column("url")]
        public String? Uri { get; set; }

        [Indexed, Column("game_id")]
        public int? GameId { get; set; }

        [Indexed, Column("play_id")]
        public int? PlayId { get; set; }
    }

}
