using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    [Table("Roster")]
    public class Roster
    {

        [PrimaryKey, AutoIncrement, Column("Pk")]
        public int Pk { get; set; }

        [Indexed, Column("game_id")]
        public String? GameId { get; set; }

        [Indexed, Column("player_id")]
        public String? PlayerId { get; set; }

    }
}
