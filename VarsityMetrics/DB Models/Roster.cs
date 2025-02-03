using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarsityMetrics.DB_Models
{
    public class Roster
    {
        // Before inserting into Roster you need a game with matching game_id and a player with matching player_id

        [PrimaryKey, AutoIncrement, Column("Pk")]
        public int Pk { get; set; }

        [Indexed, Column("game_id")]
        public String? GameId { get; set; }

        [Indexed, Column("player_id")]
        public String? PlayerId { get; set; }

    }
}
