using SQLite;
namespace VarsityMetrics.DB_Models
{
    [Table("Game")]
    public class Game
    {

        [PrimaryKey, AutoIncrement, Column("Pk")]
        public int Pk { get; set; }

        [Column("opponent")]
        public String? Opponent { get; set; }

        [Column("date")]
        public String? Date { get; set; }
    }
}
