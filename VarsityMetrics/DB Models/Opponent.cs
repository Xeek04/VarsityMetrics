using SQLite;
namespace VarsityMetrics.DB_Models
{
    [Table("Opponent")]
    public class Opponent {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        [Column("Pk")]
        public int Id { get; set; }
        [Column("gid")]
        public int GameId { get; set; }
        [Column("opponent_name")]
        public String OpponentName { get; set; }

    }
}
